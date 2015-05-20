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
                    var query = string.Format("insert into Paper (PaperTitle, Date, Author, PaperLink, Category, Methodology, MethodologyDescription, Practice, PracticeDescription, IsActive, isAnalyzed) values ('{0}', '{1}','{2}', '{3}','{4}', '{5}','{6}', '{7}', '{8}', 0, 0)", model.PaperTitle,
                        model.Date, model.Author, model.PaperLink, model.Category, model.Methodology,
                        model.MethodologyDescription, model.Practice, model.PracticeDescription);
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
                    var query = "Select PaperTitle = @PaperTitle, Category = @Category, Date = @Date, Author = @Author, PaperLink = @PaperLink, Publisher = @Publisher, " 
                    + "Abstract = @Abstract, Reference = @Reference, Rating = @Rating, NoOfPeopleRated = @NoOfPeopleRated, Methodology = @Methodology, MethodologyDescription = @MethodologyDescription, "
                    + "Practice = @Practice, PracticeDescription = @PracticeDescription, OutcomeBeingTested = @OutcomeBeingTested, StudyContext = @StudyContext, "
                    + "StudyResult = @StudyResult, ImplementationIntegrity = @ImplementationIntegrity, ConfidenceRating = @ConfidenceRating, WhoRated = @WhoRated, "
                    + "ResearchQuestion = @ResearchQuestion, ResearchMethod = @ResearchMethod, ResearchMetrics = @ResearchMetrics, ParticipantsNature = @ParticipantsNature "
                    + "from Paper;";
                    conn.Open();
                    conn.Execute(query, new { PaperTitle = model.PaperTitle, Category = model.Category, Date = model.Date, Author = model.Author, PaperLink = model.PaperLink, 
                    PaperId = model.PaperId, Publisher = model.Publisher, Abstract =model.Abstract, Reference = model.Reference, Rating = model.Rating, 
                    NoOfPeopleRated = model.NoOfPeopleRated, Methodology = model.Methodology,MethodologyDescription = model.MethodologyDescription, Practice = model.Practice,
                    PracticeDescription = model.PracticeDescription, OutcomeBeingTested = model.OutcomeBeingTested, StudyContext = model.StudyContext, StudyResult = model.StudyResult,
                    ImplementationIntegrity = model.ImplementationIntegrity, ConfidenceRating = model.ConfidenceRating, WhoRated = model.WhoRated, ResearchQuestion = model.ResearchQuestion,
                    ResearchMethod = model.ResearchMethod, ResearchMetrics = model.ResearchMetrics, ParticipantsNature = model.ParticipantsNature});
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
            if (model.Date == null)
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = "Update Paper set PaperTitle = @PaperTitle, Category = @Category, Date = @Date, Author = @Author, PaperLink = @PaperLink, Publisher = @Publisher, Abstract = @Abstract, Reference = @Reference, "
                    + "Rating = @Rating, NoOfPeopleRated = @NoOfPeopleRated, Methodology = @Methodology, MethodologyDescription = @MethodologyDescription, Practice = @Practice, PracticeDescription = @PracticeDescription, "
                    + "OutcomeBeingTested = @OutcomeBeingTested, StudyContext = @StudyContext,StudyResult = @StudyResult, ImplementationIntegrity = @ImplementationIntegrity, ConfidenceRating = @ConfidenceRating, WhoRated = @WhoRated, "
                    + "ResearchQuestion = @ResearchQuestion, ResearchMethod = @ResearchMethod, ResearchMetrics = @ResearchMetrics, ParticipantsNature = @ParticipantsNature where PaperId = @PaperId";
                    conn.Open();
                    conn.Execute(query, new { PaperTitle = model.PaperTitle, Category = model.Category, Date = model.Date, Author = model.Author, PaperLink = model.PaperLink, PaperId = model.PaperId, Publisher = model.Publisher, Abstract = model.Abstract, Reference = model.Reference, Rating = model.Rating, 
                    NoOfPeopleRated = model.NoOfPeopleRated, Methodology = model.Methodology,MethodologyDescription = model.MethodologyDescription, Practice = model.Practice,
                    PracticeDescription = model.PracticeDescription, OutcomeBeingTested = model.OutcomeBeingTested, StudyContext = model.StudyContext, StudyResult = model.StudyResult,
                    ImplementationIntegrity = model.ImplementationIntegrity, ConfidenceRating = model.ConfidenceRating, WhoRated = model.WhoRated, ResearchQuestion = model.ResearchQuestion,
                    ResearchMethod = model.ResearchMethod, ResearchMetrics = model.ResearchMetrics, ParticipantsNature = model.ParticipantsNature});
                    return RedirectToAction("AdministratePaper");
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
            if (model.Abstract == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("Abstract", "Abstract cannot be empty");
            }

            if (model.Reference == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("Reference", "Outcome cannot be empty");
            }

            if (model.OutcomeBeingTested == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("OutcomeBeingTested", "Outcome cannot be empty");
            }

            if (model.WhoRated == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("WhoRated", "Who Rated cannot be empty");
            }

            if (model.StudyContext == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("StudyContext", "Context of the study cannot be empty");
            }

            if (model.StudyResult == null)
            {
                isCorrectInput = false;
                ModelState.AddModelError("StudyResult", "Result of study cannot be empty");
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
                    var query = "Update Paper set Abstract = @Abstract, Reference = @Reference, OutcomeBeingTested = @OutcomeBeingTested, StudyContext = @StudyContext,StudyResult = @StudyResult, ImplementationIntegrity = @ImplementationIntegrity, ConfidenceRating = @ConfidenceRating, WhoRated = @WhoRated, "
                    + "ResearchQuestion = @ResearchQuestion, ResearchMethod = @ResearchMethod, ResearchMetrics = @ResearchMetrics, ParticipantsNature = @ParticipantsNature, isAnalyzed = 1 where PaperId = @PaperId";
                    conn.Open();
                    conn.Execute(query, new {OutcomeBeingTested = model.OutcomeBeingTested, StudyContext = model.StudyContext, StudyResult = model.StudyResult,
                    ImplementationIntegrity = model.ImplementationIntegrity, ConfidenceRating = model.ConfidenceRating, WhoRated = model.WhoRated, ResearchQuestion = model.ResearchQuestion,
                    ResearchMethod = model.ResearchMethod, ResearchMetrics = model.ResearchMetrics, ParticipantsNature = model.ParticipantsNature, PaperId = model.PaperId, Abstract = model.Abstract,
                    Reference = model.Reference});
                    return RedirectToAction("ViewAnalystList");
                }
            }
                return View(model);
            }

        public ActionResult ViewAnalystList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult RejectPaper(int id)
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
        public ActionResult RejectPaper(PaperViewModel model)
        {
            if (ModelState.IsValid)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = "Delete * from Paper where PaperId = @PaperId";
                    conn.Open();
                    conn.Execute(query, new { PaperId = model.PaperId });
                    return RedirectToAction("ViewModeratorList");
                }
            }
            return View(model);
        }

        public ActionResult ViewPapers()
        {
            return View();
        }

        public ActionResult Search(string searchText, int? page)
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

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var countQuery = "select COUNT(PaperId) from Paper where (PaperTitle like @searchText or Author like @searchText or Publisher like @searchText) and IsActive = 1;";
                var query = string.Format("select * from Paper where IsActive = 1 order by PaperTitle;");
                conn.Open();
                model.TotalCount = conn.Query<int>(countQuery, new { searchText = "%" + searchText + "%" }).FirstOrDefault();
                model.SearchModel = new SearchModel
                {
                    SearchText = "%" + searchText + "%",
                    Take = 10
                };
                model.SearchModel.Skip = (page.Value - 1) * model.SearchModel.Take;

                var member = conn.Query<PaperViewModel>(query).ToList();
                if (member != null)
                {
                    model.Result = member;
                }
                var maxPage = Math.Ceiling((double)model.TotalCount / model.SearchModel.Take);

                var pagination1 = new PaginationModel();
                pagination1.DisplayLabel = "<<";
                pagination1.Url = "~/Search?page=1";
                pagination1.IsEnable = page <= 1 ? false : true;

                var pagination2 = new PaginationModel();
                pagination2.DisplayLabel = "<";
                pagination2.Url = "~/Search?page=" + (page - 1);
                pagination2.IsEnable = page <= 1 ? false : true;

                var pagination3 = new PaginationModel();
                pagination3.DisplayLabel = ">";
                pagination3.Url = "~/Search?page=" + (page + 1);
                pagination3.IsEnable = page >= maxPage ? false : true;

                var pagination4 = new PaginationModel();
                pagination4.DisplayLabel = ">>";
                pagination4.Url = "~/Search?page=" + (page - 1);
                pagination4.IsEnable = page >= maxPage ? false : true;

                model.PaginationModel = new List<PaginationModel>();
                model.PaginationModel.Add(pagination1);
                model.PaginationModel.Add(pagination2);

                for (int i = 0; i < maxPage; i++)
                {
                    var paginationModel = new PaginationModel();
                    paginationModel.DisplayLabel = i + 1 + "";
                    paginationModel.Url = "~/Search?page=" + (i + 1);
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

