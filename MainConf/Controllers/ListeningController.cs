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
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PhotoE = MainConf.Models.Listening.PhotoE;

namespace MainConf.Controllers
{
    [Authorize]
    public class ListeningController : Controller
    {
        private IWebHostEnvironment _hostingEnvironment;

        private UserManager<ApplicationUser> _userManager;
        private ListeningContext db;

        public ListeningController( ListeningContext context, UserManager<ApplicationUser> userManager, IWebHostEnvironment hostingEnvironment)
        {
             _hostingEnvironment = hostingEnvironment;
            _userManager = userManager;
            db = context;

        }
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        public IActionResult Photoinsert(string parameter1 , int parameter2)
        {
            var new1 = new Entermodel();
            new1.Lets = true;
            new1.Positions = " ";
            if (parameter2 != 7) { 
            Candidatesl exports = db.Candidatesl.FirstOrDefault(p => p.Pnfl == User.Identity.Name);
            ExamRooms exams = db.ExamRoom.FirstOrDefault(p => p.Can_id == exports.Id_candidate && p.Ended == 0);
            try
            {
                PhotoE photoE = db.PhotoE.FirstOrDefault(p => p.Exam_id == exams.Id_exam);
                if (photoE == null)
                {
                    PhotoE photoE1 = new Models.Listening.PhotoE { Part1 = parameter1, Exam_id = exams.Id_exam, Part2 = " ", Part3 = " ", Part4 = " ", Part5 = " ", Part6 = " " };
                    db.PhotoE.Add(photoE1);

                }
                else if (parameter2 == 1) {

                    photoE.Part1 = parameter1;
                    db.PhotoE.Update(photoE);
                    

                } else if (parameter2 == 2)
                {
                    photoE.Part2 = parameter1;
                    db.PhotoE.Update(photoE);
                }
                else if (parameter2 == 3)
                {
                    photoE.Part3 = parameter1;
                    db.PhotoE.Update(photoE);
                }
                else if (parameter2 == 4)
                {
                    photoE.Part4 = parameter1;
                    db.PhotoE.Update(photoE);
                }
                else if (parameter2 == 5)
                {
                    photoE.Part5 = parameter1;
                    db.PhotoE.Update(photoE);
                }
                else if (parameter2 == 6)
                {
                    photoE.Part6 = parameter1;
                    db.PhotoE.Update(photoE);
                }
                db.SaveChanges();
            } catch (Exception e) {
                new1.Lets = false;
                new1.Positions = e.ToString();
            }
            }

            return Json(new1);
        }

        
       
