using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using tekmarket_MVC.Controllers;
using tekmarket_MVC.Models;
using System.Data;
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
            string sqlSorugum = "SELECT * FROM Musteri WHERE nik='" + inputEmail + "';";
            CodeDB cntrl = new CodeDB();
            DataTable sqlSonuc =  cntrl.SqlSorgu(sqlSorugum);
            if (sqlSonuc!=null)
            {
                Session["musteri"] = new Musteri(sqlSonuc); 
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