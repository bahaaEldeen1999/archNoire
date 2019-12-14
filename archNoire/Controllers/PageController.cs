using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace archNoire.Controllers
{
    public class PageController : Controller
    {
        private int pageID;
        // GET: Page
        public ActionResult Index(int id)
        {
            pageID = id;
            return View();
        }
    }
}