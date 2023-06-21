using MainConf.Models;
using MainConf.Models.Main;
using MainConf.Models.Seconds;
using MainConf.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace MainConf.Controllers
{
    
    public class AccountController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly DataContext _context;
        public AccountController(DataContext context, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IWebHostEnvironment hostingEnvironment)
        {

            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }
        [Authorize]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
               // 
                ApplicationUser user = new ApplicationUser { PNFL=model.Pnfl, UserName = model.Pnfl, Position = model.Position, Is_active="0" };
                // добавляем пользователя
                model.Password = model.Pnfl;
                var result = await _userManager.CreateAsync(user, model.Pnfl);
                if (result.Succeeded)
                {
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Privacy", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        public async Task<IActionResult> Entering1(string parameter1) {
            var ret = new Entermodel();
            ApplicationUser user = await _userManager.FindByNameAsync(parameter1);
            if (user == null)
            {
                
                ret.Lets = false;
                ret.Positions = "";

            }
            else {
                ret.Lets = true;
                ret.Positions = user.Position;
            }
            return Json(ret);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {


                /*   var result =
                       await _signInManager.PasswordSignInAsync(model.Pnfl, model.Pnfl, false, false);
                   if (result.Succeeded)
                   {
                       // проверяем, принадлежит ли URL приложению
                       if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                       {
                           return Redirect(model.ReturnUrl);
                       }
                       else
                       {
                           return RedirectToAction("Index", "Home");
                       }
                   }
                   else
                   {
                       ModelState.AddModelError("", "Неправильный ПНФЛ");
                   }*/
                string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "face\\data\\" + model.Pnfl);
                string uploads1 = Path.Combine(_hostingEnvironment.WebRootPath, "face\\data1\\" + model.Pnfl);
                if (Directory.Exists(uploads))
                {
                    //ApplicationUser user = await _userManager.FindByNameAsync(model.Pnfl);
                    model.ReturnUrl = "data";
                    return View("Face", model);
                } else
                 if (Directory.Exists(uploads1))
                {
                    //ApplicationUser user = await _userManager.FindByNameAsync(model.Pnfl);
                    model.ReturnUrl = "data1";
                    return View("Face", model);
                }
                else {
                    ModelState.AddModelError("", "Неправильный ПНФЛ");
                }
                

            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Sendmes(string parameter1)
        {
            var ret = new EnterModel1();
            ret.Positions = parameter1;
            ret.Tol = "";

            /*
                var result =
                        await _signInManager.PasswordSignInAsync(parameter1, parameter1, false, false);
                if (result.Succeeded)
                {
                    ret.Lets = true;


                    // проверяем, принадлежит ли URL приложению


                    return Json(ret);

                }


       */
            LoginViewModel model = new LoginViewModel {Pnfl=parameter1, ReturnUrl="welcome" };
            string uploads = Path.Combine(_hostingEnvironment.WebRootPath, "face\\data\\" + model.Pnfl);
            string uploads1 = Path.Combine(_hostingEnvironment.WebRootPath, "face\\data1\\" + model.Pnfl);
            if (Directory.Exists(uploads))
            {
                //ApplicationUser user = await _userManager.FindByNameAsync(parameter1);
                ret.Lets = true;
                ret.Tol = "data";
                return Json(ret);

            }
            else
                 if (Directory.Exists(uploads1))
            {
                //ApplicationUser user = await _userManager.FindByNameAsync(model.Pnfl);
                ret.Lets = true;
                ret.Tol = "data1";
                return Json(ret);
            }

            ret.Lets = false;
            return Json(ret);

        }
      
        

     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


    }
}
