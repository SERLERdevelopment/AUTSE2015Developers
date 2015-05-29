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

        [HttpGet]
        public ActionResult ModifyUser(int id)
        {
            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var query = "select * from Users where UserId = @UserId";
                conn.Open();
                var model = conn.Query<UserViewModel>(query, new { UserId = id }).FirstOrDefault();
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ModifyUser(UserViewModel model)
        {
            if(ModelState.IsValid)
            {
                using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
                {
                    var query = "update Users set IsPendingUser = @IsPendingUser, IsSystemAdmin = @IsSystemAdmin, IsModerator = @IsModerator, IsAnalyst = @IsAnalyst where UserId = @UserId;";
                    conn.Open();
                    conn.Execute(query, new { IsPendingUser = model.IsPendingUser, IsSystemAdmin = model.IsSystemAdmin, IsModerator = model.IsModerator, IsAnalyst = model.IsAnalyst, UserId = model.UserId });
                    return RedirectToAction("AllUsers");
                }
            }
            return View(model);
        }

        public ActionResult AllUsers(string searchText, int? page)
        {
            if (page == null || page < 0)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                searchText = "";
            }
            var model = new PagedViewModel<UserViewModel>();

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var countQuery = "select COUNT(UserId) from Users where Email like @searchText and IsActive = 1;";
                var query = string.Format("select * from Users where IsActive = 1 order by Email;");
                conn.Open();
                model.TotalCount = conn.Query<int>(countQuery, new { searchText = "%" + searchText + "%" }).FirstOrDefault();
                model.SearchModel = new SearchModel
                {
                    SearchText = "%" + searchText + "%",
                    Take = 10
                };
                model.SearchModel.Skip = (page.Value - 1) * model.SearchModel.Take;

                var member = conn.Query<UserViewModel>(query).ToList();
                if(member != null)
                {
                    model.Result = member;
                }
                var maxPage = Math.Ceiling((double)model.TotalCount / model.SearchModel.Take);

                var pagination1 = new PaginationModel();
                pagination1.DisplayLabel = "<<";
                pagination1.Url = "~/Admin/AllUsers?page=1";
                pagination1.IsEnable = page <= 1 ? false : true;

                var pagination2 = new PaginationModel();
                pagination2.DisplayLabel = "<";
                pagination2.Url = "~/Admin/AllUsers?page=" + (page - 1);
                pagination2.IsEnable = page <= 1 ? false : true;

                var pagination3 = new PaginationModel();
                pagination3.DisplayLabel = ">";
                pagination3.Url = "~/Admin/AllUsers?page=" + (page + 1);
                pagination3.IsEnable = page >= maxPage ? false : true;

                var pagination4 = new PaginationModel();
                pagination4.DisplayLabel = ">>";
                pagination4.Url = "~/Admin/AllUsers?page=" + (maxPage);
                pagination4.IsEnable = page >= maxPage ? false : true;

                model.PaginationModel = new List<PaginationModel>();
                model.PaginationModel.Add(pagination1);
                model.PaginationModel.Add(pagination2);

                for (int i = 0; i < maxPage; i++)
                {
                    var paginationModel = new PaginationModel();
                    paginationModel.DisplayLabel = i + 1 + "";
                    paginationModel.Url = "~/Admin/AllUsers?page=" + (i + 1);
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

        public ActionResult PendingUser(string searchText, int? page)
        {
            if (page == null || page < 0)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                searchText = "";
            }
            var model = new PagedViewModel<UserViewModel>();

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var countQuery = "select COUNT(UserId) from Users where Email like @searchText and IsActive = 1 and IsPendingUser = 1;";
                var query = string.Format("select * from Users where IsActive = 1 and IsPendingUser = 1 order by Email;");
                conn.Open();
                model.TotalCount = conn.Query<int>(countQuery, new { searchText = "%" + searchText + "%" }).FirstOrDefault();
                model.SearchModel = new SearchModel
                {
                    SearchText = "%" + searchText + "%",
                    Take = 10
                };
                model.SearchModel.Skip = (page.Value - 1) * model.SearchModel.Take;

                var member = conn.Query<UserViewModel>(query).ToList();
                if (member != null)
                {
                    model.Result = member;
                }
                var maxPage = Math.Ceiling((double)model.TotalCount / model.SearchModel.Take);

                var pagination1 = new PaginationModel();
                pagination1.DisplayLabel = "<<";
                pagination1.Url = "~/Admin/PendingUser?page=1";
                pagination1.IsEnable = page <= 1 ? false : true;

                var pagination2 = new PaginationModel();
                pagination2.DisplayLabel = "<";
                pagination2.Url = "~/Admin/PendingUser?page=" + (page - 1);
                pagination2.IsEnable = page <= 1 ? false : true;

                var pagination3 = new PaginationModel();
                pagination3.DisplayLabel = ">";
                pagination3.Url = "~/Admin/PendingUser?page=" + (page + 1);
                pagination3.IsEnable = page >= maxPage ? false : true;

                var pagination4 = new PaginationModel();
                pagination4.DisplayLabel = ">>";
                pagination4.Url = "~/Admin/PendingUser?page=" + (maxPage);
                pagination4.IsEnable = page >= maxPage ? false : true;

                model.PaginationModel = new List<PaginationModel>();
                model.PaginationModel.Add(pagination1);
                model.PaginationModel.Add(pagination2);

                for (int i = 0; i < maxPage; i++)
                {
                    var paginationModel = new PaginationModel();
                    paginationModel.DisplayLabel = i + 1 + "";
                    paginationModel.Url = "~/Admin/PendingUser?page=" + (i + 1);
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

        public ActionResult SystemAdmin(string searchText, int? page)
        {
            if (page == null || page < 0)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                searchText = "";
            }
            var model = new PagedViewModel<UserViewModel>();

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var countQuery = "select COUNT(UserId) from Users where Email like @searchText and IsActive = 1 and IsSystemAdmin = 1;";
                var query = string.Format("select * from Users where IsActive = 1 and IsSystemAdmin = 1 order by Email;");
                conn.Open();
                model.TotalCount = conn.Query<int>(countQuery, new { searchText = "%" + searchText + "%" }).FirstOrDefault();
                model.SearchModel = new SearchModel
                {
                    SearchText = "%" + searchText + "%",
                    Take = 10
                };
                model.SearchModel.Skip = (page.Value - 1) * model.SearchModel.Take;

                var member = conn.Query<UserViewModel>(query).ToList();
                if (member != null)
                {
                    model.Result = member;
                }
                var maxPage = Math.Ceiling((double)model.TotalCount / model.SearchModel.Take);

                var pagination1 = new PaginationModel();
                pagination1.DisplayLabel = "<<";
                pagination1.Url = "~/Admin/SystemAdmin?page=1";
                pagination1.IsEnable = page <= 1 ? false : true;

                var pagination2 = new PaginationModel();
                pagination2.DisplayLabel = "<";
                pagination2.Url = "~/Admin/SystemAdmin?page=" + (page - 1);
                pagination2.IsEnable = page <= 1 ? false : true;

                var pagination3 = new PaginationModel();
                pagination3.DisplayLabel = ">";
                pagination3.Url = "~/Admin/SystemAdmin?page=" + (page + 1);
                pagination3.IsEnable = page >= maxPage ? false : true;

                var pagination4 = new PaginationModel();
                pagination4.DisplayLabel = ">>";
                pagination4.Url = "~/Admin/SystemAdmin?page=" + (maxPage);
                pagination4.IsEnable = page >= maxPage ? false : true;

                model.PaginationModel = new List<PaginationModel>();
                model.PaginationModel.Add(pagination1);
                model.PaginationModel.Add(pagination2);

                for (int i = 0; i < maxPage; i++)
                {
                    var paginationModel = new PaginationModel();
                    paginationModel.DisplayLabel = i + 1 + "";
                    paginationModel.Url = "~/Admin/SystemAdmin?page=" + (i + 1);
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

        public ActionResult Moderator(string searchText, int? page)
        {
            if (page == null || page < 0)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                searchText = "";
            }
            var model = new PagedViewModel<UserViewModel>();

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var countQuery = "select COUNT(UserId) from Users where Email like @searchText and IsActive = 1 and IsModerator = 1;";
                var query = string.Format("select * from Users where IsActive = 1 and IsModerator = 1 order by Email;");
                conn.Open();
                model.TotalCount = conn.Query<int>(countQuery, new { searchText = "%" + searchText + "%" }).FirstOrDefault();
                model.SearchModel = new SearchModel
                {
                    SearchText = "%" + searchText + "%",
                    Take = 10
                };
                model.SearchModel.Skip = (page.Value - 1) * model.SearchModel.Take;

                var member = conn.Query<UserViewModel>(query).ToList();
                if (member != null)
                {
                    model.Result = member;
                }
                var maxPage = Math.Ceiling((double)model.TotalCount / model.SearchModel.Take);

                var pagination1 = new PaginationModel();
                pagination1.DisplayLabel = "<<";
                pagination1.Url = "~/Admin/Moderator?page=1";
                pagination1.IsEnable = page <= 1 ? false : true;

                var pagination2 = new PaginationModel();
                pagination2.DisplayLabel = "<";
                pagination2.Url = "~/Admin/Moderator?page=" + (page - 1);
                pagination2.IsEnable = page <= 1 ? false : true;

                var pagination3 = new PaginationModel();
                pagination3.DisplayLabel = ">";
                pagination3.Url = "~/Admin/Moderator?page=" + (page + 1);
                pagination3.IsEnable = page >= maxPage ? false : true;

                var pagination4 = new PaginationModel();
                pagination4.DisplayLabel = ">>";
                pagination4.Url = "~/Admin/Moderator?page=" + (maxPage);
                pagination4.IsEnable = page >= maxPage ? false : true;

                model.PaginationModel = new List<PaginationModel>();
                model.PaginationModel.Add(pagination1);
                model.PaginationModel.Add(pagination2);

                for (int i = 0; i < maxPage; i++)
                {
                    var paginationModel = new PaginationModel();
                    paginationModel.DisplayLabel = i + 1 + "";
                    paginationModel.Url = "~/Admin/Moderator?page=" + (i + 1);
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

        public ActionResult Analyst(string searchText, int? page)
        {
            if (page == null || page < 0)
            {
                page = 1;
            }

            if (string.IsNullOrEmpty(searchText))
            {
                searchText = "";
            }
            var model = new PagedViewModel<UserViewModel>();

            using (var conn = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Serler"].ConnectionString))
            {
                var countQuery = "select COUNT(UserId) from Users where Email like @searchText and IsActive = 1 and IsAnalyst = 1;";
                var query = string.Format("select * from Users where IsActive = 1 and IsAnalyst = 1 order by Email;");
                conn.Open();
                model.TotalCount = conn.Query<int>(countQuery, new { searchText = "%" + searchText + "%" }).FirstOrDefault();
                model.SearchModel = new SearchModel
                {
                    SearchText = "%" + searchText + "%",
                    Take = 10
                };
                model.SearchModel.Skip = (page.Value - 1) * model.SearchModel.Take;

                var member = conn.Query<UserViewModel>(query).ToList();
                if (member != null)
                {
                    model.Result = member;
                }
                var maxPage = Math.Ceiling((double)model.TotalCount / model.SearchModel.Take);

                var pagination1 = new PaginationModel();
                pagination1.DisplayLabel = "<<";
                pagination1.Url = "~/Admin/Analyst?page=1";
                pagination1.IsEnable = page <= 1 ? false : true;

                var pagination2 = new PaginationModel();
                pagination2.DisplayLabel = "<";
                pagination2.Url = "~/Admin/Analyst?page=" + (page - 1);
                pagination2.IsEnable = page <= 1 ? false : true;

                var pagination3 = new PaginationModel();
                pagination3.DisplayLabel = ">";
                pagination3.Url = "~/Admin/Analyst?page=" + (page + 1);
                pagination3.IsEnable = page >= maxPage ? false : true;

                var pagination4 = new PaginationModel();
                pagination4.DisplayLabel = ">>";
                pagination4.Url = "~/Admin/Analyst?page=" + (maxPage);
                pagination4.IsEnable = page >= maxPage ? false : true;

                model.PaginationModel = new List<PaginationModel>();
                model.PaginationModel.Add(pagination1);
                model.PaginationModel.Add(pagination2);

                for (int i = 0; i < maxPage; i++)
                {
                    var paginationModel = new PaginationModel();
                    paginationModel.DisplayLabel = i + 1 + "";
                    paginationModel.Url = "~/Admin/Analyst?page=" + (i + 1);
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
