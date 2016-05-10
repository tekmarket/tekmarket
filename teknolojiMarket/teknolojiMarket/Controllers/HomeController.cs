using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tekmarket_MVC.Models;
namespace tekmarket_MVC.Controllers
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