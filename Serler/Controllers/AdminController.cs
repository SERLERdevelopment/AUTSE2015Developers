using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using Serler.Models;
using System.Data.SqlClient;

namespace Serler.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllUsers()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = string.Format("select * from Users where IsActive = 1 order by Email;");
                conn.Open();
                var member = conn.Query<UserViewModel>(query).ToList();
                return View(member);
            }
        }

        public ActionResult PendingUser()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = string.Format("select * from Users where IsPendingUser = 1 and IsActive = 1 order by Email;");
                conn.Open();
                var member = conn.Query<UserViewModel>(query).ToList();
                return View(member);
            }
        }

        public ActionResult SystemAdmin()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = string.Format("select * from Users where IsSystemAdmin = 1 and IsActive = 1 order by Email;");
                conn.Open();
                var member = conn.Query<UserViewModel>(query).ToList();
                return View(member);
            }
        }

        public ActionResult Moderator()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = string.Format("select * from Users where IsModerator = 1 and IsActive = 1 order by Email;");
                conn.Open();
                var member = conn.Query<UserViewModel>(query).ToList();
                return View(member);
            }
        }

        public ActionResult Analyst()
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = string.Format("select * from Users where IsAnalyst = 1 and IsActive = 1 order by Email;");
                conn.Open();
                var member = conn.Query<UserViewModel>(query).ToList();
                return View(member);
            }
        }
    }
}
