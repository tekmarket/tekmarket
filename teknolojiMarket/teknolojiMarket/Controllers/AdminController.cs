using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace teknolojiMarket.Controllers
{
    public class AdminController : Controller
    {
        // GET: Adminds
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Panel()
        {
            return View();
        }
    }
}