        public IActionResult InterListening()
        {
            return View();
        }
      
   
      
   
        public IActionResult GetStart()
        {
            Starts starts = db.Starts.FirstOrDefault();
            Start1 str = new Start1 {Starttime=starts.StartTime, Starttime2=DateTime.Now, Listening=starts.Listening, Reading=starts.Reading, Speaking=starts.Speaking, Writing=starts.Writing };
            return Json(str);
        }
        [Authorize(Roles = "Candidate")]
        [HttpGet]
        public IActionResult Gettimes()
        {
            Modeltime modeltime = new Modeltime();
            Candidatesl candidates = db.Candidatesl.First(p => p.Pnfl == User.Identity.Name);
            var exams = db.ExamRoom.FirstOrDefault(p => p.Can_id == candidates.Id_candidate);
            modeltime.Currenttime = exams.Currenttime;
            modeltime.Starttime = exams.Starttime2.AddSeconds(3);
            modeltime.Timers = exams.Currenttime.Subtract(exams.Starttime2.AddSeconds(3));
            exams.Starttime2 = DateTime.Now.Subtract(modeltime.Timers);
            exams.Currenttime = DateTime.Now;
            db.ExamRoom.Update(exams);
            db.SaveChanges();
            modeltime.Timenow = DateTime.Now;
            return Json(modeltime);
        }
        [Authorize(Roles = "Candidate")]
        [HttpPost]
        public IActionResult Setstarttime(DateTime parameter1)
        {
            Entermodel entermodel = new Entermodel();
            try {
                entermodel.Lets = true;
                Candidatesl candidates = db.Candidatesl.First(p => p.Pnfl == User.Identity.Name);
                var exams = db.ExamRoom.FirstOrDefault(p => p.Can_id == candidates.Id_candidate && p.Ended == 0);
                entermodel.Positions = exams.Starttime2.ToString("dd.mm.yyyy HH:MM:ss");
                exams.Starttime2 = parameter1;
                db.ExamRoom.Update(exams);
                db.SaveChanges();
                var exams2 = db.ExamRoom.FirstOrDefault(p => p.Can_id == candidates.Id_candidate && p.Ended == 0);
                entermodel.Positions = entermodel.Positions+" " + exams2.Starttime2.ToString("dd.mm.yyyy HH:MM:ss")+" "+ parameter1.ToString("dd.mm.yyyy HH:MM:ss");
            } catch (Exception e) {
                entermodel.Lets = false;
                entermodel.Positions = e.ToString();
            }
           
            return Json(entermodel);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Examing()
        {



            return View();
        }
   
        [Authorize(Roles = "Admin")]
        public IActionResult Startlive()
        {
            var query2 = from b in db.Candidatesl
                         from k in db.Languages
                         from c in db.ExamRoom
                         .Where(c => c.Can_id == b.Id_candidate && b.Lang_id == k.Id_language && c.Ended==0)
                         select new Listeninglive
                         {
                             Fio=b.Last_name+" "+b.First_name+" "+b.S_name,
                             Pnfl=b.Pnfl,
                             Language=k.Names_lan,
                             Exp1=c.Exp1,
                             Exp2=c.Exp2,
                             Exp3=c.Exp3,
                             Exp4=c.Exp4,
                             Exp5=c.Exp5,
                             Exp6=c.Exp6
                         };
            //query2 = query2.OrderBy(c => c.Dates);
            return Json(query2);

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Errorings(Erroparts erroparts)
        {
            if (erroparts.Pnfl!=null) {
                Candidatesl candidatesl = db.Candidatesl.FirstOrDefault(p=>p.Pnfl==erroparts.Pnfl);
                if (candidatesl != null) {
                    ExamRooms examRooms = db.ExamRoom.FirstOrDefault(p=>p.Can_id==candidatesl.Id_candidate);
                    if (examRooms != null) {
                        int a = 0;
                        if (erroparts.Part1 !=null) {
                            Erroring erroring = new Erroring { Canpnfl = erroparts.Pnfl, Created_by = User.Identity.Name, Created_time = DateTime.Now, Exam_id = examRooms.Id_exam, Part = 1, Reason = erroparts.Reason };
                            db.Erroring.Add(erroring);
                            a = 1;
                            examRooms.Exp1 = 0;
                        }
                        if (erroparts.Part2 != null)
                        {
                            Erroring erroring = new Erroring { Canpnfl = erroparts.Pnfl, Created_by = User.Identity.Name, Created_time = DateTime.Now, Exam_id = examRooms.Id_exam, Part = 2, Reason = erroparts.Reason };
                            db.Erroring.Add(erroring);
                            a = 1;
                            examRooms.Exp2 = 0;
                        }
                        if (erroparts.Part3 != null)
                        {
                            Erroring erroring = new Erroring { Canpnfl = erroparts.Pnfl, Created_by = User.Identity.Name, Created_time = DateTime.Now, Exam_id = examRooms.Id_exam, Part = 3, Reason = erroparts.Reason };
                            db.Erroring.Add(erroring);
                            a = 1;
                            examRooms.Exp3 = 0;
                        }
                        if (erroparts.Part4 != null)
                        {
                            Erroring erroring = new Erroring { Canpnfl = erroparts.Pnfl, Created_by = User.Identity.Name, Created_time = DateTime.Now, Exam_id = examRooms.Id_exam, Part = 4, Reason = erroparts.Reason };
                            db.Erroring.Add(erroring);
                            a = 1;
                            examRooms.Exp4 = 0;
                        }
                        if (erroparts.Part5 != null)
                        {
                            Erroring erroring = new Erroring { Canpnfl = erroparts.Pnfl, Created_by = User.Identity.Name, Created_time = DateTime.Now, Exam_id = examRooms.Id_exam, Part = 5, Reason = erroparts.Reason };
                            db.Erroring.Add(erroring);
                            a = 1;
                            examRooms.Exp5 = 0;
                        }
                        if (erroparts.Part6 != null)
                        {
                            Erroring erroring = new Erroring { Canpnfl = erroparts.Pnfl, Created_by = User.Identity.Name, Created_time = DateTime.Now, Exam_id = examRooms.Id_exam, Part = 6, Reason = erroparts.Reason };
                            db.Erroring.Add(erroring);
                            a = 1;
                            examRooms.Exp6 = 0;
                        }
                        if (a == 1) {
                            examRooms.Ended = 0;
                            db.ExamRoom.Update(examRooms);
                            db.SaveChanges();
                        }
                    }
                }
            }

            return View("../Exports/Waited");

        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult ControlingL()
        {
            Loglistening logreading = db.Loglistening.FirstOrDefault(p => p.Admin_id == User.Identity.Name && p.Ended == 0);
            if (logreading == null)
            {
                return View("../Exports/Waited");
            }
            else
                return View(logreading);
        }
       
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Getmessages(string pnfl)
        {
            Candidatesl candidatesr = db.Candidatesl.FirstOrDefault(p => p.Pnfl == pnfl);
            ExamRooms examRoomr = db.ExamRoom.FirstOrDefault(p => p.Can_id == candidatesr.Id_candidate);
            var warnig = db.Warnings.Where(p => p.Exam_id == examRoomr.Id_exam);

            return Json(warnig);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Getexamid()
        {
            Onloadmodel entermodel = new Onloadmodel();
            try
            {

                Loglistening logreading = db.Loglistening.FirstOrDefault(p => p.Admin_id == User.Identity.Name && p.Ended == 0);
                entermodel.Video = "0";
                entermodel.Lets = true;
                Candidatesl candidatesr = db.Candidatesl.FirstOrDefault(p => p.Pnfl == logreading.Cand_id);
                ExamRooms examRoomr = db.ExamRoom.FirstOrDefault(p => p.Can_id == candidatesr.Id_candidate);
                entermodel.Positions = examRoomr.Id_exam.ToString();
            }
            catch (Exception e)
            {
                entermodel.Positions = e.ToString();
                entermodel.Lets = false;
            }


            return Json(entermodel);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Endedview()
        {
            Onloadmodel entermodel = new Onloadmodel();
            try
            {

                Loglistening logreading = db.Loglistening.FirstOrDefault(p => p.Admin_id == User.Identity.Name && p.Ended == 0);
                logreading.Endedtime = DateTime.Now;
                logreading.Ended = 1;
                db.Loglistening.Update(logreading);
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
        [HttpGet]
        public IActionResult Showimages(string pnfl)
        {
            var candidate = db.Candidatesl.FirstOrDefault(p=>p.Pnfl==pnfl);
           var exam = db.ExamRoom.FirstOrDefault(p=>p.Can_id==candidate.Id_candidate);
            var photos = db.PhotoE.FirstOrDefault(p=>p.Exam_id==exam.Id_exam);
            var lists = db.Controlling.Where(p=>p.Pnfl==pnfl).ToList();
            var logs= new Showlogs {Fio=candidate.Last_name+' '+candidate.First_name+' '+candidate.S_name, Pnfl=pnfl, Dateing=exam.Starttime, Photo=photos, Controllings=lists };

            return View(logs);
        }

    }
}
