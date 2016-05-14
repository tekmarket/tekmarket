using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teknolojiMarket.Models;
using System.Data;


namespace teknolojiMarket.Controllers
{//push icin
    public class CreateController : Controller
    {
        // GET: Create
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(string namesignup, string surnamesignup, string usernamesignup, string emailsignup, string passwordsignup)
         {
             string sqlSorugum2 = "INSERT INTO Musteri (adi,soyadi,nik,sifre,eposta,bakiye) values ('" + namesignup + "','" + surnamesignup + "','" + usernamesignup + "','" + passwordsignup + "','" + emailsignup + "',0)";
             CodeDB cntrl2 = new CodeDB();
            if (cntrl2.SqlKomut(sqlSorugum2)) {
                return RedirectToAction("Index", "Home");
            }


             return View();
         }


    }
}