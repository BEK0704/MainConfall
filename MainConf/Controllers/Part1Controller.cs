using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MainConf.Models;
using MainConf.Models.Main;
using MainConf.Models.Seconds;
using MainConf.TagHelpers.Sorting;
using MainConf.TagHelpers;
using MainConf.TagHelpers.Filtering;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MainConf.Controllers
{
    [Authorize(Roles = "Admin")]
    public class Part1Controller : Controller
    {
        private readonly DataContext _context;
        private UserManager<ApplicationUser> _userManager;

        public Part1Controller(UserManager<ApplicationUser> userManager, DataContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Part1
        public async Task<IActionResult> Index( int? levels1, string name, int page = 1, SortStatePart sortOrder = SortStatePart.TitleAsc)
        {
            var query = from r in _context.Levels
                        from l in _context.Languages
                        from p in _context.Part1s
                        .Where(p => p.Level_id == r.Id_level && r.Lang_id == l.Id_language)
                        select new Questions {
                        Id_part=p.Id_part1,
                        Actived=p.Actived,
                        Level=l.Names_lan+" "+r.Names_level,
                        Level_id=p.Level_id,
                        Question=p.Question,
                        Timeq=p.Timeq,
                        Title=p.Title
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
            if (levels1 > 0) {
                query = query.Where(p => p.Level_id == levels1);
            }
            if (!String.IsNullOrEmpty(name))
            {
                query = query.Where(p => p.Question.Contains(name) || p.Title.Contains(name));
            }
            switch (sortOrder)
            {
                case SortStatePart.TitleDesc:
                    query = query.OrderByDescending(p => p.Title);
                    break;
                case SortStatePart.LanguageAsc:
                    query = query.OrderBy(p => p.Level);
                    break;
                case SortStatePart.LanguageDesc:
                    query = query.OrderByDescending(p => p.Level);
                    break;
                case SortStatePart.ActiveAsc:
                    query = query.OrderBy(p => p.Actived);
                    break;
                case SortStatePart.ActiveDesc:
                    query = query.OrderByDescending(p => p.Actived);
                    break;
                default:
                    query = query.OrderBy(p => p.Title);
                    break;
            }
            int pageSize = 10;
            var item = await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            var count = await query.CountAsync();
            PartsViewModel candidatesViewModel = new PartsViewModel
            {
                PageViewModel = new PageViewModel(count, page, pageSize),
                countall = count,
                Questionstab = item,
                SortingPart = new SortingPart(sortOrder),
                FilteringPart = new FilteringPart( name, levlist.ToList(), levels1)
            };
            return View(candidatesViewModel);
        }
        public IActionResult Active (int? id) {

            Part1 part1 = _context.Part1s.FirstOrDefault(p=>p.Id_part1==id);
            part1.Actived = 1;
            _context.Update(part1);
            _context.SaveChanges();
            var query = from r in _context.Levels
                        from l in _context.Languages
                        from p in _context.Part1s
                        .Where(p => p.Level_id == r.Id_level && r.Lang_id == l.Id_language && p.Id_part1 == id)
                        select new Questions
                        {
                            Id_part = p.Id_part1,
                            Actived = p.Actived,
                            Level = l.Names_lan + " " + r.Names_level,
                            Level_id = p.Level_id,
                            Question = p.Question,
                            Timeq = p.Timeq,
                            Title = p.Title
                        };
            var part12 = query.FirstOrDefault(m => m.Id_part == id);
            return View("Details",part12);

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
            int a = levlist.Count();
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
            ViewData["Level_id1"] = items;
        }
        public IActionResult Deactive(int? id)
        {

            Part1 part1 = _context.Part1s.FirstOrDefault(p => p.Id_part1 == id);
            part1.Actived = 0;
            _context.Update(part1);
            _context.SaveChanges();
            var query = from r in _context.Levels
                        from l in _context.Languages
                        from p in _context.Part1s
                        .Where(p => p.Level_id == r.Id_level && r.Lang_id == l.Id_language && p.Id_part1 == id)
                        select new Questions
                        {
                            Id_part = p.Id_part1,
                            Actived = p.Actived,
                            Level = l.Names_lan + " " + r.Names_level,
                            Level_id = p.Level_id,
                            Question = p.Question,
                            Timeq = p.Timeq,
                            Title = p.Title
                        };
            var part12 = query.FirstOrDefault(m => m.Id_part == id);
            return View("Details", part12);

        }

        // GET: Part1/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var query = from r in _context.Levels
                        from l in _context.Languages
                        from p in _context.Part1s
                        .Where(p => p.Level_id == r.Id_level && r.Lang_id == l.Id_language && p.Id_part1==id)
                        select new Questions
                        {
                            Id_part = p.Id_part1,
                            Actived = p.Actived,
                            Level = l.Names_lan + " " + r.Names_level,
                            Level_id = p.Level_id,
                            Question = p.Question,
                            Timeq = p.Timeq,
                            Title = p.Title
                        };

            /* var part1 = await _context.Part1s
                 .FirstOrDefaultAsync(m => m.Id_part1 == id);*/
            var part1 =  query.FirstOrDefault(m=>m.Id_part==id);
            if (part1 == null)
            {
                return NotFound();
            }

            return View(part1);
        }

        // GET: Part1/Create
        public IActionResult Create()
        {
            Listlocation();
            return View();
        }

        // POST: Part1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id_part1,Question,Timeq,Level_id,Actived,Created_by,Created_time,Updated_by,Updated_time,Title")] Part1 part1)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                part1.Created_by = user.Id;
                part1.Created_time = DateTime.Now;
                part1.Updated_by = user.Id;
                part1.Updated_time = DateTime.Now;
                part1.Actived = 1;
                _context.Add(part1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            Listlocation();
            return View(part1);
        }

        // GET: Part1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            Listlocation();
            if (id == null)
            {
                return NotFound();
            }

            var part1 = await _context.Part1s.FindAsync(id);
            if (part1 == null)
            {
                return NotFound();
            }
            Listlocation();
            return View(part1);
        }

        // POST: Part1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id_part1,Question,Timeq,Level_id,Actived,Created_by,Created_time,Updated_by,Updated_time,Title")] Part1 part1)
        {
            Listlocation();
            if (id != part1.Id_part1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    ApplicationUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                    part1.Updated_by = user.Id;
                    part1.Updated_time = DateTime.Now;
                    _context.Update(part1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Part1Exists(part1.Id_part1))
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
            return View(part1);
        }

        // GET: Part1/Delete/5
     

        // POST: Part1/Delete/5
       // [HttpPost, ActionName("Delete")]
       [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var part1 = await _context.Part1s.FindAsync(id);
            _context.Part1s.Remove(part1);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Part1Exists(int id)
        {
            return _context.Part1s.Any(e => e.Id_part1 == id);
        }
    }
}
