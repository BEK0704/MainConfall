using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainConf.Models;
using MainConf.Models.Main;
using Microsoft.AspNetCore.Authorization;
using System.Data.OleDb;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.IO;
using MainConf.Models.Seconds;
using MainConf.TagHelpers;
using MainConf.TagHelpers.Sorting;
using MainConf.TagHelpers.Filtering;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace MainConf.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExaminersController : Controller
    {
        private readonly DataContext _context;
        private UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        private IWebHostEnvironment _hostingEnvironment;
        public ExaminersController(DataContext context, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Examiners
        public async Task<IActionResult> Index(int? levels1, string name, int page = 1, SortStateExp sortOrder = SortStateExp.FioAsc)
        {
            var query = from l in _context.Languages
                        from k in _context.Levels
                        from c in _context.Examiners
                        .Where(c => c.Lang_id == l.Id_language && c.Level_id == k.Id_level)
                        select new ExpertView
                        {
                            Exp_id = c.Id_examiner,
                            Exp_fio = c.Last_name + " " + c.First_name + " " + c.S_name,
                            Pnfl = c.Pnfl,
                            Lan = l.Names_lan,
                            Lev = k.Names_level
                        };
            var levlist = from l in _context.Levels
                          from k in _context.Languages
                          .Where(k => k.Id_language == l.Lang_id)
                          select new Levels
                          {
                              Id_level = l.Id_level,
                              Names_level = k.Names_lan + " " + l.Names_level,
                              Lang_id = l.Lang_id,
                              Created_by = l.Created_by,
                              Created_time = l.Created_time,
                              Updated_by = l.Updated_by,
                              Updated_time = l.Updated_time,
                          };
            if (levels1 > 0 && levels1 != null)
            {
                Levels levels = _context.Levels.FirstOrDefault(p => p.Id_level == levels1);
                Languages languages = _context.Languages.FirstOrDefault(p => p.Id_language == levels.Lang_id);
                query = query.Where(p => p.Lan == languages.Names_lan);
                query = query.Where(p => p.Lev == levels.Names_level);
            }
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Pnfl.Contains(name));
            }
            switch (sortOrder)
            {
                case SortStateExp.FioDesc:
                    query = query.OrderByDescending(p => p.Exp_fio);
                    break;
                case SortStateExp.LanguageAsc:
                    query = query.OrderBy(p => p.Lan);
                    break;
                case SortStateExp.LanguageDesc:
                    query = query.OrderByDescending(p => p.Lan);
                    break;
                case SortStateExp.PnflAsc:
                    query = query.OrderBy(p => p.Pnfl);
                    break;
                case SortStateExp.PnflDesc:
                    query = query.OrderByDescending(p => p.Pnfl);
                    break;
                default:
                    query = query.OrderBy(p => p.Exp_fio);
                    break;
            }
            int pageSize = 10;
            var item = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();
            ExpertViewModel candidatesViewModel = new ExpertViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                countall = count,
                ExpertViewstab = item,
                SortingExpert = new SortingExpert(sortOrder),
                FilterExpert = new FilterExperts(name, levlist.ToList(), levels1)
            };

            return View(candidatesViewModel);
        }
        public async Task<IActionResult> Card(string Pnfl) {
            if (Pnfl == null)
            {
                return NotFound();
            }
            ApplicationUser appuser = await _userManager.FindByNameAsync(Pnfl);
            MemoryStream mem = new MemoryStream();
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            string sFileName = @"1.png";
            string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);

            QRCodeGenerator _qrCode = new QRCodeGenerator();
            QRCodeData _qrCodeData;
            Examiners export = _context.Examiners.FirstOrDefault(f => f.Pnfl == appuser.UserName);
            _qrCodeData = _qrCode.CreateQrCode(export.Pnfl, QRCodeGenerator.ECCLevel.Q);
            
            QRCode qrCode = new QRCode(_qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                qrCodeImage.Save(fs, System.Drawing.Imaging.ImageFormat.Png);

            }

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(mem);
            }
            mem.Position = 0;

            Examiners exports = _context.Examiners.FirstOrDefault(p => p.Pnfl == appuser.UserName);
            Languages languages = _context.Languages.FirstOrDefault(p => p.Id_language == exports.Lang_id);
            Levels levels = _context.Levels.FirstOrDefault(p => p.Id_level == exports.Level_id);
            // int a = exports.Last_name.Length;
            CardModel model = new CardModel();
            model.Fio = exports.Last_name.Substring(0, 1) + exports.Last_name.Substring(1, exports.Last_name.Length - 1).ToLower() + " " + exports.First_name.Substring(0, 1) + " " + exports.S_name.Substring(0, 1);
            model.Lan = languages.Names_lan;
            model.Lev = levels.Names_level;
            model.Location = "Toshkent shahri";
            model.Pnfl = exports.Pnfl;
            model.QRCoderlink = "~/1.png";
            if (model.Lev.Equals("B1"))
                model.Backgroundlink = "~/images/B1.jpg";
            if (model.Lev.Equals("B2"))
                model.Backgroundlink = "~/images/B2.jpg";
            else
                model.Backgroundlink = "~/images/C1.jpg";

            var base64 = exports.Photos;
            ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);
            ViewBag.imagelegth = base64.Length;
            return View(model);
        }
        // GET: Examiners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var exports = await _context.Examiners
                .FirstOrDefaultAsync(m => m.Id_examiner == id);
            if (exports == null)
            {
                return NotFound();
            }
            ApplicationUser user = await _userManager.FindByNameAsync(exports.Pnfl);
            int a = 0;
            bool rol = await _userManager.IsInRoleAsync(user, "Manager");
            if (rol)
            {
                a = 1;
            }

            var lan = _context.Languages.FirstOrDefault(m => m.Id_language == exports.Lang_id);
            var lev = _context.Levels.FirstOrDefault(m => m.Id_level == exports.Level_id);
            var exams1 = _context.Exams.Where(m => m.Examiner_id == exports.Id_examiner);
            ExpDeatails expDeatails = new ExpDeatails { Exp_id = exports.Id_examiner, Exp_fio = exports.Last_name + " " + exports.First_name + " " + exports.S_name, Phone = exports.Phones, Photo = "data:image/png;base64," + exports.Photos, Pnfl = exports.Pnfl, Lan = lan.Names_lan, Lev = lev.Names_level, First = exams1.Count(), Second = 0, Man = a };
            var facelogs = _context.Facelog.Where(p => p.Pnfl == exports.Pnfl && p.Entered==1).OrderByDescending(p => p.Created_time).ToList();
            Facelog facelog = facelogs.FirstOrDefault();
            if (facelog == null)
            {
                expDeatails.Entertime = "0";
                expDeatails.EnterPhoto = "0";
                expDeatails.Entertol = "0";
            }
            else
            {
                double a1 = Convert.ToDouble(facelog.Tol.Replace('.', ','));
                expDeatails.Entertime = facelog.Created_time.ToString("dd/MM/yyyy HH:mm:ss");
                expDeatails.EnterPhoto = facelog.Images;
                expDeatails.Entertol = ((1 - a1) * 100).ToString() + " %";
            }

            return View(expDeatails);
        }

        public IActionResult InsertExaminers()
        {
            return View();
        }
        
        // GET: Examiners/Create
        public void Listlocation1()
        {
            var levlist = from l in _context.Levels
                          from k in _context.Languages
                          .Where(k => k.Id_language == l.Lang_id)
                          select new Levels
                          {
                              Id_level = l.Id_level,
                              Names_level = k.Names_lan + " " + l.Names_level,
                              Lang_id = l.Lang_id,
                              Created_by = l.Created_by,
                              Created_time = l.Created_time,
                              Updated_by = l.Updated_by,
                              Updated_time = l.Updated_time,
                          };
            //  int a = levlist.Count();
            List<SelectListItem> items = new List<SelectListItem>();
            foreach (var item in levlist)
            {
                SelectListItem sel = new SelectListItem
                {
                    Text = item.Names_level,
                    Value = item.Id_level.ToString(),
                };
                SelectListItem s = sel;
                items.Add(s);


            }
            ViewData["Level_id"] = items;
        }
        public IActionResult Create()
        {
            Listlocation1();
            return View();
        }

       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examiners = await _context.Examiners.FindAsync(id);
            if (examiners == null)
            {
                return NotFound();
            }
            Listlocation1();
            examiners.Photos = "data:image/png;base64," + examiners.Photos;
            return View(examiners);
        }

        // POST: Examiners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_examiner,First_name,Last_name,S_name,Pnfl,Phones,Photos,Lang_id,Level_id,Created_by,Created_time,Updated_by,Updated_time")] Examiners examiners)
        {
            if (id != examiners.Id_examiner)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Levels levels = _context.Levels.FirstOrDefault(p => p.Id_level == examiners.Level_id);
                    examiners.Lang_id = levels.Lang_id;
                    ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    //   Candidates candidates1 = _context.Candidates.Find(candidates.Id_candidate);
                    //  candidates.Pnfl = candidates1.Pnfl;
                    examiners.Updated_by = user.Id;
                    examiners.Updated_time = DateTime.Now;
                    examiners.Photos = examiners.Photos.Replace("\n", "");
                    examiners.Photos = examiners.Photos.Replace(" ", "");
                    examiners.Photos = examiners.Photos.Replace("data:image/png;base64,", "");
                    examiners.Photos = examiners.Photos.Replace("data:image/jpeg;base64,", "");
                    _context.Update(examiners);
                    await _context.SaveChangesAsync();
                    string uploads1 = Path.Combine(_hostingEnvironment.WebRootPath, "face\\data\\" + examiners.Pnfl);
                    System.IO.DirectoryInfo data1 = new DirectoryInfo(uploads1);
                    if (Directory.Exists(uploads1))
                    {
                        Directory.Delete(uploads1, true);
                    }                    
                    System.IO.Directory.CreateDirectory(uploads1);
                    string image1 = uploads1 + "\\1.jpg";
                    string image2 = uploads1 + "\\2.jpg";
                    try
                    {


                        byte[] bytes = Convert.FromBase64String(examiners.Photos);
                        using (Image image = Image.FromStream(new MemoryStream(bytes)))
                        {
                            image.Save(image1, ImageFormat.Jpeg);  // Or Png
                        }

                        byte[] bytes1 = Convert.FromBase64String(examiners.Photos);
                        using (Image image = Image.FromStream(new MemoryStream(bytes1)))
                        {
                            image.Save(image2, ImageFormat.Jpeg);  // Or Png
                        }
                    }
                    catch (Exception e)
                    {
                        string a1 = e.ToString();
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExaminersExists(examiners.Id_examiner))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(examiners);
        }

        // GET: Examiners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examiners = await _context.Examiners
                .FirstOrDefaultAsync(m => m.Id_examiner == id);
            if (examiners == null)
            {
                return NotFound();
            }

            return View(examiners);
        }

       

        private bool ExaminersExists(int id)
        {
            return _context.Examiners.Any(e => e.Id_examiner == id);
        }


    }
}
