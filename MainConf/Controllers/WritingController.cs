using MainConf.Models;
using MainConf.Models.Listening;
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
using System.Threading.Tasks;

namespace MainConf.Controllers
{
    [Authorize]
    public class WritingController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;
        private UserManager<ApplicationUser> _userManager;
        private WritingContext db;
        public WritingController(WritingContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            db = context;
        }

        // GET: WritingController
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Enterwriting()
        {
            return View();
        }
    
        
        public ActionResult Create()
        {
            return View();
        }

        // POST: WritingController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
       
       
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Errorings(string pnfl, string reason)
        {
            Onloadmodel entermodel = new Onloadmodel();
            entermodel.Video = "0";
            entermodel.Lets = true;
            Candidatesr candidatesr = db.Candidatesw.FirstOrDefault(p => p.Pnfl == pnfl);
            var exams = db.Exams.FirstOrDefault(p => p.Cand_id == candidatesr.Id_candidate);
            exams.Ended = 2;
            Erroring erroring = new Erroring { Canpnfl = pnfl, Created_by = User.Identity.Name, Created_time = DateTime.Now, Exam_id = exams.Id, Part = 1, Reason = reason };
            db.Erroring.Add(erroring);
            db.Exams.Update(exams);
            db.SaveChanges();
            return Json(entermodel);
        }
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        public IActionResult Checkstream(int new1, int last)
        {
            Onloadmodel entermodel = new Onloadmodel();
            try
            {
                var candidatesr = db.Candidatesw.FirstOrDefault(p => p.Pnfl == User.Identity.Name);
                var examRoomr = db.Exams.FirstOrDefault(p => p.Ended == 0 && p.Cand_id == candidatesr.Id_candidate);
                Logreading logreading = db.Logwriting.FirstOrDefault(p => p.Ended == 0 && p.Cand_id == User.Identity.Name);
                if (logreading == null)
                {
                    entermodel.Video = "0";
                }
                else
                {
                    entermodel.Video = examRoomr.Id.ToString();
                }

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
        [HttpPost]
        public IActionResult Writingview(string pnfl)
        {
            Entermodel entermodel = new Entermodel();
            try
            {
                var log = db.Logwriting.FirstOrDefault(p => p.Admin_id == User.Identity.Name && p.Ended == 0);
                if (log != null)
                {
                    log.Ended = 1;
                    log.Endedtime = DateTime.Now;
                    db.Logwriting.Update(log);
                }
                Candidatesr candidatesr = db.Candidatesw.FirstOrDefault(p => p.Pnfl == pnfl);
                Examwriting examRoomr = db.Exams.FirstOrDefault(p => p.Ended == 0 && p.Cand_id == candidatesr.Id_candidate);
                Logreading logreading = new Logreading { Admin_id = User.Identity.Name, Cand_id = candidatesr.Pnfl, Ended = 0, Endedtime = DateTime.Now, Startedtime = DateTime.Now, Warnings = 0 };
                db.Logwriting.Add(logreading);
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
        public ActionResult ControlingW()
        {
            Logreading logreading = db.Logwriting.FirstOrDefault(p => p.Admin_id == User.Identity.Name && p.Ended == 0);
            if (logreading == null)
            {
                return View("../Exports/Waited");
            }
            else
                return View(logreading);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Getcount(string pnfl)
        {
            Onloadmodel entermodel = new Onloadmodel();
            try
            {

                entermodel.Positions = db.Controlling.Where(p => p.Pnfl == pnfl && p.Typeerror == 1).Count().ToString();
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
        [HttpPost]
        public IActionResult Endedview()
        {
            Onloadmodel entermodel = new Onloadmodel();
            try
            {

                Logreading logreading = db.Logwriting.FirstOrDefault(p => p.Admin_id == User.Identity.Name && p.Ended == 0);
                logreading.Endedtime = DateTime.Now;
                logreading.Ended = 1;
                db.Logwriting.Update(logreading);
                db.SaveChanges();
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
        [HttpPost]
        public IActionResult Getexamid()
        {
            Onloadmodel entermodel = new Onloadmodel();
            try
            {

                Logreading logreading = db.Logwriting.FirstOrDefault(p => p.Admin_id == User.Identity.Name && p.Ended == 0);
                entermodel.Video = "0";
                entermodel.Lets = true;
                Candidatesr candidatesr = db.Candidatesw.FirstOrDefault(p => p.Pnfl == logreading.Cand_id);
                Examwriting examRoomr = db.Exams.FirstOrDefault(p => p.Cand_id == candidatesr.Id_candidate);
                entermodel.Positions = examRoomr.Id.ToString();
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
        public IActionResult ShowimagesW(string pnfl)
        {
            var candidate = db.Candidatesw.FirstOrDefault(p => p.Pnfl == pnfl);
            var exam = db.Exams.FirstOrDefault(p => p.Cand_id == candidate.Id_candidate);
            var photos = db.PhotoW.FirstOrDefault(p => p.Exam_id == exam.Id);
            var lists = db.Controlling.Where(p => p.Pnfl == pnfl).ToList();
            var logs = new Logwriting { Fio = candidate.Last_name + ' ' + candidate.First_name + ' ' + candidate.S_name, Pnfl = pnfl, Dateing = exam.Started_time, Photo = photos, Controllings = lists };

            return View(logs);
        }
        public IActionResult Posterror(int type, string photo)
        {
            Entermodel entermodel = new Entermodel();
            entermodel.Lets = true;
            try
            {
                Controlling controlling = new Controlling { Photoer = photo, Pnfl = User.Identity.Name, Errortime = DateTime.Now, Status = 1, Typeerror = type };
                db.Controlling.Add(controlling);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                entermodel.Lets = false;
                entermodel.Positions = e.ToString();
            }
            return Json(entermodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: WritingController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WritingController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
