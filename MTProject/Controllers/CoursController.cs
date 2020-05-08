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
    public class CoursController : Controller
    {
        private Manage_TrainingEntities7 db = new Manage_TrainingEntities7();

        // GET: Cours
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.CourseCategory1).Include(c => c.Trainee_Course);
            return View(courses.ToList());
        }

        // GET: Cours/Details/5
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

        // GET: Cours/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.CourseCategories, "Id", "CategName");
            //ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id");
            return View();
        }

        // POST: Cours/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseName,Description,CreateAt")] Cours cours, int CategoryId)
        {
            if (ModelState.IsValid)
            {
                CourseCategory category = db.CourseCategories.Find(CategoryId);
                Cours course = new Cours();

                course.CourseCategory1 = category;

                course.CourseName = cours.CourseName;
                course.Description = cours.Description;
                course.CreateAt = cours.CreateAt;
                course.CategoryId = CategoryId;

                db.Courses.Add(course);


                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.CourseCategories, "Id", "CategName", cours.Id);
            ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id", cours.Id);
            return View(cours);
        }

        // GET: Cours/Edit/5
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
            ViewBag.Id = new SelectList(db.CourseCategories, "Id", "CategName", cours.Id);
            ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id", cours.Id);
            return View(cours);
        }

        // POST: Cours/Edit/5
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
            ViewBag.Id = new SelectList(db.CourseCategories, "Id", "CategName", cours.Id);
            ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id", cours.Id);
            return View(cours);
        }

        // GET: Cours/Delete/5
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

        // POST: Cours/Delete/5
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
