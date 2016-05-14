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
        public ActionResult Create(string namesignup, string surnamesignup, string usernamesignup, string emailsignup, string passwordsignup, string us2address, string us2lat, string us2lon)
        {
            string sqlSorugum51 = "Select kullaniciID From Musteri where nik='" + usernamesignup + "'";
            CodeDB cntrl2 = new CodeDB();
            DataTable sqlSonuc = cntrl2.SqlSorgu(sqlSorugum51);

            if (sqlSonuc.Rows.Count==0) { 
                string sqlSorugum2 = "INSERT INTO Musteri (adi,soyadi,nik,sifre,eposta,bakiye) values ('" + namesignup + "','" + surnamesignup + "','" + usernamesignup + "','" + passwordsignup + "','" + emailsignup + "',0)";

                bool a = cntrl2.SqlKomut(sqlSorugum2);

                string sqlSorugum5 = "Select kullaniciID From Musteri where nik='" + usernamesignup + "'";

                DataTable sqlSonuc2 = cntrl2.SqlSorgu(sqlSorugum5);
                string cevap = sqlSonuc2.Rows[0]["kullaniciID"].ToString();
                string sqlsorgum6 = "INSERT INTO Adres(baslik,enlem,boylam,kid) values ('" + us2address + "','" + us2lat + "','" + us2lon + "'," + cevap + ")";
                if (a && cntrl2.SqlKomut(sqlsorgum6))
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            return View();
         }


    }
}