
using MainConf.Models;
using MainConf.Models.Main;
using MainConf.Models.Seconds;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Localization;
using MainConf.Models.Writing;
using Microsoft.EntityFrameworkCore;
using MainConf.TagHelpers;
using MainConf.TagHelpers.Filtering;

namespace MainConf.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;

        private UserManager<ApplicationUser> _userManager;
        private DataContext db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, DataContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            db = context;
            _logger = logger;

        }

      

        public IActionResult Checking() {
            return View();
        }
      
   


 
  
        public IActionResult Privacy()
        {
            if (User.IsInRole("Admin"))
            {
                /*  Admins admins = db.Admins.FirstOrDefault(p => p.Pnfl == User.Identity.Name);
                  if (admins.Photos.Length < 60) admins.Photos = "0";
                  else
                  {

                      var base64 = admins.Photos;
                      ViewBag.imagesrc = string.Format("data:image/png;base64,{0}", base64);
                      ViewBag.imagelegth = base64.Length;
                  }
                  indexHome = new IndexHome { Id = admins.Id_admin, First_name = admins.First_name, Last_name = admins.Last_name, S_name = admins.S_name, Language = "No", Level = "No", Phones = admins.Phones, Photos = admins.Photos, Pnfl = admins.Pnfl };*/
                return View("../Exports/Waited");

            }
            else { return View(); }
            
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Report()
        {
         
            Listlocation();
            return View();
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
            var querry = from c in db.Languages
                         from l in db.Levels
                         .Where(p => p.Lang_id == c.Id_language)
                         select new SelectionOption
                         {
                             Lan = c.Names_lan,
                             Lev=l.Names_level,
                             Lev_id=l.Id_level
                         };
            foreach (var item in querry)
            {
               
                SelectListItem sel = new SelectListItem
                {
                    Text = item.Lan+"-"+item.Lev,
                    Value = item.Lev_id.ToString()
                };
                SelectListItem s = sel;
                items.Add(s);

                
            }
            ViewData["Location_id"] = items;
            List<SelectListItem> items1 = new List<SelectListItem>();
            SelectListItem sel21 = new SelectListItem
            {
                Text = "Barchasi",
                Value = "0"
            };
            items1.Add(sel21);
            var querry1 = db.Viloyat;
            foreach (var item in querry1)
            {

                SelectListItem sel = new SelectListItem
                {
                    Text = item.Names_V,
                    Value = item.Id_viloyat.ToString()
                };
                SelectListItem s = sel;
                items1.Add(s);


            }
            ViewData["Language_id"] = items1;
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Konfigure()
        {
            var query = db.Konfig.OrderBy(p=>p.Id_konf);
            return View(query);
        }
       
        public IActionResult IpCreate()
        {

            Listlocationip();
            return View();
        }
       public void Listlocationip()
        {
            
            List<SelectListItem> items1 = new List<SelectListItem>();
          
            var querry1 = db.Viloyat;
            foreach (var item in querry1)
            {

                SelectListItem sel = new SelectListItem
                {
                    Text = item.Names_V,
                    Value = item.Id_viloyat.ToString()
                };
                SelectListItem s = sel;
                items1.Add(s);


            }
            ViewData["Region_id"] = items1;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> EditIp(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            var part1 = await db.Ipconfig.FindAsync(id);
            if (part1 == null)
            {
                return NotFound();
            }
            Listlocationip();
            return View(part1);
        }
       
       

    }
}
