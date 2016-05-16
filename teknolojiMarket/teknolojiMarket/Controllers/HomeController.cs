using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teknolojiMarket.Models;
using System.Data;
namespace teknolojiMarket.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            Session["siparisSon"] = "";

            if (Session["musteri"] == null) {
                return RedirectToAction("Login", "Login");
            }
            return View();
        }
        public ActionResult Satinal(string uid) {
            SEkle(uid);
            return RedirectToAction("Checkout", "Home");
        }
        public ActionResult SEkle(string uid)
        {


            if (Session["musteri"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Musteri m = Session["musteri"] as Musteri;
                int id = m.kullaniciID;
                VeriSepet(uid.ToString());

            }
            return RedirectToAction("Index", "Home");

        }
        public ActionResult Checkout() {
            if (Session["musteri"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var m = Session["musteri"] as Musteri;
            string sqlSorugum = "SELECT A.adres_id,A.baslik,A.boylam,A.enlem,A.kid FROM Adres A, Musteri M WHERE A.kid = M.kullaniciID AND kid =" + m.kullaniciID;
            CodeDB cntrl = new CodeDB();
            DataTable sqlSonuc = cntrl.SqlSorgu(sqlSorugum);
            m.adresDoldur(sqlSonuc);
            List<Adres> adres = m.adresler;
            IEnumerable<SelectListItem> list = from k in adres
                          select new SelectListItem
                        {
                             Value = k.adres_id.ToString(),
                            Text = k.baslik,
                      };
            ViewData["dropdown"] = list;
     
            return View();
        }

        
        public ActionResult Tamamla() {
            string sqlQeuery;
            if (Session["musteri"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            Musteri m = Session["musteri"] as Musteri;
            if (m.sepet.Count != 0)
            {
                /* sepet toplamını bul */
                double tutar = 0;
                foreach (Urun u in m.sepet)
                    {
                    tutar += u.fiyat;
                    }

                if (tutar > m.bakiye)
                {
                    Session["siparisSon"] = "bakiyeniz Yetersiz";
                    return RedirectToAction("Checkout", "Home");
                }
                CodeDB cdb = new CodeDB();
                DataTable dt = new DataTable();
                /* sepetteki ürünlerden stokta yeteri kadar var mı? */
                foreach (Urun u in m.sepet) {
                    sqlQeuery = "SELECT stok FROM Urun WHERE kodu = " + u.kodu;
                    dt = cdb.SqlSorgu(sqlQeuery);
                    if (Convert.ToInt32(dt.Rows[0]["stok"].ToString()) < u.adet) {
                        Session["siparisSon"] = u.baslik + " ünününde stok yetersiz !";
                        return RedirectToAction("Checkout", "Home");
                    }
                }

                /* ürünleri stoktan düş */
                int stok;
                    foreach (Urun u in m.sepet)
                    {
                        sqlQeuery = "SELECT stok FROM Urun WHERE kodu = " + u.kodu;
                        dt = cdb.SqlSorgu(sqlQeuery);
                        stok = Convert.ToInt32(dt.Rows[0]["stok"].ToString());
                        sqlQeuery = "UPDATE Urun SET stok=" + (stok - u.adet) + " WHERE kodu = " + u.kodu;
                        cdb.SqlKomut(sqlQeuery);
                    }

                /* bakiyeyi güncelle */
                m.bakiye -= tutar;
                sqlQeuery = "UPDATE Musteri SET bakiye="+m.bakiye+"WHERE kullaniciID="+m.kullaniciID;
                cdb.SqlKomut(sqlQeuery);


                /* Sipariş tablosuna siparişi yaz */
                sqlQeuery = "INSERT INTO Siparis(tutar,durum,tarih,musteriID) VALUES(" + tutar.ToString() + ",'Tedarik Sürecinde'";
                sqlQeuery +=", '"+DateTime.Today.ToString("dd.MM.yyyy") +"',"+m.kullaniciID + ")";
                cdb.SqlKomut(sqlQeuery);

                /* sipariş kodunu al */
                sqlQeuery = "SELECT siparisKodu FROM Siparis WHERE tutar =" + tutar.ToString() + " AND musteriID = " + m.kullaniciID;
                sqlQeuery += " AND tarih='" + DateTime.Today.ToString("dd.MM.yyyy")+"'";
                dt = cdb.SqlSorgu(sqlQeuery);

                
                if (dt.Rows.Count != 0) {
                    string siparisKodu = dt.Rows[0]["siparisKodu"].ToString();
                    /* içerir tablosunu doldur */
                    foreach (Urun u in m.sepet) {
                        sqlQeuery = "INSERT INTO Icerir(sKod, adet, urun_kodu) VALUES(" + siparisKodu;
                        sqlQeuery += ", " + u.adet +", "+u.kodu + ")";
                        cdb.SqlKomut(sqlQeuery);
                    }
                    /* Sepeti temizle */
                    sqlQeuery = "DELETE FROM Sepet Where Musteri_kullaniciID=" + m.kullaniciID;
                    cdb.SqlKomut(sqlQeuery);
                    m.sepet = new List<Urun>();
                    Session["musteri"] = m;
                    Session["siparisSon"] = " Siparişiniz tamamlandı";
                }
                else
                {
                    /* data table boş geldi */
                   Session["siparisSon"] = "Bir hata ile karşılaşıldı";
                }
                

            }
            else {
               Session["siparisSon"] = "sepetiniz Boş";
            }
            return RedirectToAction("Checkout","Home");
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
        public void VeriSepet(string i)
        {
            int Ukodu;
            Ukodu = Convert.ToInt32(i);
            int adet = 1;
            DataTable sqlSonuc = new DataTable();
            Musteri m = Session["musteri"] as Musteri;

            int id1 = m.kullaniciID;

            CodeDB cntrl = new CodeDB();
            string sqlSorugum2 = "SELECT adet FROM Sepet WHERE Musteri_kullaniciID="+id1+" AND Urun_kodu="+Ukodu+" ";
            DataTable sqlSonuc2 = new DataTable();
            sqlSonuc2 = cntrl.SqlSorgu(sqlSorugum2);
            if (sqlSonuc2.Rows.Count != 0)
            {
                adet = Convert.ToInt32(sqlSonuc2.Rows[0]["adet"].ToString()) + 1;
                string sqlSorugum4 = "UPDATE Sepet SET adet=" + adet + " WHERE Musteri_kullaniciID=" + id1 + " AND Urun_kodu=" + Ukodu + " ";
                bool a = cntrl.SqlKomut(sqlSorugum4);
            }
            else {
                string sqlSorugum3 = "INSERT INTO Sepet(adet,Musteri_kullaniciID,Urun_kodu) VALUES(" + adet + "," + id1 + "," + Ukodu + " )";
                bool a1 = cntrl.SqlKomut(sqlSorugum3);
            }
            string sqlSorugum = "SELECT U.kodu,U.baslik, U.aciklama,U.marka,U.fiyat,U.resim,U.stok, S.adet FROM Urun as U, Sepet as S WHERE U.kodu=S.Urun_kodu ";
            sqlSorugum += "AND S.Musteri_kullaniciID = " + m.kullaniciID;
            sqlSonuc = cntrl.SqlSorgu(sqlSorugum);
            m.sepetDoldur(sqlSonuc);
        }

        public void SepetDelete(string i)
        {
            Session["siparisSon"] = "";
            int Ukodu;
            Ukodu = Convert.ToInt32(i);

            DataTable sqlSonuc = new DataTable();
            Musteri m = Session["musteri"] as Musteri;

            int id1 = m.kullaniciID;

            CodeDB cntrl = new CodeDB();
            string sqlSorugum = "DELETE FROM Sepet WHERE Musteri_kullaniciID=" + id1 + " AND Urun_kodu=" + Ukodu + " ";
            bool a = cntrl.SqlKomut(sqlSorugum);
            sqlSorugum = "SELECT U.kodu,U.baslik, U.aciklama,U.marka,U.fiyat,U.resim,U.stok, S.adet FROM Urun as U, Sepet as S WHERE U.kodu=S.Urun_kodu ";
            sqlSorugum += "AND S.Musteri_kullaniciID = " + m.kullaniciID;
            sqlSonuc = cntrl.SqlSorgu(sqlSorugum);
            

        }
        public ActionResult SEkle2(string uid)
        {


            if (Session["musteri"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Musteri m = Session["musteri"] as Musteri;
                int id = m.kullaniciID;
                SepetDelete(uid.ToString());
                string sqlSorugum = "SELECT U.kodu,U.baslik, U.aciklama,U.marka,U.fiyat,U.resim,U.stok, S.adet FROM Urun as U, Sepet as S WHERE U.kodu=S.Urun_kodu ";
                sqlSorugum += "AND S.Musteri_kullaniciID = " + m.kullaniciID;
                DataTable sqlSonuc = new DataTable();
                CodeDB cntrl = new CodeDB();
                sqlSonuc = cntrl.SqlSorgu(sqlSorugum);
                m.sepetDoldur(sqlSonuc);
            }
            return RedirectToAction("Checkout", "Home");

        }

        public ActionResult SiparisTakip()
        {
            if (Session["musteri"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            else
            {
                Musteri m = Session["musteri"] as Musteri;
                string sqlSorugum = "SELECT S.tutar,S.durum,S.tarih,U.baslik, S.siparisKodu FROM Siparis S,Icerir I, Urun U WHERE I.urun_kodu=U.kodu ";
                sqlSorugum += "AND S.musteriID = " + m.kullaniciID + " AND I.sKod=S.siparisKodu  ORDER BY S.siparisKodu";
                DataTable sqlSonuc = new DataTable();
                CodeDB cntrl = new CodeDB();
                sqlSonuc = cntrl.SqlSorgu(sqlSorugum);
                if (sqlSonuc.Rows.Count != 0) {

                    ViewData["siparisler"] = sqlSonuc;

                    /*
                for (int i=0;i<sqlSonuc.Rows.Count;i++) { 
                    ViewData["a"] +="<p>"+ sqlSonuc.Rows[i]["durum"].ToString();
                    ViewData["a"] +=" "+ sqlSonuc.Rows[i]["baslik"].ToString();
                    ViewData["a"] += " " + sqlSonuc.Rows[i]["tarih"].ToString();
                    ViewData["a"] += " " + sqlSonuc.Rows[i]["tutar"].ToString();
                    ViewData["a"] += " " + "</p>";
                }*/ }
            }
            return View();

            
        }
      




    }
}