using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Lohas.Web.database;

namespace Lohas.Web.Controllers
{
    public class SystemConfigController : Controller
    {
        private TSQLFundamentals2008Entities db = new TSQLFundamentals2008Entities();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Systems_SiteMap_Read([DataSourceRequest]DataSourceRequest request)
        {
            IQueryable<Systems_SiteMap> systems_sitemap = db.Systems_SiteMap;
            DataSourceResult result = systems_sitemap.ToDataSourceResult(request, c => new Systems_SiteMap 
            {
                ID = c.ID,
                Title = c.Title,
                Description = c.Description,
                Url = c.Url,
                UrlController = c.UrlController,
                UrlAction = c.UrlAction,
                ParentID = c.ParentID
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
