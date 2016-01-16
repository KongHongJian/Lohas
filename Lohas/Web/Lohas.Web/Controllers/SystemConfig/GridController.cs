﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Lohas.Web.database;

namespace Lohas.Web.Controllers.SystemConfig
{
    public class GridController : Controller
    {
        private TSQLFundamentals2008Entities db = new TSQLFundamentals2008Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Categories_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Category> categories = db.Categories;
            DataSourceResult result = categories.ToDataSourceResult(request, c => new Category 
            {
               // Category = c.Category,
                categoryid = c.categoryid,
                categoryname = c.categoryname,
                description = c.description,
                Products = c.Products
            });

            return Json(result);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
