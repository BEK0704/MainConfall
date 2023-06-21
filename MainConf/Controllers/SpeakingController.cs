using MainConf.Models;
using MainConf.Models.Main;
using MainConf.Models.Reading;
using MainConf.Models.Seconds;
using MainConf.Models.Seondlistening;
using MainConf.Models.Speaking;
using MainConf.Models.Writing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MainConf.Controllers
{
    [Authorize]
    public class SpeakingController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        private UserManager<ApplicationUser> _userManager;
        private SpeakingContext db;
        public SpeakingController(SpeakingContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            db = context;
        }

 
            public string hashing(string a) {
            byte[] hash;
            string result = "21";
            using (MD5 md5 = MD5.Create())
            {
                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(a));
                result = BitConverter.ToString(hash).Replace("-", string.Empty);
            }
            return result;
        }
    
        public IActionResult Erroring(string pnfl)
        {
            var can = db.Candidatess.FirstOrDefault(p=>p.Pnfl==pnfl);
            var exam = db.Examings.FirstOrDefault(p=>p.Can_id==can.Id_candidate);
            exam.End = 2;
            db.Examings.Update(exam);

            return View("../Exports/Waited");

        }
        public IActionResult EnterSpeaking()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        
        public IActionResult ShowimagesS(string pnfl)
        {
            var candidate = db.Candidatess.FirstOrDefault(p => p.Pnfl == pnfl);
            var exam = db.Examings.FirstOrDefault(p => p.Can_id == candidate.Id_candidate);
            var photos = db.PhotoS.FirstOrDefault(p => p.Exam_id == exam.Id);
            var logs = new Logspeaking { Fio = candidate.Last_name + ' ' + candidate.First_name + ' ' + candidate.S_name, Pnfl = pnfl, Dateing = exam.Starttime, Photo = photos };

            return View(logs);
        }

    }
}
