using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using teknolojiMarket.Models;

namespace teknolojiMarket.Controllers
{
    public class AdminController : Controller
    {

        public ActionResult ekbakiye() {
            if (Session["yonetici"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult urunek() {
            if (Session["yonetici"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult guncelle()
        {
            if (Session["yonetici"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        public ActionResult urunsil()
        {
            if (Session["yonetici"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
        // GET: Adminds
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string adminNik,string adminPass)
        {
            string sqlSorugum = "SELECT * FROM Yonetici WHERE nik='" + adminNik + "' AND sifre='" + adminPass + "'";
            CodeDB cntrl = new CodeDB();
            DataTable sqlSonuc = cntrl.SqlSorgu(sqlSorugum);

            if (sqlSonuc.Rows.Count != 0)
            {
                Yonetici y = new Yonetici(sqlSonuc);
                Session["yonetici"] = y;
                return RedirectToAction("Panel", "Admin");
            }
            else {
                Session["yonetici"] = null;
            }         
            return View();
        }
        public ActionResult Panel()
        {
            if (Session["yonetici"] == null) {
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }
    }
}