﻿using System;
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
        [HttpPost]
        public ActionResult ekbakiye(string kullaniciID, string eklenecek)
        {
            CodeDB cdb = new CodeDB();
            DataTable dt;
            Musteri m;
            if (Session["yonetici"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }

            string sqlQuery;
            if (Request.Form["btgetir"] != null)
            {
                ViewData["isguncel"]="";
                sqlQuery = "select * from  Musteri where nik = '" + kullaniciID + "'";
                dt = cdb.SqlSorgu(sqlQuery);
                if (dt.Rows.Count == 0)
                {
                    Session["kullanici"] = null;
                    ViewData["kbul"] = "kullanıcı bulunamadı !";
                }
                else {
                    ViewData["kbul"] = "";
                   m = new Musteri(dt);
                    Session["kullanici"] = m;
                }

            }
            else if(Request.Form["btek"] != null) {

                int yenibakiye;
                if (Session["kullanici"] != null && Int32.TryParse(eklenecek,out yenibakiye)) {
                    m = Session["kullanici"] as Musteri;
                    m.bakiye = yenibakiye;
                    sqlQuery = "UPDATE Musteri SET bakiye =" +m.bakiye + "where kullaniciID="+m.kullaniciID;
                    Session["kullanici"] = m;
                    if (cdb.SqlKomut(sqlQuery))
                    {
                        ViewData["isguncel"] = "bakiye güncellendi";
                    }
                    else {
                        ViewData["isguncel"] = "işlem gerçekleştrilemedi !";
                    }

                }
                else
                {
                    ViewData["isguncel"] = "işlem gerçekleştrilemedi !";
                }
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

        [HttpPost]
        public ActionResult guncelle(string tbdurum,string tbSkod)
        {
            CodeDB cdb = new CodeDB();
            DataTable dt;
            Siparis s;
            if (Session["yonetici"] == null)
            {
                return RedirectToAction("Index", "Admin");
            }
            if (Request.Form["btdgetir"] != null) {
                ViewData["isupdurum"] = "";
                string sqlQuery = "SELECT * from Siparis Where siparisKodu=" + tbSkod;
                dt = cdb.SqlSorgu(sqlQuery);
                if (dt.Rows.Count != 0)
                {
                    @ViewData["sbul"] = "";
                    s = new Siparis(dt);
                    Session["siparis"] = s;
                }
                else {
                    @ViewData["sbul"] = "sipariş bulunamadı";
                    
                    Session["siparis"] = null;
                }
            } else if (Request.Form["btdgunc"] != null) {
                if (Session["siparis"] != null)
                {
                    ViewData["isupdurum"] = "";
                    s = Session["siparis"] as Siparis;
                    string sqlQuery = "UPDATE Siparis SET durum ='" + tbdurum + "' WHERE siparisKodu="+ tbSkod;
                    if (cdb.SqlKomut(sqlQuery))
                    {
                        ViewData["isupdurum"] = "işlem Başarılı";
                        s.durum = tbdurum;
                        Session["siparis"] = s;

                    }
                    else {
                        ViewData["isupdurum"] = "işlem başarısız";
                    }
                }
                else {
                    @ViewData["isupdurum"] = "işlem başarısız";
                }
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

        [HttpPost]
        public ActionResult urunsil(string urunid) {
            string sqlQuery = "DELETE FROM Urun WHERE kodu = " + urunid;
            CodeDB cdb = new CodeDB();
            if (cdb.SqlKomut(sqlQuery))
            {
                ViewBag.silindi = "ürün silindi ";
            }
            else {
                ViewBag.silindi = "Böyle bir ürün yok";
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
        public ActionResult Logout() {
            Session["yonetici"] = null;
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult Upload(HttpPostedFileBase file,string tbBaslik,string taAciklama, string tbMarka,string tbFiyat,string tbStok) {
            string sqlQuery = "INSERT INTO Urun(baslik, aciklama, marka,fiyat, resim, stok) ";
            sqlQuery += "VALUES('" +tbBaslik+"','"+taAciklama+"','"+tbMarka+"'," + Convert.ToDouble(tbFiyat)+",'x',"+Convert.ToInt32(tbStok)+")";
            CodeDB cdb = new CodeDB();
            if (cdb.SqlKomut(sqlQuery))
            {
                sqlQuery = "Select kodu From Urun WHERE baslik='" + tbBaslik + "' AND resim='x' ";
                DataTable dt = new DataTable();
                dt = cdb.SqlSorgu(sqlQuery);
                if (dt.Rows.Count != 0)
                {
                    string urunid = dt.Rows[0]["kodu"].ToString();
                    string yol = "~/Content/img/products/" + urunid + file.FileName;
                    string path = Server.MapPath(yol);
                    file.SaveAs(path);
                    sqlQuery = "UPDATE Urun SET resim='" + yol + "' WHERE kodu="+urunid;
                    cdb.SqlKomut(sqlQuery);
                    ViewBag.isUploaded = "Ürün Kaydedildi";


                }
                else {
                    ViewBag.isUploaded = "Ürün kaydedilemedi";
                }
            
            }
            else {
                ViewBag.isUploaded = "Ürün kaydedilemedi";
            }
            
            return RedirectToAction("urunek", "Admin");
        }
    }
}