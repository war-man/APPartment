﻿using APPartment.Data;
using APPartment.Models;
using APPartment.Controllers.Base;
using Microsoft.AspNetCore.Http;
using System.Linq;
using SmartBreadcrumbs.Attributes;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APPartment.Utilities.Constants.Breadcrumbs;
using System;
using System.Linq.Expressions;

namespace APPartment.Controllers
{
    public class SurveysController : BaseCRUDController<Survey>
    {
        private readonly DataAccessContext _context;

        public SurveysController(IHttpContextAccessor contextAccessor, DataAccessContext context) : base(contextAccessor, context)
        {
            _context = context;
        }

        public override Expression<Func<Survey, bool>> FilterExpression { get; set; }

        public override Expression<Func<Survey, bool>> FuncToExpression(Func<Survey, bool> f)
        {
            return x => f(x);
        }

        #region Actions
        [Breadcrumb(SurveysBreadcrumbs.All_Breadcrumb)]
        public override Task<IActionResult> Index()
        {
            ViewData["GridTitle"] = "Surveys - All";
            ViewData["Module"] = "Surveys";
            ViewData["Manage"] = true;

            FilterExpression = FuncToExpression(x => x.HouseId == CurrentHouseId);

            return base.Index();
        }

        [Breadcrumb(SurveysBreadcrumbs.Completed_Breadcrumb)]
        public async Task<IActionResult> Completed()
        {
            ViewData["GridTitle"] = "Surveys - Completed";
            ViewData["Module"] = "Surveys";
            ViewData["Manage"] = false;

            FilterExpression = FuncToExpression(x => x.HouseId == CurrentHouseId && x.IsCompleted == true);

            return await base.Index();
        }

        [Breadcrumb(SurveysBreadcrumbs.Pending_Breadcrumb)]
        public async Task<IActionResult> Pending()
        {
            ViewData["GridTitle"] = "Surveys - Pending";
            ViewData["Module"] = "Surveys";
            ViewData["Manage"] = false;

            FilterExpression = FuncToExpression(x => x.HouseId == CurrentHouseId && x.IsCompleted == false);

            return await base.Index();
        }

        public JsonResult GetPendingSurveysCount()
        {
            var pendingSurveysCount = _context.Set<Survey>().ToList().Where(x => x.HouseId == CurrentHouseId && x.IsCompleted == false).Count();
            return Json(pendingSurveysCount);
        }
        #endregion

        public override void PopulateViewData()
        {
        }
    }
}
