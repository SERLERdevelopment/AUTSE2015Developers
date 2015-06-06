using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Serler.Models;
using System.Data.SqlClient;
using Dapper;
using System.IO;
using Rotativa.Options;

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

            if (model.JournalName == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("JournalName", "Journal Name is empty.");
            }

            if (ModelState.IsValid && isCorrectInput == true)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = string.Format("insert into Paper (PaperTitle, Date, Author, JournalName, Category, Methodology, Practice, IsActive, isAnalyzed, isRejected)" 
                    + "values ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 0, 0, 0)", model.PaperTitle, model.Date, model.Author, model.JournalName, 
                    model.Category, model.Methodology, model.Practice);
                    conn.Open();
                    conn.Execute(query);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult QuickSubmit()
        {
            return View(new PaperViewModel());
        }

        [HttpPost]
        public ActionResult QuickSubmit(PaperViewModel model)
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

            if (model.JournalName == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("JournalName", "Name of journal is empty.");
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

            if (model.Publisher == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("Publisher", "Publisher is empty.");
            }

            if (model.OutcomeBeingTested == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("OutcomeBeingTested", "Outcome being tested is empty.");
            }


            if (model.ContextWho == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ContextWho", "Study context of who is is empty.");
            }

            if (model.ContextWhat == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ContextWhat", "Study context of what is is empty.");
            }

            if (model.ContextWhere == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ContextWhere", "Study context of where is is empty.");
            }

            if (model.WhoRated == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("WhoRated", "Who rated is is empty.");
            }

            if (model.StudyResult == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("StudyResult", "Result of study is empty.");
            }

            if (model.ImplementationIntegrity == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ImplementationIntegrity", "Implementation Integrity cannot be empty.");
            }

            if (model.ConfidenceRating == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ConfidenceRating", "Confidence Rating cannot be empty");
            }

            if (model.ConfidenceRating == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ConfidenceRatingReason", "Reason for Confidence Rating cannot be empty");
            }

            if (model.ResearchQuestion == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ResearchQuestion", "Research Question cannot be empty");
            }

            if (model.ResearchMetrics == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ResearchMetrics", "Research Metrics cannot be empty");
            }

            if (ModelState.IsValid && isCorrectInput == true)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = string.Format("insert into Paper (PaperTitle, Date, Author, JournalName, Category, Publisher, Methodology, Practice, OutcomeBeingTested, ContextWho," 
                    + " ContextWhat, ContextWhere, StudyResult, ImplementationIntegrity, ConfidenceRating, ConfidenceRatingReason, WhoRated, ResearchQuestion, ResearchMethod,"
                    + " ResearchMetrics, ResearchLevel, ParticipantsNature, IsActive, isAnalyzed, isRejected) "
                    + "values ('{0}', '{1}','{2}', '{3}','{4}', '{5}','{6}', '{7}', '{8}','{9}', '{10}','{11}', '{12}','{13}', '{14}','{15}', '{16}', '{17}',"
                    + " '{18}', '{19}','{20}', '{21}', 1, 1, 0)", model.PaperTitle, model.Date, model.Author, model.JournalName, model.Category, model.Publisher, 
                    model.Methodology, model.Practice, model.OutcomeBeingTested, model.ContextWho, model.ContextWhat, model.ContextWhere, model.StudyResult,
                    model.ImplementationIntegrity, model.ConfidenceRating, model.ConfidenceRatingReason, model.WhoRated, model.ResearchQuestion, model.ResearchMethod,
                    model.ResearchMetrics, model.ResearchLevel, model.ParticipantsNature);
                    conn.Open();
                    conn.Execute(query);
                }
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }

        public ActionResult AdministratePaper()
        {
            return View();
        }

        public ActionResult EditPaper()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ViewPaper(int id, string submit)
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
        public ActionResult ViewPaper(PaperViewModel model, string submit)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = "Select PaperTitle = @PaperTitle, Category = @Category, Date = @Date, Author = @Author, JournalName = @JournalName, Publisher = @Publisher, " 
                    + "Rating = @Rating, NoOfPeopleRated = @NoOfPeopleRated, Methodology = @Methodology, Practice = @Practice, OutcomeBeingTested = @OutcomeBeingTested,"
                    + "ContextWho = @ContextWho, ContextWhat = @ContextWhat, ContextWhere = @ContextWhere, StudyResult = @StudyResult,"
                    + "ImplementationIntegrity = @ImplementationIntegrity, ConfidenceRating = @ConfidenceRating, ConfidenceRatingReason = @ConfidenceRatingReason, "
                    + "WhoRated = @WhoRated, ResearchQuestion = @ResearchQuestion, ResearchMethod = @ResearchMethod, ResearchMetrics = @ResearchMetrics,"
                    + " ResearchLevel = @ResearchLevel, ParticipantsNature = @ParticipantsNature from Paper;";
                    conn.Open();
                    conn.Execute(query, new { PaperTitle = model.PaperTitle, Category = model.Category, Date = model.Date, Author = model.Author, JournalName = model.JournalName, 
                    PaperId = model.PaperId, Publisher = model.Publisher, Rating = model.Rating, NoOfPeopleRated = model.NoOfPeopleRated, Methodology = model.Methodology,
                    Practice = model.Practice, OutcomeBeingTested = model.OutcomeBeingTested, ContextWho = model.ContextWho, ContextWhat = model.ContextWhat, 
                    ContextWhere = model.ContextWhere,StudyResult = model.StudyResult, ImplementationIntegrity = model.ImplementationIntegrity, 
                    ConfidenceRating = model.ConfidenceRating, ConfidenceRatingReason = model.ConfidenceRatingReason, WhoRated = model.WhoRated, 
                    ResearchQuestion = model.ResearchQuestion,ResearchMethod = model.ResearchMethod, ResearchMetrics = model.ResearchMetrics, 
                    ResearchLevel = model.ResearchLevel, ParticipantsNature = model.ParticipantsNature});
                }
                switch (submit)
                {
                    case "Save":
                        return new Rotativa.PartialViewAsPdf("ViewPaper", model);
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditPaper(int id, string submit)
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
        public ActionResult EditPaper(PaperViewModel model, string submit)
        {
            switch (submit) {
                case "Change":
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

                    if (model.JournalName == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("JournalName", "Name of journal is empty.");
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

                    if (model.Publisher == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("Publisher", "Publisher is empty.");
                    }

                    if (model.OutcomeBeingTested == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("OutcomeBeingTested", "Outcome being tested is empty.");
                    }


                    if (model.ContextWho == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("ContextWho", "Study context of who is is empty.");
                    }

                    if (model.ContextWhat == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("ContextWhat", "Study context of what is is empty.");
                    }

                    if (model.ContextWhere == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("ContextWhere", "Study context of where is is empty.");
                    }

                    if (model.StudyResult == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("StudyResult", "Result of study is empty.");
                    }

                    if (model.ImplementationIntegrity == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("ImplementationIntegrity", "Implementation Integrity cannot be empty.");
                    }

                    if (model.ConfidenceRating == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("ConfidenceRating", "Confidence Rating cannot be empty");
                    }

                    if (model.ConfidenceRatingReason == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("ConfidenceRatingReason", "Reason for Confidence Rating cannot be empty");
                    }

                    if (model.WhoRated == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("WhoRated", "Who Rated cannot be empty");
                    }

                    if (model.ResearchQuestion == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("ResearchQuestion", "Research Question cannot be empty");
                    }

                    if (model.ResearchMetrics == null)
                    {
                        isCorrectInput = false;
                        ModelState.AddModelError("ResearchMetrics", "Research Metrics cannot be empty");
                    }

                    if (ModelState.IsValid && isCorrectInput)
                    {
                     using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                    {
                    var query = "Update Paper set PaperTitle = @PaperTitle, Category = @Category, Date = @Date, Author = @Author, JournalName = @JournalName, Publisher = @Publisher,"
                    + "Rating = @Rating, NoOfPeopleRated = @NoOfPeopleRated, Methodology = @Methodology,Practice = @Practice, OutcomeBeingTested = @OutcomeBeingTested,"
                    + "ContextWho = @ContextWho,ContextWhat = @ContextWhat, ContextWhere = @ContextWhere,StudyResult = @StudyResult," 
                    + "ImplementationIntegrity = @ImplementationIntegrity, ConfidenceRating = @ConfidenceRating,"
                    + "ConfidenceRatingReason = @ConfidenceRatingReason, WhoRated = @WhoRated, ResearchQuestion = @ResearchQuestion, ResearchMethod = @ResearchMethod,"
                    + "ResearchMetrics = @ResearchMetrics, ResearchLevel = @ResearchLevel, ParticipantsNature = @ParticipantsNature where PaperId = @PaperId";
                    conn.Open();
                    conn.Execute(query, new { PaperTitle = model.PaperTitle, Category = model.Category, Date = model.Date, Author = model.Author, JournalName = model.JournalName, 
                    PaperId = model.PaperId, Publisher = model.Publisher, Rating = model.Rating,  NoOfPeopleRated = model.NoOfPeopleRated, Methodology = model.Methodology,
                    Practice = model.Practice, OutcomeBeingTested = model.OutcomeBeingTested, ContextWho = model.ContextWho, ContextWhat = model.ContextWhat, 
                    ContextWhere = model.ContextWhere,StudyResult = model.StudyResult, ImplementationIntegrity = model.ImplementationIntegrity, 
                    ConfidenceRating = model.ConfidenceRating, ConfidenceRatingReason = model.ConfidenceRatingReason, WhoRated = model.WhoRated, 
                    ResearchQuestion = model.ResearchQuestion, ResearchMethod = model.ResearchMethod, ResearchMetrics = model.ResearchMetrics, 
                    ResearchLevel = model.ResearchLevel, ParticipantsNature = model.ParticipantsNature});
                    return RedirectToAction("AdministratePaper");
                }
            }
            break;
                case "Reject":
                    if (ModelState.IsValid)
                    {
                        using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                        {
                            var query = "Update Paper set isRejected = 1 where PaperId = @PaperId";
                            conn.Open();
                            conn.Execute(query, new { PaperId = model.PaperId });
                            return RedirectToAction("AdministratePaper");
                        }
                    }
                    break;
        }
            return View(model);
        }

        public ActionResult ViewModeratorList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ModeratePaper(int id, string submit)
        {
            Response.Write(submit);
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = "select * from Paper where PaperId = @PaperId";
                conn.Open();
                var model = conn.Query<PaperViewModel>(query, new { PaperId = id }).FirstOrDefault();
                return View(model);
            }
        }

        [HttpPost]
        public ActionResult ModeratePaper(PaperViewModel model, string submit)
        {
            switch (submit) {
                case "Accept":
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
                    break;
                case "Reject":
                    if (ModelState.IsValid)
                    {
                        using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                        {
                            var query = "Update Paper set isRejected = 1 where PaperId = @PaperId";
                            conn.Open();
                            conn.Execute(query, new { PaperId = model.PaperId });
                            return RedirectToAction("ViewModeratorList");
                        }
                    }
                    break;
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult AnalyzePaper(int id)
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
        public ActionResult AnalyzePaper(PaperViewModel model)
        {
            var isCorrectInput = true;
            if (model.OutcomeBeingTested == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("OutcomeBeingTested", "Outcome being tested is empty.");
            }


            if (model.ContextWho == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ContextWho", "Study context of who is is empty.");
            }

            if (model.ContextWhat == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ContextWhat", "Study context of what is is empty.");
            }

            if (model.ContextWhere == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ContextWhere", "Study context of where is is empty.");
            }

            if (model.StudyResult == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("StudyResult", "Result of study is empty.");
            }

            if (model.ImplementationIntegrity == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ImplementationIntegrity", "Implementation Integrity cannot be empty.");
            }

            if (model.ConfidenceRating == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ConfidenceRating", "Confidence Rating cannot be empty");
            }

            if (model.ConfidenceRating == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ConfidenceRatingReason", "Reason for Confidence Rating cannot be empty");
            }

            if (model.ResearchQuestion == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ResearchQuestion", "Research Question cannot be empty");
            }

            if (model.ResearchMetrics == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("ResearchMetrics", "Research Metrics cannot be empty");
            }

            if (ModelState.IsValid && isCorrectInput)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = "Update Paper set OutcomeBeingTested = @OutcomeBeingTested, ContextWho = @ContextWho, ContextWhat = @ContextWhat, ContextWhere = @ContextWhere,"
                    + "StudyResult = @StudyResult, ImplementationIntegrity = @ImplementationIntegrity, ConfidenceRating = @ConfidenceRating,"
                    + "ConfidenceRatingReason = @ConfidenceRatingReason, WhoRated = @WhoRated, ResearchQuestion = @ResearchQuestion, ResearchMethod = @ResearchMethod,"
                    + "ResearchMetrics = @ResearchMetrics, ResearchLevel = @ResearchLevel, ParticipantsNature = @ParticipantsNature, isAnalyzed = 1 where PaperId = @PaperId";
                    conn.Open();
                    conn.Execute(query, new {OutcomeBeingTested = model.OutcomeBeingTested, ContextWho = model.ContextWho, ContextWhat = model.ContextWhat, 
                    ContextWhere = model.ContextWhere, StudyResult = model.StudyResult, ImplementationIntegrity = model.ImplementationIntegrity, 
                    ConfidenceRating = model.ConfidenceRating, ConfidenceRatingReason = model.ConfidenceRatingReason, WhoRated = model.WhoRated,
                    ResearchQuestion = model.ResearchQuestion, ResearchMethod = model.ResearchMethod, ResearchMetrics = model.ResearchMetrics, ResearchLevel = model.ResearchLevel,
                    ParticipantsNature = model.ParticipantsNature, PaperId = model.PaperId});
                    return RedirectToAction("ViewAnalystList");
                }
            }
                return View(model);
            }

        public ActionResult ViewAnalystList()
        {
            return View();
        }

        public ActionResult ViewPapers()
        {
            return View();
        }

        public ActionResult Search(string searchText, string attribute, int? page)
        {
            if (page == null || page < 0)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                searchText = "";
            }

            var model = new PagedViewModel<PaperViewModel>();

            var countQuery = "";
            var query = "";

            if (attribute == "Author")
            {
                countQuery = "select COUNT(PaperId) from Paper where (Author like @searchText) and IsActive = 1;";
                query = "select * from Paper where (Author like @searchText) and IsActive = 1 order by PaperTitle offset @skip rows fetch next @take rows only;";
            }
            else if (attribute == "Publisher")
            {
                countQuery = "select COUNT(PaperId) from Paper where (Publisher like @searchText) and IsActive = 1;";
                query = "select * from Paper where (Publisher like @searchText) and IsActive = 1 order by PaperTitle offset @skip rows fetch next @take rows only;";
            }
            else
            {
                countQuery = "select COUNT(PaperId) from Paper where (PaperTitle like @searchText) and IsActive = 1;";
                query = "select * from Paper where (PaperTitle like @searchText) and IsActive = 1 order by PaperTitle offset @skip rows fetch next @take rows only;";
            }

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {                
                conn.Open();

                model.TotalCount = conn.Query<int>(countQuery, new { attribute = attribute, searchText = "%" + searchText + "%" }).FirstOrDefault();

                model.SearchModel = new SearchModel
                {
                    SearchText = searchText,
                    Take = 10,
                    Attribute = attribute
                };
                model.SearchModel.Skip = (page.Value - 1) * model.SearchModel.Take;

                var member = conn.Query<PaperViewModel>(query, new { attribute = attribute, searchText = "%" + searchText + "%", skip = model.SearchModel.Skip, take = model.SearchModel.Take }).ToList();
                if (member != null)
                {
                    model.Result = member;
                }
                var maxPage = Math.Ceiling((double)model.TotalCount / model.SearchModel.Take);

                var pagination1 = new PaginationModel();
                pagination1.DisplayLabel = "<<";
                pagination1.Url = "~/Paper/Search?page=1";
                pagination1.IsEnable = page <= 1 ? false : true;

                var pagination2 = new PaginationModel();
                pagination2.DisplayLabel = "<";
                pagination2.Url = "~/Paper/Search?page=" + (page - 1);
                pagination2.IsEnable = page <= 1 ? false : true;

                var pagination3 = new PaginationModel();
                pagination3.DisplayLabel = ">";
                pagination3.Url = "~/Paper/Search?page=" + (page + 1);
                pagination3.IsEnable = page >= maxPage ? false : true;

                var pagination4 = new PaginationModel();
                pagination4.DisplayLabel = ">>";
                pagination4.Url = "~/Paper/Search?page=" + maxPage;
                pagination4.IsEnable = page >= maxPage ? false : true;

                model.PaginationModel = new List<PaginationModel>();
                model.PaginationModel.Add(pagination1);
                model.PaginationModel.Add(pagination2);

                for (int i = 0; i < maxPage; i++)
                {
                    var paginationModel = new PaginationModel();
                    paginationModel.DisplayLabel = i + 1 + "";
                    paginationModel.Url = "~/Paper/Search?page=" + (i + 1);
                    paginationModel.IsEnable = true;
                    if (i + 1 == page)
                    {
                        paginationModel.IsActive = true;
                    }
                    model.PaginationModel.Add(paginationModel);
                }

                model.PaginationModel.Add(pagination3);
                model.PaginationModel.Add(pagination4);

                return View(model);
            }
        }
    }
}

