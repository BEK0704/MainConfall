using MainConf.Models;
using MainConf.Models.Listening;
using MainConf.Models.Main;
using MainConf.Models.Reading;
using MainConf.Models.SecondReading;
using MainConf.Models.Seconds;
using MainConf.Models.Seondlistening;
using MainConf.Models.Writing;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MainConf.Controllers
{
    public class ReadingController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;

        private UserManager<ApplicationUser> _userManager;
        private ReadingContext db;
        private DataContext context1;

        public ReadingController(ReadingContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment, DataContext _context)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            db = context;
            context1 = _context;
        }
        // GET: ReadingController
        public ActionResult Enterreading()
        {
            return View();
        }
       
        // GET: ReadingController/Details/5

       

        public ActionResult Details(int id)
        {
            return View();
        }

        
   
        public IActionResult Readingview(string pnfl)
        {
            Entermodel entermodel = new Entermodel();
            try
            {
                var log = db.Logreading.FirstOrDefault(p => p.Admin_id == User.Identity.Name && p.Ended == 0);
                if (log != null)
                {
                    log.Ended = 1;
                    log.Endedtime = DateTime.Now;
                    db.Logreading.Update(log);
                }
                Candidatesr candidatesr = db.Candidatesr.FirstOrDefault(p => p.Pnfl == pnfl);
                ExamRoomr examRoomr = db.ExamRoomr.FirstOrDefault(p => p.Ended == 0 && p.Can_id == candidatesr.Id_candidate);
                Logreading logreading = new Logreading { Admin_id = User.Identity.Name, Cand_id = candidatesr.Pnfl, Ended = 0, Endedtime = DateTime.Now, Startedtime = DateTime.Now, Warnings = 0 };
                db.Logreading.Add(logreading);
                db.SaveChanges();
                entermodel.Lets = true;
            }
            catch (Exception e)
            {
                entermodel.Lets = false;
                entermodel.Positions = e.ToString();
            }



            return Json(entermodel);

        }

     
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Getinfo(string pnfl)
        {
            Inforeading entermodel = new Inforeading();
            try {
                var candidates = context1.Candidates.FirstOrDefault(p=>p.Pnfl==pnfl); 
                var facelog = context1.Facelog.Where(p => p.Pnfl == candidates.Pnfl && p.Entered == 1).OrderByDescending(p => p.Created_time).FirstOrDefault();
                var lan = context1.Languages.FirstOrDefault(p=>p.Id_language==candidates.Lang_id);
                entermodel.Fullname = candidates.Last_name + ' ' + candidates.First_name + " " + candidates.S_name;
                entermodel.Image1 = "data:image/jpeg;base64," + candidates.Photos;
                entermodel.Language = lan.Names_lan;
                entermodel.Image2 = facelog.Images;
                entermodel.Datetime= facelog.Created_time.ToString("dd/MM/yyyy HH:mm:ss");
                double a1 = Convert.ToDouble(facelog.Tol.Replace('.', ','));
                double a2 = (1 - a1) * 100;
                entermodel.Koef=a2.ToString()+ " %";
               
                entermodel.Lets = true;
            } catch (Exception e) {
                entermodel.Lets = false;
                entermodel.Image1 = e.ToString();
            }


            return Json(entermodel);
        }
      [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Getmessages(string pnfl)
        {
            Candidatesr candidatesr = db.Candidatesr.FirstOrDefault(p=>p.Pnfl==pnfl);
            ExamRoomr examRoomr = db.ExamRoomr.FirstOrDefault(p=>p.Can_id==candidatesr.Id_candidate);
            var warnig = db.Warnings.Where(p=>p.Exam_id==examRoomr.Id_exam);

            return Json(warnig);
        }

        // GET: ReadingController/Delete/5
        public ActionResult Streaming()
        {
            return View();
        }
        public ActionResult Streaming2()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Getcount(string pnfl)
        {
            Onloadmodel entermodel = new Onloadmodel();
            try
            {

                entermodel.Positions = db.Controlling.Where(p=>p.Pnfl==pnfl && p.Typeerror==1).Count().ToString();
                entermodel.Video = db.Controlling.Where(p => p.Pnfl == pnfl && p.Typeerror == 2).Count().ToString();
                entermodel.Lets = true;
            }
            catch (Exception e)
            {
                entermodel.Positions = e.ToString();
                entermodel.Lets = false;
            }


            return Json(entermodel);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ShowimagesR(string pnfl)
        {
            var candidate = db.Candidatesr.FirstOrDefault(p => p.Pnfl == pnfl);
            var exam = db.ExamRoomr.FirstOrDefault(p => p.Can_id == candidate.Id_candidate);
            var photos = db.PhotoR.FirstOrDefault(p => p.Exam_id == exam.Id_exam);
            var lists = db.Controlling.Where(p => p.Pnfl == pnfl).ToList();
            var logs = new Logimages { Fio = candidate.Last_name + ' ' + candidate.First_name + ' ' + candidate.S_name, Pnfl = pnfl, Dateing = exam.Starttime, Photo = photos, Controllings = lists };

            return View(logs);
        }


    }
}
