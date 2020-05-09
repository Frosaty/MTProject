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
    public class Cours1Controller : Controller
    {
        private Manage_TrainingEntities7 db = new Manage_TrainingEntities7();

        // GET: Cours1
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Trainee_Course).Include(c => c.CourseCategory1);
            return View(courses.ToList());
        }

        // GET: Cours1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: Cours1/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id");
            ViewBag.CategoryId = new SelectList(db.CourseCategories, "Id", "CategName");
            return View();
        }

        // POST: Cours1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CourseName,Description,CategoryId,CreateAt")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id", cours.Id);
            ViewBag.CategoryId = new SelectList(db.CourseCategories, "Id", "CategName", cours.CategoryId);
            return View(cours);
        }

        // GET: Cours1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id", cours.Id);
            ViewBag.CategoryId = new SelectList(db.CourseCategories, "Id", "CategName", cours.CategoryId);
            return View(cours);
        }

        // POST: Cours1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CourseName,Description,CategoryId,CreateAt")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id", cours.Id);
            ViewBag.CategoryId = new SelectList(db.CourseCategories, "Id", "CategName", cours.CategoryId);
            return View(cours);
        }

        // GET: Cours1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
            db.SaveChanges();
            return RedirectToAction("Index");
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
