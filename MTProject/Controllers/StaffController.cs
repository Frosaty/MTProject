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
    [Authorize]
    public class StaffController : Controller
    {
        private Manage_TrainingEntities7 db = new Manage_TrainingEntities7();
        // GET: Staff
        public ActionResult Index()
        {
            return View();
        }
        //Authorize for Staff page
        public ActionResult ManageTrainee()
        {
            var trainees = db.Trainees.Include(t => t.Account);
            return View(trainees.ToList());
        }
        [HttpPost]
        public ActionResult CreateTrainee(Trainee trainee)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account();
                account.UserName = trainee.userName;
                account.Password = trainee.password;
                account.Role = 4;

                db.Accounts.Add(account);
                db.SaveChanges();

                var t = new Trainee
                {
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
                return RedirectToAction("ManageTrainee");
            }
            return View(trainee);
        }
        // GET: Trainees/Create
        public ActionResult CreateTrainee()
        {
            ViewBag.Message = "";
            if (TempData["Message"] != null)
            {
                ViewBag.Message = "Username exist!";
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "UserName");
            return View();
        }

        // POST: Trainees/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccountId,FullName,DoB,Education,ProLanguage,ToeicScore,Experience,Location,Department")] Trainee trainee)
        {
            Account account1 = db.Accounts.Where(a => a.UserName == trainee.userName).FirstOrDefault();

            if (account1 != null)
            {
                TempData["Message"] = "Username exist!";
                return RedirectToAction("Create");
            }

            if (ModelState.IsValid)
            {
                db.Trainees.Add(trainee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "UserName", trainee.AccountId);
            return View(trainee);
        }

        // GET: Trainees/Edit/5
        public ActionResult EditTrainee(int? id)
        {
            ViewBag.Message = "";
            if (TempData["Message"] != null)
            {
                ViewBag.Message = "Username exist!";
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Trainee trainee = db.Trainees.Find(id);
            Trainee trainee = db.Trainees.Where(tr => tr.Id == id).Include(tr => tr.Account).FirstOrDefault();
            if (trainee == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "UserName", trainee.AccountId);
            return View(trainee);
        }

        // POST: Trainees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //UserName, Password,
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrainee([Bind(Include = "Id,Account,AccountId,FullName,DoB,Education,ProLanguage,ToeicScore,Experience,Location,Department")] Trainee trainee)
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

        // GET: Trainees/Delete/5
        public ActionResult DeleteTrainee(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainee trainee = db.Trainees.Find(id);
            if (trainee == null)
            {
                return HttpNotFound();
            }
            return View(trainee);
        }

        // POST: Trainees/Delete/5
        [HttpPost, ActionName("DeleteTrainee")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trainee trainee = db.Trainees.Find(id);
            Account account = db.Accounts.Find(trainee.Account.Id);
            db.Accounts.Remove(account);
            db.Trainees.Remove(trainee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}


        /*-----------------------------------Trainer Area----------------------------------------------- */
        public ActionResult ManageTrainer()
        {
            var trainers = db.Trainers.Include(t => t.Account).ToList();
            return View(trainers);
        }
        // GET: Trainers/Details/5
        public ActionResult DetailsTrainer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }
        [HttpPost]
        public ActionResult CreateTrainer(Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                Account account = new Account();
                account.UserName = trainer.userName;
                account.Password = trainer.password;
                account.Role = 3;

                db.Accounts.Add(account);
                db.SaveChanges();

                var trer = new Trainer
                {
                    AccountId = db.Accounts.Max(a => a.Id),
                    FullName = trainer.FullName,
                    Address = trainer.Address,
                    Education = trainer.Education,
                    Email = trainer.Email,
                    Telephone = trainer.Telephone,
                    Types = trainer.Types,
                    WorkingPlace = trainer.WorkingPlace
                };

                db.Trainers.Add(trer);

                db.SaveChanges();
                return RedirectToAction("ManageTrainer");
            }
            return View(trainer);
        }

            // GET: Trainers/Create
            public ActionResult CreateTrainer()
        {
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "UserName");
            return View();
        }

        // POST: Trainers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTrainerConfirmed([Bind(Include = "Id,Account,FullName,Telephone,Address,Email,Types,Education,WorkingPlace,AccountId")] Trainer trainer)
        {
            Account account1 = db.Accounts.Where(a => a.UserName == trainer.userName).FirstOrDefault();

            if (account1 != null)
            {
                TempData["Message"] = "Username exist!";
                return RedirectToAction("Create");
            }

            if (ModelState.IsValid)
            {
                db.Trainers.Add(trainer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "UserName", trainer.AccountId);
            return View(trainer);
        }

        // GET: Trainers/Edit/5
        public ActionResult EditTrainer(int? id)
        {
            ViewBag.Message = "";
            if (TempData["Message"] != null)
            {
                ViewBag.Message = "Username exist!";
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Trainer trainer = db.Trainers.Find(id);
            Trainer trainer = db.Trainers.Where(er => er.Id == id).Include(er => er.Account).FirstOrDefault();
            if (trainer == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Accounts, "Id", "UserName", trainer.AccountId);
            return View(trainer);
        }

        // POST: Trainers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrainer([Bind(Include = "Id,Account,FullName,Telephone,Address,Email,Types,Education,WorkingPlace,AccountId")] Trainer trainer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trainer).State = EntityState.Modified;
                db.Entry(trainer.Account).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ManageTrainer");
            }
            //ViewBag.AccountId = new SelectList(db.Accounts, "Id", "UserName", trainer.AccountId);
            return View(trainer);
        }

        // GET: Trainers/Delete/5
        public ActionResult DeleteTrainer(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trainer trainer = db.Trainers.Find(id);
            if (trainer == null)
            {
                return HttpNotFound();
            }
            return View(trainer);
        }

        // POST: Trainers/Delete/5
        [HttpPost, ActionName("DeleteTrainer")]
        [ValidateAntiForgeryToken]
        //public ActionResult Assign (int TopicsId, int Trainers)
        //{
        //    Trainer trainer = db.Trainers.Find(Trainers);
        //    Topic topic = db.Topics.Find(TopicsId);
        //    if (Trainers != null)
        //    {
        //        trainer.Trainer_Topics.Add(trainer);
        //        topic.
        //    }
        //}
        //public ActionResult DeleteConfirmedTrainer(int trainers, int TopicsId)
        public ActionResult DeleteConfirmedTrainer(int trainers)
        {
            Trainer trainer = db.Trainers.Find(trainers);
            Account account = db.Accounts.Find(trainer.Account.Id);
            //Topic topic = db.Topics.Find(TopicsId);

            //db.Topics.Remove(topic);
            db.Accounts.Remove(account);
            db.Trainers.Remove(trainer);
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
        /*-----------------------------------End of Trainer----------------------------------------------- */


        /*-----------------------------------Start Topics----------------------------------------------- */
        public ActionResult Topics(string searching)
        {
            return View(db.Topics.Where(x => x.TopicName.Contains(searching) || searching == null).ToList());
            //return View(db.Topics.ToList());
        }
        public ActionResult DetailsTopics(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // GET: Topics/Create
        public ActionResult CreateTopics()
        {
            Topic topic = new Topic();
            topic.CreateAt = System.DateTime.Now;
            return View(topic);
        }

        // POST: Topics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateTopics([Bind(Include = "Id,TopicName,Description,CourseId,CreateAt")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Topics.Add(topic);
                db.SaveChanges();
                return RedirectToAction("Topics");
            }

            return View(topic);
        }

        // GET: Topics/Edit/5
        public ActionResult EditTopics(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);

            ViewBag.Trainers = new SelectList(db.Trainers, "Id", "FullName");
            ViewBag.TrainerTable = db.Trainer_Topics.Where(t => t.TopicsId == id).Select(t => t.TrainerID).ToList();

            //ViewBag.TrainerTable = db.Trainer_Topics.Where(t => t.Trainer_Topics == id).SelectMany(t => t.Trainers).ToList();
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTopics([Bind(Include = "Id,TopicName,Description,CourseId,CreateAt")] Topic topic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(topic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Topics");
            }
            return View(topic);
        }

        public ActionResult Assign(int TopicsId, int Trainers)
        {
            Trainer trainers = db.Trainers.Find(Trainers);
            Topic topic = db.Topics.Find(TopicsId);

            if (trainers != null)
            {
                //neu nhiêu nhiều là vậy
                //trainers.Topics.Add(topic);
                //topic.Trainers.Add(trainer);
                //mà giờ em có cái class Trainer_Topics rồi thì không dùng cách này được 

                Trainer_Topics trainer_topic = new Trainer_Topics();

                trainer_topic.Topic = topic;
                trainer_topic.Trainer = trainers;

                db.Trainer_Topics.Add(trainer_topic);
                db.SaveChanges();
                return View();
            }

            return View();
        }

        public ActionResult DeleteTrainer(int trainers, int topicId, int trainer_topicsc)
        {
            Trainer trainer = db.Trainers.Find(trainers);
            Topic topic = db.Topics.Find(topicId);
            Trainer_Topics trainer_Topics = db.Trainer_Topics.Find(trainer_topicsc);

            trainer.Trainer_Topics.Remove(trainer_Topics);
            topic.Trainer_Topics.Remove(trainer_Topics);
            db.SaveChanges();

            return RedirectToAction("EditTopics", "Staff", new { id = topicId });


        }

        // GET: Topics/Delete/5
        public ActionResult DeleteTopics(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Topic topic = db.Topics.Find(id);
            if (topic == null)
            {
                return HttpNotFound();
            }
            return View(topic);
        }

        // POST: Topics/Delete/5
        [HttpPost, ActionName("DeleteTopics")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteTopicsConfirmed(int id)
        {
            Topic topic = db.Topics.Find(id);
            db.Topics.Remove(topic);
            db.SaveChanges();
            return RedirectToAction("Topics");
        }

        /*-----------------------------------End of Topics----------------------------------------------- */

        public ActionResult Courses()
        {
            return View();
        }

        /*-----------------------------------Start of CategorCourse----------------------------------------------- */
        public ActionResult CoursesCategory()
        {
            var courseCategories = db.CourseCategories.Include(c => c.Courses);
            return View(courseCategories.ToList());
        }
        public ActionResult DetailsCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }
            return View(courseCategory);
        }

        // GET: CourseCategories/Create
        public ActionResult CreateCategory()
        {
            CourseCategory courseCategory = new CourseCategory();
            courseCategory.CreateAt = System.DateTime.Now;
            return View(courseCategory);
        }

        // POST: CourseCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCategory([Bind(Include = "Id,CategName,Description,CreateAt")] CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                db.CourseCategories.Add(courseCategory);
                db.SaveChanges();
                return RedirectToAction("CoursesCategory");
            }

            ViewBag.Id = new SelectList(db.Courses, "Id", "CourseName", courseCategory.Id);
            return View(courseCategory);
        }

        // GET: CourseCategories/Edit/5
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Courses, "Id", "CourseName", courseCategory.Id);
            return View(courseCategory);
        }

        // POST: CourseCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCategory([Bind(Include = "Id,CategName,Description,CreateAt")] CourseCategory courseCategory)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseCategory).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("CoursesCategory");
            }
            ViewBag.Id = new SelectList(db.Courses, "Id", "CourseName", courseCategory.Id);
            return View(courseCategory);
        }

        // GET: CourseCategories/Delete/5
        public ActionResult DeleteCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            if (courseCategory == null)
            {
                return HttpNotFound();
            }
            return View(courseCategory);
        }

        // POST: CourseCategories/Delete/5
        [HttpPost, ActionName("DeleteCategory")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCategoryConfirmed(int id)
        {
            CourseCategory courseCategory = db.CourseCategories.Find(id);
            db.CourseCategories.Remove(courseCategory);
            db.SaveChanges();
            return RedirectToAction("CoursesCategory");
        }
        /*-----------------------------------End of Category Course----------------------------------------------- */
    }
}