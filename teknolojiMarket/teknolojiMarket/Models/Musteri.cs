using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using teknolojiMarket.Models;
namespace teknolojiMarket.Models
{
    public class Musteri
    {
        public string ad { get; set; }
        public string soyad { get; set; }
        public string sifre { get; set; }
        public string nik { get; set; }
        public string eposta { get; set; }
        public double bakiye { get; set; }
        public int kullaniciID { get; set; }
        public List<Urun> sepet = new List<Urun>();
        public List<Adres> adresler = new List<Adres>();


        public Musteri(DataTable dt) {
            if (dt.IsInitialized && dt!=null) {
                ad = dt.Rows[0]["adi"].ToString();
                soyad = dt.Rows[0]["soyadi"].ToString();
                nik = dt.Rows[0]["nik"].ToString();
                sifre = dt.Rows[0]["sifre"].ToString();
                eposta = dt.Rows[0]["eposta"].ToString();
                bakiye = Convert.ToDouble(dt.Rows[0]["bakiye"].ToString());
                kullaniciID = Convert.ToInt32(dt.Rows[0]["kullaniciID"].ToString());
            }
           
        }
        public void sepetDoldur(DataTable dt)
        {
            sepet = new List<Urun>();
            Urun u;
            for (int i=0;i<dt.Rows.Count;i++) {
                u = new Urun(dt.Rows[i]);
                sepet.Add(u);

            }
        }

        public void adresDoldur(DataTable dt) {
            Adres a;
            for (int i = 0; i < dt.Rows.Count; i++) {
                a = new Adres(dt.Rows[i]);
                adresler.Add(a);       
            }
        }
    }
   
}