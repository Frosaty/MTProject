using MTProject.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace MTProject.Controllers
{
    public class AccountController : Controller
    {
        Manage_TrainingEntities8 db = new Manage_TrainingEntities8();
        public ActionResult Login()
        {

            return View();
            ViewBag.Message = "";
        }
        [HttpPost]
        public ActionResult Login(Account account)
        {
            Manage_TrainingEntities8 db = new Manage_TrainingEntities8();
            var dataItem = db.Accounts.Where(x => x.UserName == account.UserName && x.Password == account.Password).ToList();
            if (dataItem.Count() > 0)
            {
                Session["LoginID"] = dataItem[0].Id;
                FormsAuthentication.SetAuthCookie(dataItem[0].UserName, false);
                //admin
                if (dataItem[0].Role == 1)
                {
                    return RedirectToAction("../Admin/Index");
                }
                //staff
                if (dataItem[0].Role == 2)
                {
                    return RedirectToAction("../Staff/Index");
                }
                //Trainer
                if (dataItem[0].Role == 3)
                {
                    return RedirectToAction("../Trainers/Index");
                }

                //Trainee
                if (dataItem[0].Role == 4)
                {
                    return RedirectToAction("../Trainees/Index");
                }
            } 
            else
            {
                ViewBag.Message = "Incorrect Username or Password!";
            }
            return View(account);
        }

        [Authorize]
        public ActionResult SignOut()
        {
            Session["LoginID"] = 0;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}