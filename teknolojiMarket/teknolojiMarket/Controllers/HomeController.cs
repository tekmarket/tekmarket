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
        public ActionResult Checkout() {
            if (Session["musteri"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public ActionResult AdresEkle()
        {
            if (Session["musteri"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Session["htmladresekle"] = " ";
            return View();
        }
        [HttpPost]
        public ActionResult AdresEkle(string us2lon,string us2lat,string us2address)
        {
            Musteri a = Session["musteri"] as Musteri;
           
            
            CodeDB cntrl2 = new CodeDB();
            string sqlsorgum6 = "INSERT INTO Adres(baslik,enlem,boylam,kid) values ('" + us2address + "','" + us2lat + "','" + us2lon + "'," + a.kullaniciID + ")";
            if (cntrl2.SqlKomut(sqlsorgum6))
            {
                Session["htmladresekle"] = "Başarılı bir şekilde eklendi";
            }
            else { Session["htmladresekle"] = "Eklenemedi "; }
            return View();
        }
    }
}