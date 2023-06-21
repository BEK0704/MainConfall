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
using MainConf.Models.Seconds;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using MainConf.TagHelpers.Sorting;
using MainConf.TagHelpers;
using MainConf.TagHelpers.Filtering;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;

namespace MainConf.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ExportsController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;

        private readonly DataContext _context;
        private UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;

        public ExportsController(DataContext context, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        // GET: Exports
        public async Task<IActionResult> Index(int? levels1, string name, int page = 1, SortStateExp sortOrder = SortStateExp.FioAsc)
        {
            var query = from l in _context.Languages
                        from k in _context.Levels
                        from c in _context.Exports
                        .Where(c => c.Lang_id == l.Id_language && c.Level_id == k.Id_level)
                        select new ExpertView
                        {
                            Exp_id = c.Id_export,
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
                FilterExpert = new FilterExperts( name, levlist.ToList(), levels1)
            };

            return View(candidatesViewModel);
        }

       
       
       

        public void Listlocation()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            SelectListItem sel2 = new SelectListItem
            {
                Text = "Barchasi",
                Value = "0"
            };
            items.Add(sel2);
            foreach (var item in _context.Languages)
            {
                SelectListItem sel = new SelectListItem
                {
                    Text = item.Names_lan,
                    Value = item.Id_language.ToString()
                };
                SelectListItem s = sel;
                items.Add(s);

                ViewData["Location_id"] = items;
            }
        }
        // GET: Exports/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exports = await _context.Exports
                .FirstOrDefaultAsync(m => m.Id_export == id);
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

            var lan = _context.Languages.FirstOrDefault(m=>m.Id_language==exports.Lang_id);
            var lev = _context.Levels.FirstOrDefault(m=>m.Id_level==exports.Level_id);
            var exams1 = _context.Exams.Where(m=>m.Export1_id==exports.Id_export);
            var exams2 = _context.Exams.Where(m => m.Export2_id == exports.Id_export);
            var facelogs = _context.Facelog.Where(p => p.Pnfl == exports.Pnfl && p.Entered == 1).OrderByDescending(p => p.Created_time).ToList();
            Facelog facelog = facelogs.FirstOrDefault();
            ExpDeatails expDeatails = new ExpDeatails { Exp_id = exports.Id_export, Exp_fio = exports.Last_name + " " + exports.First_name + " " + exports.S_name, Phone = exports.Phones, Photo = "data:image/png;base64,"+ exports.Photos, Pnfl = exports.Pnfl, Lan = lan.Names_lan, Lev = lev.Names_level, First = exams1.Count(), Second=exams2.Count(), Man=a };
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
       

        // GET: Exports/Create
        public IActionResult Create()
        {
            Listlocation1();
            return View();
        }


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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_export,First_name,Last_name,S_name,Pnfl,Phones,Photos,Lang_id,Level_id,Created_by,Created_time,Updated_by,Updated_time")] Exports exports)
        {
            if (ModelState.IsValid)
            {
                Levels levels = _context.Levels.FirstOrDefault(p => p.Id_level == exports.Level_id);
                exports.Lang_id = levels.Lang_id;
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                exports.Created_by = user.Id;
                exports.Created_time = DateTime.Now;
                exports.Updated_by = user.Id;
                exports.Updated_time = DateTime.Now;
                ApplicationUser userexcit = await _userManager.FindByNameAsync(exports.Pnfl);
                Exports exports1 = _context.Exports.FirstOrDefault(p => p.Pnfl == exports.Pnfl);
                if (exports1 != null)
                {
                    Listlocation1();
                    ModelState.AddModelError("All", exports.Pnfl + " JSHIR li Ekspert bazda mavjud! Iltimos Tekshiring");
                    return View(exports);
                }
                else
               if (userexcit != null)
                {
                    bool rol = await _userManager.IsInRoleAsync(userexcit, "Examiner");
                    if (rol)
                    {
                        exports.Photos = exports.Photos.Replace("\n", "");
                        exports.Photos = exports.Photos.Replace(" ", "");
                        exports.Photos = exports.Photos.Replace("data:image/png;base64,", "");
                        exports.Photos = exports.Photos.Replace("data:image/jpeg;base64,", "");
                        _context.Add(exports);
                        await _context.SaveChangesAsync();
                        await _userManager.AddToRoleAsync(userexcit, "Export");
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        Listlocation1();
                        ModelState.AddModelError("All", exports.Pnfl + " JSHIR li Foydalanuvchi bazda mavjud! Iltimos Tekshiring. va boshqa JSHIR bilan bazga kiriting! Bu foydalnuvchi Ekspert bo'la olmaydi.");
                        return View(exports);
                    }
                }
                else
                {
                    exports.Photos = exports.Photos.Replace("\n", "");
                    exports.Photos = exports.Photos.Replace(" ", "");
                    exports.Photos = exports.Photos.Replace("data:image/png;base64,", "");
                    exports.Photos = exports.Photos.Replace("data:image/jpeg;base64,", "");
                    _context.Add(exports);
                    await _context.SaveChangesAsync();
                    string uploads1 = Path.Combine(_hostingEnvironment.WebRootPath, "face\\data\\" + exports.Pnfl);
                    System.IO.Directory.CreateDirectory(uploads1);
                    string image1 = uploads1 + "\\1.jpg";
                    string image2 = uploads1 + "\\2.jpg";
                    try
                    {


                        byte[] bytes = Convert.FromBase64String(exports.Photos);
                        using (Image image = Image.FromStream(new MemoryStream(bytes)))
                        {
                            image.Save(image1, ImageFormat.Jpeg);  // Or Png
                        }

                        byte[] bytes1 = Convert.FromBase64String(exports.Photos);
                        using (Image image = Image.FromStream(new MemoryStream(bytes1)))
                        {
                            image.Save(image2, ImageFormat.Jpeg);  // Or Png
                        }
                    }
                    catch (Exception e)
                    {
                        string a1 = e.ToString();
                    }
                    ApplicationUser user1 = new ApplicationUser { UserName = exports.Pnfl, PNFL = exports.Pnfl, Position = "0.4", Is_active = "1" };
                    await _userManager.CreateAsync(user1, user1.PNFL);
                    await _userManager.AddToRoleAsync(user1, "Export");

                    return RedirectToAction(nameof(Index));
                }
            }
            Listlocation1();
            return View(exports);
        }

        // GET: Exports/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Listlocation1();
            var exports = await _context.Exports.FindAsync(id);
            if (exports == null)
            {
                return NotFound();
            }
            exports.Photos = "data:image/png;base64," + exports.Photos;
            return View(exports);
        }

        // POST: Exports/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_export,First_name,Last_name,S_name,Pnfl,Phones,Photos,Lang_id,Level_id,Created_by,Created_time,Updated_by,Updated_time")] Exports exports)
        {
            if (id != exports.Id_export)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Levels levels = _context.Levels.FirstOrDefault(p => p.Id_level == exports.Level_id);
                    exports.Lang_id = levels.Lang_id;
                    ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    //   Candidates candidates1 = _context.Candidates.Find(candidates.Id_candidate);
                    //  candidates.Pnfl = candidates1.Pnfl;
                    exports.Updated_by = user.Id;
                    exports.Updated_time = DateTime.Now;
                    exports.Photos = exports.Photos.Replace("\n", "");
                    exports.Photos = exports.Photos.Replace(" ", "");
                    exports.Photos = exports.Photos.Replace("data:image/png;base64,", "");
                    exports.Photos = exports.Photos.Replace("data:image/jpeg;base64,", "");
                    _context.Update(exports);
                    await _context.SaveChangesAsync();
                    string uploads1 = Path.Combine(_hostingEnvironment.WebRootPath, "face\\data\\" + exports.Pnfl);
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


                        byte[] bytes = Convert.FromBase64String(exports.Photos);
                        using (Image image = Image.FromStream(new MemoryStream(bytes)))
                        {
                            image.Save(image1, ImageFormat.Jpeg);  // Or Png
                        }

                        byte[] bytes1 = Convert.FromBase64String(exports.Photos);
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
                    if (!ExportsExists(exports.Id_export))
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
            Listlocation1();
            return View(exports);
        }

        // GET: Exports/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exports = await _context.Exports
                .FirstOrDefaultAsync(m => m.Id_export == id);
            if (exports == null)
            {
                return NotFound();
            }

            return View(exports);
        }

        

        private bool ExportsExists(int id)
        {
            return _context.Exports.Any(e => e.Id_export == id);
        }
        public  IActionResult Started()
        {
            return View();
        }
    
        public IActionResult Waited()
        {
            return View();
        }
        public IActionResult SecondCount()
        {
            return View();
        }
        public IActionResult ThirdCounts()
        {
            return View();
        }
    }
}
