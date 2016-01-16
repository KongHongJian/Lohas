using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lohas.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        [ChildActionOnly]
        public ActionResult LoadNavigation()
        {
            SiteMapNodeCollection nodes = null;

            if (SiteMap.Provider.RootNode == null)
            {
                throw  new Exception();
            }

            
            var sitemaps = GetAuthorizedMenu("");

            ViewBag.AuthorizedMenu = sitemaps;
            nodes = SiteMap.Provider.RootNode.ChildNodes;
            return PartialView("_Navigation", nodes);
        }

        private IEnumerable<string> GetAuthorizedMenu(string empty)
        {
            var result = new List<string>();
            for (int i = 0; i < 10; i++)
            {
                result.Add(i.ToString(CultureInfo.InvariantCulture));
            }
            return result;
        }
    }
}