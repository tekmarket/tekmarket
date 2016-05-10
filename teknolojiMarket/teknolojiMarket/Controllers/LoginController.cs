using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tekmarket_MVC.Controllers;
using tekmarket_MVC.Models;
namespace tekmarket_MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string inputEmail, string inputPassword)
        {
            if (inputEmail == "yusuf@e.com" && inputPassword == "123")
            {
                Session["musteri"] = new Musteri() { ad = "Yusuf Aga", soyad = "Kaya", nik = "yusuf", sifre = "123" };
                return RedirectToAction("Index", "Home");
            }
            else {
                Session["musteri"] = null;
            }
            return View();
        }
        public ActionResult Create() {
            return View();
        }
        public ActionResult Logout()
        {
            Session["musteri"] = null;

            return RedirectToAction("Login","Login");
        }

    }
}