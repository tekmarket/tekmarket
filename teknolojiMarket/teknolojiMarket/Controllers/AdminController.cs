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
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Save")]
        public ActionResult Save() {
            return View();
        }

        [HttpPost]
        [MultipleButton(Name = "action", Argument = "Cancel")]
        public ActionResult Cancel() {
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
            return View();
        }
    }
}