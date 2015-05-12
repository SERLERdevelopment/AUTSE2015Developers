using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serler.Models;
using System.Data.SqlClient;
using Dapper;

namespace Serler.Controllers
{
    public class PaperController : Controller
    {
        //
        // GET: /Paper/
        [HttpGet]
        public ActionResult SubmitPaper()
        {
            return View(new PaperViewModel());
        }

        [HttpPost]
        public ActionResult SubmitPaper(PaperViewModel model)
        {
            var isCorrectInput = true;
            if (model.PaperTitle == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("PaperTitle", "The title is empty.");
            }

            if (model.Category == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("Category", "The category is empty.");
            }

            if (model.Author == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("Author", "The author is empty.");
            }

            if (model.Date == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("Date", "The Date is empty.");
            }

            if (model.PaperLink == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("PaperLink", "The link to paper is empty.");
            }
            if (ModelState.IsValid && isCorrectInput == true)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = string.Format("insert into Paper (PaperTitle, Date, Author, PaperLink, Category, IsActive) values ('{0}', '{1}','{2}', '{3}','{4}', 0)", model.PaperTitle, model.Date, model.Author, model.PaperLink, model.Category);
                    conn.Open();
                    conn.Execute(query);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult ViewPaperList()
        {
            return View();
        }

        public ActionResult EditPaper()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewPaper(int id)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = "select * from Paper where PaperId = @PaperId";
                conn.Open();
                var model = conn.Query<PaperViewModel>(query, new { PaperId = id }).FirstOrDefault();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ViewPaper(PaperViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = "Select PaperTitle = @PaperTitle, Category = @Category, Date = @Date, Author = @Author, PaperLink = @PaperLink, Publisher = @Publisher, Abstract = @Abstract, Reference = @Reference, Rating = @Rating, NoOfPeopleRated = @NoOfPeopleRated from Paper;";
                    conn.Open();
                    conn.Execute(query, new { PaperTitle = model.PaperTitle, Category = model.Category, Date = model.Date, Author = model.Author, PaperLink = model.PaperLink, PaperId = model.PaperId, Publisher = model.Publisher, Abstract =model.Abstract, Reference = model.Reference, Rating = model.Rating, NoOfPeopleRated = model.NoOfPeopleRated });
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditPaper(int id)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = "select * from Paper where PaperId = @PaperId";
                conn.Open();
                var model = conn.Query<PaperViewModel>(query, new { PaperId = id }).FirstOrDefault();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult EditPaper(PaperViewModel model)
        {
            var isCorrectInput = true;
            if (model.PaperTitle == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("PaperTitle", "The title is empty.");
            }

            if (model.Category == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("Category", "The category is empty.");
            }

            if (model.Author == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("Author", "The author is empty.");
            }

            if (model.Date == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("Date", "The Date is empty.");
            }

            if (model.PaperLink == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("PaperLink", "The link to paper is empty.");
            }
            if (ModelState.IsValid && isCorrectInput)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = "Update Paper set PaperTitle = @PaperTitle, Category = @Category, Date = @Date, Author = @Author, PaperLink = @PaperLink, Publisher = @Publisher, Abstract = @Abstract, Reference = @Reference where PaperId = @PaperId";
                    conn.Open();
                    conn.Execute(query, new { PaperTitle = model.PaperTitle, Category = model.Category, Date = model.Date, Author = model.Author, PaperLink = model.PaperLink, PaperId = model.PaperId, Publisher = model.Publisher, Abstract = model.Abstract, Reference = model.Reference });
                    return RedirectToAction("ViewPaperList");
                }
            }
            return View(model);
        }

        public ActionResult ViewModeratorList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModeratePaper(int id)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = "select * from Paper where PaperId = @PaperId";
                conn.Open();
                var model = conn.Query<PaperViewModel>(query, new { PaperId = id }).FirstOrDefault();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ModeratePaper(PaperViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = "Update Paper set IsActive = 1 where PaperId = @PaperId";
                    conn.Open();
                    conn.Execute(query, new {PaperId = model.PaperId});
                    return RedirectToAction("ViewModeratorList");
                }
            }
            return View(model);
        }
    }
}

