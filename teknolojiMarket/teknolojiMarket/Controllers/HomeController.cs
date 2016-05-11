using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teknolojiMarket.Models;
namespace teknolojiMarket.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if (Session["musteri"] == null) {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }

    }
}