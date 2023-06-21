using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainConf.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System.Data.OleDb;
using MainConf.Models.Main;
using Microsoft.AspNetCore.Authorization;
using Npgsql;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using MainConf.Models.Seconds;
using System.Security.Cryptography;
using MainConf.TagHelpers.Sorting;
using MainConf.TagHelpers;
using MainConf.TagHelpers.Filtering;
using System.Drawing;
using System.Drawing.Imaging;
using MainConf.Models.Reading;
using MainConf.Models.Listening;
using MainConf.Models.Writing;
using MainConf.Models.Speaking;

namespace MainConf.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class CandidatesController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        private readonly DataContext _context;
        private ReadingContext rd;
        private ListeningContext ls;
        private WritingContext wr;
        private SpeakingContext sp;
        private UserManager<ApplicationUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        string connectionString = "";

        public CandidatesController(IConfiguration configuration, DataContext context, ReadingContext context1, ListeningContext context2, WritingContext context3, SpeakingContext context4, IWebHostEnvironment hostingEnvironment, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
            rd = context1;
            ls = context2;
            wr = context3;
            sp = context4;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Candidates
        public async Task<IActionResult> IndexAsync(int? location1, int? levels1, string name, int page = 1, SortStateCan sortOrder = SortStateCan.FioAsc)
        {
            var query = from l in _context.Languages
                        from k in _context.Levels
                        from v in _context.Viloyat
                        from c in _context.Candidates
                        .Where(c => c.Group_id == v.Id_viloyat && c.Lang_id == l.Id_language && c.Level_id == k.Id_level)
                        select new CandidView {
                            Can_id = c.Id_candidate,
                            Can_fio = c.Last_name + " " + c.First_name + " " + c.S_name,
                            Pnfl = c.Pnfl,
                            Lan = l.Names_lan,
                            Lev = k.Names_level,
                            Region = v.Names_V
                        };
            var levlist = from l in _context.Levels
                          from k in _context.Languages
                          .Where(k => k.Id_language == l.Lang_id)
                          select new Levels
                          {
                              Id_level=l.Id_level,
                              Names_level=k.Names_lan+" "+l.Names_level,
                              Lang_id=l.Lang_id,
                              Created_by=l.Created_by,
                              Created_time=l.Created_time,
                              Updated_by=l.Updated_by,
                              Updated_time=l.Updated_time,
                          };
            if (location1 > 0 && location1!=null) {
                Viloyat viloyat = _context.Viloyat.FirstOrDefault(p => p.Id_viloyat == location1);
                query = query.Where(p => p.Region == viloyat.Names_V);
            }
            if (levels1 > 0 && levels1 != null) {
                Levels levels = _context.Levels.FirstOrDefault(p => p.Id_level == levels1);
                Languages languages = _context.Languages.FirstOrDefault(p => p.Id_language == levels.Lang_id);
                query = query.Where(p => p.Lan == languages.Names_lan);
                query = query.Where(p => p.Lev == levels.Names_level);
            }
            if (!String.IsNullOrEmpty(name)) {
                query = query.Where(p => p.Pnfl.Contains(name));
            }
            switch (sortOrder)
            {
                case SortStateCan.FioDesc:
                    query = query.OrderByDescending(p => p.Can_fio);
                    break;
                case SortStateCan.LanguageAsc:
                    query = query.OrderBy(p => p.Lan);
                    break;
                case SortStateCan.LanguageDesc:
                    query = query.OrderByDescending(p => p.Lan);
                    break;
                case SortStateCan.RegionAsc:
                    query = query.OrderBy(p => p.Region);
                    break;
                case SortStateCan.RegionDesc:
                    query = query.OrderByDescending(p => p.Region);
                    break;
                case SortStateCan.PnflAsc:
                    query = query.OrderBy(p => p.Pnfl);
                    break;
                case SortStateCan.PnflDesc:
                    query = query.OrderByDescending(p => p.Pnfl);
                    break;
                default:
                    query = query.OrderBy(p => p.Can_fio);
                    break;
            }
            int pageSize = 60;
            var item = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();
            CandidatesViewModel candidatesViewModel = new CandidatesViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                countall = count,
                CandidViewstab = item,
                SortingCandidetes = new SortingCandidetes(sortOrder),
                FilterCandidetes =new FilterCandidetes(_context.Viloyat.ToList(),location1, name, levlist.ToList(),levels1)
            };
            return View(candidatesViewModel);
        }
        public void Listlocation()
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
            List<SelectListItem> items1 = new List<SelectListItem>();
            foreach (var item in _context.Viloyat)
            {
                SelectListItem sel = new SelectListItem
                {
                    Text = item.Names_V,
                    Value = item.Id_viloyat.ToString(),
                };
                SelectListItem s = sel;
                items1.Add(s);


            }
            ViewData["Group_id"] = items1;
        }
        // GET: Candidates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidates = await _context.Candidates
                .FirstOrDefaultAsync(m => m.Id_candidate == id);
            if (candidates == null)
            {
                return NotFound();
            }
            CanDetails can = new CanDetails();
            can.Can_fio = candidates.Last_name + " " + candidates.First_name+" "+candidates.S_name ;            
            can.Pnfl = candidates.Pnfl;
            can.Photo = "data:image/png;base64," + candidates.Photos;
            can.Can_id = candidates.Id_candidate;
            Levels levels = _context.Levels.FirstOrDefault(p=>p.Id_level==candidates.Level_id);
            can.Lev = levels.Names_level;
            Languages languages = _context.Languages.FirstOrDefault(p=>p.Id_language==candidates.Lang_id);
            can.Lan = languages.Names_lan;
            Viloyat viloyat = _context.Viloyat.FirstOrDefault(p=>p.Id_viloyat==candidates.Group_id);
            can.Region = viloyat.Names_V;
            can.LastM = 0;
            can.Exp1 = 0;
            can.Exp2 = 0;
            can.Exp3 = 0;
            Exams exams = _context.Exams.FirstOrDefault(p=>p.Cand_id==candidates.Id_candidate && p.Ended==1);
            if (exams != null) {
                can.Exp1 = 1;
                Exports exports = _context.Exports.FirstOrDefault(p=>p.Id_export==exams.Export1_id);
                can.Exp1_fio= exports.Last_name + " " + exports.First_name + " " + exports.S_name;
                can.Exp1_mark = exams.Mark1;
                can.Starttime = exams.Started_time.ToString("dd/MM/yyyy HH:mm:ss");
                can.Endtime=exams.Ended_time.ToString("dd/MM/yyyy HH:mm:ss");
                Models.Main.PhotoE photoE = _context.PhotoE.FirstOrDefault(p=>p.Exam_id==exams.Id_exam);
                if (photoE != null)
                {
                    can.Photo1 = photoE.Part1;
                    can.Photo2 = photoE.Part2;
                    can.Photo3 = photoE.Part3;
                }
                else {
                    can.Photo1 = "no photo";
                    can.Photo2 = "no photo";
                    can.Photo3 = "no photo";
                }
                if (exams.Examiner_id == 0)
                {
                    can.LastM = exams.Mark3;
                }
                else {
                    can.LastM = exams.LastMark;
                }
                byte[] hash;
                string result = "";
                using (MD5 md5 = MD5.Create())
                {
                    hash = md5.ComputeHash(Encoding.UTF8.GetBytes(exports.Pnfl + "_" + candidates.Pnfl));
                    result = BitConverter.ToString(hash).Replace("-", string.Empty);
                }
                var memory = new MemoryStream();
                string sWebRootFolder = "C:\\speaking\\wwwroot\\last";
                var uploads2 = Path.Combine(_hostingEnvironment.WebRootPath, "last");
                if (!System.IO.File.Exists(uploads2 + "\\" + result + ".mp3"))
                {
                    if (System.IO.File.Exists(sWebRootFolder + "\\" + result + ".mp3"))
                    {
                     /*   string fileName = result + ".mp3";
                        // Use Path class to manipulate file and directory paths.
                        string sourceFile = System.IO.Path.Combine(sWebRootFolder, fileName);
                        string destFile = System.IO.Path.Combine(uploads2, fileName);
                        System.IO.File.Copy(sourceFile, destFile, true);*/
                        string command = "/k cd / && cd c:\\speaking\\wwwroot\\last && xcopy " + result + ".mp3 c:\\admin\\wwwroot\\last /r";
                        System.Diagnostics.Process.Start("CMD.exe", command);
                    }
                }
                // assume FileContext is your file list DB context with FileName property (file name + extension, e.g. Sample.mp3)
                
                can.Audiolink = "~/last/" + result + ".mp3";
                Part1 part1 = _context.Part1s.FirstOrDefault(p=>p.Id_part1==exams.Part1_id);
                Part2 part2 = _context.Part2s.FirstOrDefault(p => p.Id_part2 == exams.Part2_id);
                Part3 part3 = _context.Part3s.FirstOrDefault(p => p.Id_part3 == exams.Part3_id);
                can.Mark1 = _context.Marks.FirstOrDefault(p => p.Exam_id == exams.Id_exam&& p.Typem==1);
                can.Part1 = part1.Question;
                can.Part2 = part2.Question;
                can.Part3 = part3.Question;
                if (exams.Export2_id != 0) {
                    can.Exp2 = 1;
                    Exports exports2 = _context.Exports.FirstOrDefault(p => p.Id_export == exams.Export2_id);
                    can.Exp2_fio = exports2.Last_name + " " + exports2.First_name + " " + exports2.S_name;
                    can.Exp2_mark = exams.Mark2;
                    can.Mark2 = _context.Marks.FirstOrDefault(p => p.Exam_id == exams.Id_exam && p.Typem == 2);
                }
                if (exams.Examiner_id != 0)
                {
                    can.Exp3 = 1;
                    Examiners examiners = _context.Examiners.FirstOrDefault(p => p.Id_examiner == exams.Examiner_id);
                    can.Exp3_fio = examiners.Last_name + " " + examiners.First_name + " " + examiners.S_name;
                    can.Exp3_mark = exams.Mark3;
                    can.Mark3 = _context.Marks.FirstOrDefault(p => p.Exam_id == exams.Id_exam && p.Typem == 3);
                }

            }
            var facelogs = _context.Facelog.Where(p => p.Pnfl == can.Pnfl && p.Entered == 1).OrderByDescending(p => p.Created_time).ToList();
            Facelog facelog = facelogs.FirstOrDefault();
            if (facelog == null)
            {
                can.Entertime = "0";
                can.EnterPhoto = "0";
                can.Entertol = "0";
            }
            else {
                double a = Convert.ToDouble(facelog.Tol.Replace('.',','));
                can.Entertime = facelog.Created_time.ToString("dd/MM/yyyy HH:mm:ss");
                can.EnterPhoto = facelog.Images;
                can.Entertol = ((1-a)*100).ToString()+" %";
            }
            return View(can);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteAll() {
            try
            {
                using (var connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();
                    string comtext = "Delete from \"AspNetUsers\" where \"PNFL\" in (select \"Pnfl\" from \"Candidates\" where \"Pnfl\" not like'22222222222222'); \n Delete from \"Candidates\" where \"Pnfl\" not like'22222222222222';\n Delete from \"Exams\";\n Delete from \"Marks\";\n Delete from \"Fault\";\n SELECT setval('exams_id_exam_seq', 1, true);\n SELECT setval('candidates_id_candidate_seq', 2, true);\n SELECT setval('marks_id_mark_seq', 1, true);\n ALTER SEQUENCE \"Fault_Id_fault_seq\" RESTART WITH 1; \n Delete from \"Log_file\";\n ALTER SEQUENCE \"Log_file_Id_logf_seq\" RESTART WITH 1; \n Delete from \"PhotoE\"; \n ALTER SEQUENCE \"PhotoE_Id_Photo_seq\" RESTART WITH 1;   \n Delete from \"Facelog\"; \n ALTER SEQUENCE \"Facelog_Id_facelog_seq\" RESTART WITH 1;";
                    NpgsqlCommand cmd = new NpgsqlCommand(comtext, connection);
                    cmd.ExecuteReader();
                    string path_audio = @"C:\speaking\wwwroot\audio";
                    System.IO.DirectoryInfo d_audio = new DirectoryInfo(path_audio);

                    foreach (FileInfo file in d_audio.GetFiles())
                    {
                        file.Delete();
                    }
                    string uploads1 = Path.Combine(_hostingEnvironment.WebRootPath, "face\\data1\\" );
                    System.IO.DirectoryInfo data1 = new DirectoryInfo(uploads1);
                    if (Directory.Exists(uploads1))
                    {
                        Directory.Delete(uploads1,true);
                    }
                    string path_last = @"C:\speaking\wwwroot\last";
                    System.IO.DirectoryInfo d_last = new DirectoryInfo(path_last);

                    foreach (FileInfo file in d_last.GetFiles())
                    {
                        file.Delete();
                    }
                    
                   string path_access = @"C:\speaking\wwwroot\access";
                    System.IO.DirectoryInfo d_access = new DirectoryInfo(path_access);

                    foreach (FileInfo file in d_access.GetFiles())
                    {
                        file.Delete();
                    }
                }
            }
            catch (SqlException e) {
                return Redirect (e.ToString());
            }

                return RedirectToAction("Index");
        }
        // GET: Candidates/Create
      
        public IActionResult Insertlist()
        {
            return View();
        }
       
        [Authorize(Roles = "Admin")]
        public IActionResult InsertExport()
        {
            return View();
        }
        
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            Listlocation();
            return View();
        }
        // POST: Candidates/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_candidate,First_name,Last_name,S_name,Pnfl,Phones,Photos,Lang_id,Level_id,Group_id,Created_by,Created_time,Updated_by,Updated_time")] Candidates candidates)
        {
            if (ModelState.IsValid)
            {
                Levels levels = _context.Levels.FirstOrDefault(p=>p.Id_level==candidates.Level_id);
                candidates.Lang_id = levels.Lang_id;
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                candidates.Created_by = user.Id;
                candidates.Created_time = DateTime.Now;
                candidates.Updated_by = user.Id;
                candidates.Updated_time = DateTime.Now;
                ApplicationUser userexcit = await _userManager.FindByNameAsync(candidates.Pnfl);
                Candidates candidates1 = _context.Candidates.FirstOrDefault(p=>p.Pnfl==candidates.Pnfl);
                if (candidates1 != null)
                {
                    Listlocation();
                    ModelState.AddModelError("ModelOnly", candidates.Pnfl + " JSHIR liy nomzod bazda mavjud! Iltimos Tekshiring");
                    return View(candidates);
                }else
                if (userexcit != null) {
                    Listlocation();
                    ModelState.AddModelError("ModelOnly", candidates.Pnfl + " JSHIR liy Foydalanuvchi bazda mavjud! Iltimos Tekshiring. va boshqa JSHIR bilan bazga kiriting!");
                    return View(candidates);
                }
                else
                {

                    candidates.Photos = candidates.Photos.Replace("\n", "");
                    candidates.Photos = candidates.Photos.Replace(" ", "");
                    candidates.Photos = candidates.Photos.Replace("data:image/png;base64,", "");
                    candidates.Photos = candidates.Photos.Replace("data:image/jpeg;base64,", "");
                    _context.Add(candidates);
                    await _context.SaveChangesAsync();
                    string uploads1 = Path.Combine(_hostingEnvironment.WebRootPath, "face\\data1\\" + candidates.Pnfl);
                    System.IO.Directory.CreateDirectory(uploads1);
                    string image1 = uploads1 + "\\1.jpg";
                    string image2 = uploads1 + "\\2.jpg";
                    try
                    {
                        

                        byte[] bytes = Convert.FromBase64String(candidates.Photos);
                        using (Image image = Image.FromStream(new MemoryStream(bytes)))
                        {
                            image.Save(image1, ImageFormat.Jpeg);  // Or Png
                        }

                        byte[] bytes1 = Convert.FromBase64String(candidates.Photos);
                        using (Image image = Image.FromStream(new MemoryStream(bytes1)))
                        {
                            image.Save(image2, ImageFormat.Jpeg);  // Or Png
                        }
                    }
                    catch (Exception e)
                    {
                        string a = e.ToString();
                    }
                    ApplicationUser user1 = new ApplicationUser { UserName = candidates.Pnfl, PNFL = candidates.Pnfl, Position = "0.4", Is_active = "1" };
                    await _userManager.CreateAsync(user1, user1.PNFL);
                    await _userManager.AddToRoleAsync(user1, "Candidate");
                   
                    return RedirectToAction(nameof(Index));
                }
            }
            Listlocation();
            return View(candidates);
        }

        // GET: Candidates/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidates = await _context.Candidates.FindAsync(id);
            if (candidates == null)
            {
                return NotFound();
            }
            Listlocation();
            candidates.Photos = "data:image/png;base64," + candidates.Photos;
            return View(candidates);
        }

     
        // GET: Candidates/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var candidates = await _context.Candidates
                .FirstOrDefaultAsync(m => m.Id_candidate == id);
            if (candidates == null)
            {
                return NotFound();
            }

            return View(candidates);
        }
        [Authorize(Roles = "Admin")]
        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var candidates = await _context.Candidates.FindAsync(id);
            _context.Candidates.Remove(candidates);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(IndexAsync));
        }

        private bool CandidatesExists(int id)
        {
            return _context.Candidates.Any(e => e.Id_candidate == id);
        }
       
    }
}
