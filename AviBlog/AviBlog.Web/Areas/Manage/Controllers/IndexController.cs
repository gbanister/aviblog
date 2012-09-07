﻿namespace AviBlog.Web.Areas.Manage.Controllers
{
    using System.Web.Mvc;

    using AviBlog.Core.ActionFilters;
    using AviBlog.Core.Services.Search;

    public class IndexController : Controller
    {
        private readonly ISearchIndexService _searchIndexService;

        public IndexController(ISearchIndexService searchIndexService)
        {
            _searchIndexService = searchIndexService;
        }

        [AdminAuthorize]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AdminAuthorize]
        public JsonResult Reindex()
        {
            var errors = _searchIndexService.RebuildIndex();
            if (errors != null && errors.Count > 0) return Json(new { Status = errors });
            var status = new { Status = new[] { "Success" } };
            return Json(status);
        }
    }
}