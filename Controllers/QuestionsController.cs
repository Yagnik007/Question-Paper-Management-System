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
    public class QuestionsController : Controller
    {
        private QPManagementEntities db = new QPManagementEntities();

        public bool isAuthorized()
        {
            if (Session["Role"] == null || Session["Role"].ToString() == "Student") return false;
            return true;
        }
        

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            ViewBag.QuestionPaperID = new SelectList(db.QuestionPapers, "QuestionPaperID", "Title");
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionID,QuestionText,Option1,Option2,Option3,Option4,CorrectAnswer")] Question question)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            question.QuestionPaperID = (int) Session["QuestionPaperID"];
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();
                return RedirectToAction("ViewQuestionPaper", "QuestionPapers", new {id = question.QuestionPaperID});
            }

            ViewBag.QuestionPaperID = new SelectList(db.QuestionPapers, "QuestionPaperID", "Title", question.QuestionPaperID);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,QuestionText,Option1,Option2,Option3,Option4,CorrectAnswer,QuestionPaperID")] Question question)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("ViewQuestionPaper", "QuestionPapers", new { id = question.QuestionPaperID });
            }
            ViewBag.QuestionPaperID = new SelectList(db.QuestionPapers, "QuestionPaperID", "Title", question.QuestionPaperID);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
            db.SaveChanges();
            return RedirectToAction("Index","QuestionPapers");
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
