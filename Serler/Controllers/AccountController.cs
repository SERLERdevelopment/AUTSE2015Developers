using Serler.Authentications;
using Serler.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Serler.Services;

namespace Serler.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            var isValidPassword = true;
            if (model.Password != model.Password2)
            {
                isValidPassword = false;
                ModelState.AddModelError("Password2", "Check your password.");
            }

            if (ModelState.IsValid && isValidPassword)
            {
                var password = PasswordHelper.GetNewPasswordHash(model.Password);
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = string.Format("insert into users (email, password) values ('{0}', '{1}')", model.Email, password);
                    conn.Open();
                    conn.Execute(query);
                }
                return View("Login");
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Sonny"].ConnectionString))
            {
                var query = "select membername UserName, Password from member where membername = @memberName";
                conn.Open();
                var member = conn.Query<LoginViewModel>(query, new { memberName = model.Email }).FirstOrDefault();
                if (member != null)
                {
                    if (PasswordHelper.ValidatePassword(model.Password, member.Password))
                    {
                        var auth = new AuthenticationService();
                        auth.SignIn(member);
                        return RedirectToAction("Index", "Testimonials");
                    }
                }
                return View(model);
            }
        }

        public ActionResult Logout()
        {
            var service = new AuthenticationService();
            service.SignOut();
            return View();
        }
    }
}
