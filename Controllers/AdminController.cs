using Question_Paper_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Question_Paper_Management.Controllers
{
    public class AdminController : Controller
    {
        public bool isAuthorized()
        {
            if (Session["Role"] != null && Session["Role"].ToString() == "Admin") return true;
            return false;
        }
        private QPManagementEntities db = new QPManagementEntities();

        public ActionResult Index()
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            return View();
        }
        public ActionResult PendingList()
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            var data = db.QuestionPapers.Where(qp => qp.Status == "Pending");
            return View(data.ToList());
        }
        public ActionResult ApprovedList()
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            var data = db.QuestionPapers.Where(qp => qp.Status == "Approved");
            return View(data.ToList());
        }
        public ActionResult RejectedList()
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
            var data = db.QuestionPapers.Where(qp => qp.Status == "Rejected");
            return View(data.ToList());
        }

        public ActionResult ApproveQP(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
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

            questionPaper.Status = "Approved";
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }
        public ActionResult RejectQP(int? id)
        {
            if (!isAuthorized()) return RedirectToAction("Login", "Authentication");
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

            questionPaper.Status = "Rejected";
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

    }
}