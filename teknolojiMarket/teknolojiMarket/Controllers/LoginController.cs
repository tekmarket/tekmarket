using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teknolojiMarket.Models;
using System.Data;
namespace teknolojiMarket.Controllers
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
            string sqlSorugum = "SELECT * FROM Musteri WHERE nik='" + inputEmail + "' AND sifre='" + inputPassword + "'" ;
            CodeDB cntrl = new CodeDB();
            DataTable sqlSonuc =  cntrl.SqlSorgu(sqlSorugum);
            if (sqlSonuc!=null)
            {
                Musteri m = new Musteri(sqlSonuc);
                sqlSorugum = "SELECT U.kodu,U.baslik, U.aciklama,U.marka,U.fiyat,U.resim,U.stok, S.adet FROM Urun as U, Sepet as S WHERE U.kodu=S.Urun_kodu ";
                sqlSorugum += "AND S.Musteri_kullaniciID = "+m.kullaniciID;
                sqlSonuc = cntrl.SqlSorgu(sqlSorugum);
                m.sepetDoldur(sqlSonuc);
                sqlSorugum = "SELECT A.adres_id,A.baslik,A.boylam,A.enlem,A.kid FROM Adres A, Musteri M WHERE A.kid = M.kullaniciID AND kid =" + m.kullaniciID ;
                sqlSonuc = cntrl.SqlSorgu(sqlSorugum);
                m.adresDoldur(sqlSonuc);
                Session["musteri"] = m;
                return RedirectToAction("Index", "Home");
            }
            else {
                Session["musteri"] = null;
            }
            return View();
        }
        
        


        public ActionResult Logout()
        {
            Session["musteri"] = null;

            return RedirectToAction("Login","Login");
        }

    }
}