using Question_Paper_Management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using BCrypt.Net;
using System.Web.Security;

namespace Question_Paper_Management.Controllers
{
    public class AuthenticationController : Controller
    {
        private QPManagementEntities db = new QPManagementEntities();
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var dbUser = db.Users.SingleOrDefault(u => u.Email == user.Email);

                if (dbUser != null && BCrypt.Net.BCrypt.Verify(user.PasswordHash, dbUser.PasswordHash))
                {
                    // Store user details in session
                    Session["UserId"] = dbUser.UserID;
                    Session["name"] = dbUser.Username;
                    Session["Email"] = dbUser.Email;
                    Session["Role"] = dbUser.Role;
                    // Add more user details to session if needed

                    FormsAuthentication.SetAuthCookie(user.Email, false);
                    return RedirectToAction("Index", "QuestionPapers");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Email or password");
                }
            }

            return View(user);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

    }
}