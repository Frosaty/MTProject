using MTProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MTProject.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        Manage_TrainingEntities7 db = new Manage_TrainingEntities7();

        // GET: Admin
        public ActionResult Index()
        {
            var accounts = db.Accounts.Include(a => a.Role1);
            //return View(accounts.ToList());
            return View();
        }

        //get account
        public ActionResult ManageAccount()
        {
            return View(db.Accounts.ToList());
        }
        // GET: Accounts

        // GET: Accounts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // GET: Accounts/Create
        public ActionResult Create()
        {
            ViewBag.Message = "";
            if (TempData["Message"] != null)
            {
                ViewBag.Message = "Username exist!";
            }
            ViewBag.Id = new SelectList(db.Accounts, "Id", "UserName");
            ViewBag.Id = new SelectList(db.Accounts, "Id", "UserName");
            ViewBag.Role = new SelectList(db.Roles, "Id", "RoleName");
            return View();
        }

        // POST: Accounts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserName,Password,Role")] Account account)
        {
            Account account1 = db.Accounts.Where(a => a.UserName == account.UserName).FirstOrDefault();
            
            if (account1 != null)
            {
                TempData["Message"] = "Username exist!";
                return RedirectToAction("Create");
            }
            if (ModelState.IsValid)
            {
                db.Accounts.Add(account);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Accounts, "Id", "UserName", account.Id);
            ViewBag.Id = new SelectList(db.Accounts, "Id", "UserName", account.Id);
            ViewBag.Role = new SelectList(db.Roles, "Id", "RoleName", account.Role);
            return View(account);
        }

        // GET: Accounts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Accounts, "Id", "UserName", account.Id);
            ViewBag.Id = new SelectList(db.Accounts, "Id", "UserName", account.Id);
            ViewBag.Role = new SelectList(db.Roles, "Id", "RoleName", account.Role);
            return View(account);
        }

        // POST: Accounts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserName,Password,Role")] Account account)
        {
            if (ModelState.IsValid)
            {
                db.Entry(account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Accounts, "Id", "UserName", account.Id);
            ViewBag.Id = new SelectList(db.Accounts, "Id", "UserName", account.Id);
            ViewBag.Role = new SelectList(db.Roles, "Id", "RoleName", account.Role);
            return View(account);
        }

        // GET: Accounts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Account account = db.Accounts.Find(id);
            if (account == null)
            {
                return HttpNotFound();
            }
            return View(account);
        }

        // POST: Accounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Account account = db.Accounts.Find(id);
            db.Accounts.Remove(account);
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