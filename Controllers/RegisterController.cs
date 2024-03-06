using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using BCrypt.Net;
using System.Web.Mvc;
using Question_Paper_Management.Models;

namespace Question_Paper_Management.Controllers
{
    public class RegisterController : Controller
    {
        private QPManagementEntities db = new QPManagementEntities();

        public ActionResult Index()
        {
            if (isAuthorized()) return View(db.Users.ToList());
            return RedirectToAction("Login", "Authentication");
        }

        public bool isAuthorized()
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin") return true;
            return false;
        }
        

        // GET: Register/Details/5
        public ActionResult Details(int? id)
        {
            if(!isAuthorized()) return RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Register/Create
        public ActionResult Create()
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            return View();
        }

        // POST: Register/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Username,PasswordHash,Email,Role")] User user)
        {
            if(!isAuthorized()) RedirectToAction("Login", "Authentication");
            if (ModelState.IsValid)
            {
                // Hash the password using BCrypt
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Register/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!isAuthorized()) RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Register/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Username,PasswordHash,Email,Role")] User user)
        {

            if (!isAuthorized()) RedirectToAction("Login", "Authentication");
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Register/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!isAuthorized()) RedirectToAction("Login", "Authentication");
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Register/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (id > 0)
            {
                var user = db.Users.FirstOrDefault(model => model.UserID == id);
                if (user != null)
                {
                    var questionPapers = db.QuestionPapers.Where(model => model.CreatorID == id).ToList();
                    if (questionPapers.Any())
                    {
                        foreach (var questionPaper in questionPapers)
                        {
                            if (questionPaper != null)
                            {
                                var questions = db.Questions.Where(model => model.QuestionPaperID == questionPaper.QuestionPaperID).ToList();
                                foreach (var question in questions)
                                {
                                    db.Questions.Remove(question);
                                }
                                db.SaveChanges(); // Save changes for each iteration
                                db.QuestionPapers.Remove(questionPaper);
                            }
                        }
                        db.SaveChanges(); // Save changes after removing questionPapers
                    }
                    db.Users.Remove(user);
                    db.SaveChanges(); // Save changes after removing user
                    TempData["DeleteMessage"] = "User Deleted Successfully!!";
                }
            }
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
