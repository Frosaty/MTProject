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
        private Manage_TrainingEntities8 db = new Manage_TrainingEntities8();
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
            ViewBag.TrainerTable = db.Trainer_Topics.Where(t => t.TopicsId == id).Include(t => t.Trainer).ToList();
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
                //trainers.Topics.Add(topic);
                //topic.Trainers.Add(trainer);
                Trainer_Topics trainer_topic = new Trainer_Topics();

                trainer_topic.Topic = topic;
                trainer_topic.Trainer = trainers;

                db.Trainer_Topics.Add(trainer_topic);
                db.SaveChanges();
            }
            return RedirectToAction("EditTopics", new{ id = topic.Id});            
        }
        //ASSIGN DELETE
        public ActionResult RemoveTrainer(int trainer_topic_id)
        {
            Trainer_Topics trainer_topic = db.Trainer_Topics.Find(trainer_topic_id);
            int topicId = trainer_topic.Topic.Id;
            db.Trainer_Topics.Remove(trainer_topic);
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
            var courses = db.Courses.Include(c => c.Trainee_Course).Include(c => c.CourseCategory);
            return View(courses.ToList());
        }
        public ActionResult DetailsCourses(int? id)
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
        public ActionResult CreateCourses()
        {
            Cours cours = new Cours();
            cours.CreateAt = System.DateTime.Now;
            //return View();
            ViewBag.CategoryId = new SelectList(db.CourseCategories, "Id", "CategName");
            //ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id");
            return View(cours);
        }

        // POST: Cours/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateCourses([Bind(Include = "CourseName,Description,CreateAt")] Cours cours, int CategoryId)
        {
            if (ModelState.IsValid)
            {
                CourseCategory category = db.CourseCategories.Find(CategoryId);
                Cours course = new Cours();

                course.CourseCategory = category;

                course.CourseName = cours.CourseName;
                course.Description = cours.Description;
                course.CreateAt = cours.CreateAt;
                course.CategoryId = CategoryId;

                db.Courses.Add(course);

                db.SaveChanges();
                return RedirectToAction("Courses");
            }

            ViewBag.Id = new SelectList(db.CourseCategories, "Id", "CategName", cours.Id);
            ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id", cours.Id);
            return View(cours);
        }

        // GET: Cours/Edit/5
        public ActionResult EditCourses(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
 
            ViewBag.CategoryId = new SelectList(db.CourseCategories, "Id", "CategName", cours.CategoryId);

            ViewBag.Trainee = new SelectList(db.Trainees, "Id", "FullName");
            ViewBag.TraineeTable = db.Trainee_Course.Where(t => t.CourseId == id).Include(t => t.Trainee).ToList();

            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCourses([Bind(Include = "Id,CategoryId,CourseName,Description,CreateAt")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                //db.Entry(cours.CategoryId).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Courses");
            }
            ViewBag.Id = new SelectList(db.CourseCategories, "Id", "CategName", cours.Id);
            //ViewBag.Id = new SelectList(db.Trainee_Course, "Id", "Id", cours.Id);
            return View(cours);
        }
        //Assign
        public ActionResult AssignCourse(int CourseId, int Trainee)
        {
            Trainee trainee = db.Trainees.Find(Trainee);
            Cours course = db.Courses.Find(CourseId);

            if (trainee != null)
            {
                //trainers.Topics.Add(topic);
                //topic.Trainers.Add(trainer);
                Trainee_Course trainee_Course = new Trainee_Course();

                trainee_Course.Cours = course;
                trainee_Course.Trainee = trainee;
                
                
                db.Trainee_Course.Add(trainee_Course);
                db.SaveChanges();
            }
            return RedirectToAction("EditCourses", new { id = course.Id });
        }
        //DELETE ASSIGN
        public ActionResult RemoveTrainee(int trainee_course_id)
        {
            Trainee_Course trainee_Course = db.Trainee_Course.Find(trainee_course_id);
            int courseId = trainee_Course.Cours.Id;
            db.Trainee_Course.Remove(trainee_Course);
            db.SaveChanges();

            return RedirectToAction("EditCourses", "Staff", new { id = courseId });
        }
        // GET: Cours/Delete/5
        public ActionResult DeleteCourses(int? id)
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
        [HttpPost, ActionName("DeleteCourses")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteCoursesConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
            db.SaveChanges();
            return RedirectToAction("Index");
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