using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using MTProject.Models;

namespace MTProject.Controllers
{
    public class ManageController : Controller
    {
        Manage_TrainingEntities7 db = new Manage_TrainingEntities7();
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TraineeRegistration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TraineeRegistration(Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account();
                account.UserName = trainee.userName;
                account.Password = trainee.password;
                account.Role = 4;

                db.Accounts.Add(account);
                db.SaveChanges();

                var t = new Trainee{ 
                AccountId = db.Accounts.Max(a => a.Id),
                Department = trainee.Department,
                DoB = trainee.DoB,
                Education = trainee.Education,
                Experience = trainee.Experience,
                FullName = trainee.FullName,
                Location = trainee.Location,
                ProLanguage = trainee.ProLanguage,
                ToeicScore = trainee.ToeicScore
            };

                db.Trainees.Add(t);

                db.SaveChanges();
                return RedirectToAction("TraineeRegistration");
            }
            return View(trainee);
        }
    }
}