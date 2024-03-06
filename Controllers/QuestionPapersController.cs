using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Question_Paper_Management.Models;

namespace Question_Paper_Management.Controllers
{

    public class QuestionPapersController : Controller
    {


        private QPManagementEntities db = new QPManagementEntities();

        public bool isAuthorized()
        {
            if (Session["Role"] == null) return false;
            return true;
        }

        // GET: QuestionPapers
        public ActionResult Index()
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");

            if (Convert.ToString(Session["Role"]) == "Teacher")

            {
                int userId = Session["UserID"] as int? ?? 0;

                var filterQP = db.QuestionPapers.Where(qp => qp.CreatorID == userId);
                return View(filterQP.ToList());
            }
            var questionPapers = db.QuestionPapers.Include(q => q.User);
            return View(questionPapers.ToList());
        }

        // GET: QuestionPapers/Details/5
        public ActionResult Details(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionPaper questionPaper = db.QuestionPapers.Find(id);
            if (questionPaper == null)
            {
                return HttpNotFound();
            }
            return View(questionPaper);
        }

        // GET: QuestionPapers/Create
        public ActionResult Create()
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            ViewBag.CreatorID = new SelectList(db.Users, "UserID", "Username");

            return View();
        }

        // POST: QuestionPapers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionPaperID,Title,Description")] QuestionPaper questionPaper)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            questionPaper.CreationDate = DateTime.Now;
            questionPaper.Status = "Created";
            questionPaper.CreatorID = (int)Session["UserID"];
            if (ModelState.IsValid)
            {
                db.QuestionPapers.Add(questionPaper);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CreatorID = new SelectList(db.Users, "UserID", "Username", questionPaper.CreatorID);
            return View(questionPaper);
        }

        // GET: QuestionPapers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionPaper questionPaper = db.QuestionPapers.Find(id);
            if (questionPaper == null)
            {
                return HttpNotFound();
            }
            ViewBag.CreatorID = new SelectList(db.Users, "UserID", "Username", questionPaper.CreatorID);
            return View(questionPaper);
        }

        // POST: QuestionPapers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestionPaper questionPaper)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (ModelState.IsValid)
            {
                db.Entry(questionPaper).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CreatorID = new SelectList(db.Users, "UserID", "Username", questionPaper.CreatorID);
            return View(questionPaper);
        }

        // GET: QuestionPapers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionPaper questionPaper = db.QuestionPapers.Find(id);
            if (questionPaper == null)
            {
                return HttpNotFound();
            }
            return View(questionPaper);
        }

        // POST: QuestionPapers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id > 0)
            {
                var questionPaper = db.QuestionPapers.FirstOrDefault(model => model.QuestionPaperID == id);
                if (questionPaper != null)
                {
                    var questions = db.Questions.Where(model => model.QuestionPaperID == id).ToList();
                    if (questions.Any())
                    {
                        foreach (var question in questions)
                        {
                            // Find and remove related answers first
                            //var answers = db.Answers.Where(a => a.QuestionID == question.QuestionID).ToList();
                            //if (answers.Any())
                            //{
                            //    foreach (var answer in answers)
                            //    {
                            //        db.Answers.Remove(answer);
                            //    }
                            //}
                            // Now remove the question
                            db.Questions.Remove(question);
                        }
                    }

                    db.Entry(questionPaper).State = EntityState.Deleted;
                    db.SaveChanges();
                    TempData["success"] = "Question Paper Deleted Successfully!!";
                }
            }
            if (Convert.ToString(Session["UserRole"]) == "Admin")
            {
                return RedirectToAction("QuestionPapers", "Admin");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult ViewQuestionPaper(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = db.Questions.Where(q => q.QuestionPaperID == id).ToList();
            Session["QuestionPaperID"] = id;
            return View(question);
        }

        public ActionResult AddQuestion(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            Session["QuestionPaperID"] = id;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            return View("~/Views/Questions/Create.cshtml");
        }
        public ActionResult SendForApproval(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionPaper questionPaper = db.QuestionPapers.Find(id)
        ;
            if (questionPaper == null)
            {
                return HttpNotFound();
            }

            questionPaper.Status = "Pending";
            db.SaveChanges();
            return RedirectToAction("Index", "QuestionPapers");
        }

        public ActionResult AttemptQuestionPaper(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var question = db.Questions.Where(q => q.QuestionPaperID == id).ToList();
            Session["QuestionPaperID"] = id;
            return View(question);
        }

        [HttpPost]
        public ActionResult AttemptQuestionPaper(List<Answer> data)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            return RedirectToAction("Index", "QuestionPapers");
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
