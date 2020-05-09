using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MTProject.Models;

namespace MTProject.Controllers
{
    public class TraineesController : Controller
    {
        private Manage_TrainingEntities8 db = new Manage_TrainingEntities8();

        // GET: Trainees
        public ActionResult Index()
        {
            int id = Convert.ToInt32(Session["LoginID"]);
            Trainee trainee = db.Trainees.Where(t => t.Account.Id == id).FirstOrDefault();
            ICollection<Trainee_Course> cours = db.Trainee_Course.Where(t => t.TraineeId == trainee.Id).Include(t => t.Cours).ToList();
            return View(cours);
        }

        // GET: Trainees/Edit/5
        public ActionResult Edit()
        {
            int id = Convert.ToInt32(Session["LoginID"]);
            Trainee trainee = db.Trainees.Where(t => t.Account.Id == id).FirstOrDefault();
            if (trainee == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts,"Id", "UserName",trainee.AccountId);
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Account,AccountId,FullName,DoB,Education,ProLanguage,ToeicScore,Experience,Location,Department")] Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainee).State = EntityState.Modified;
                db.Entry(trainee.Account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "UserName", trainee.AccountId);
            return View(trainee);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
