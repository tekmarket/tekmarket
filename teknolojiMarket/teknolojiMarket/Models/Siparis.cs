using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using teknolojiMarket.Models;
using System.Data;
namespace teknolojiMarket.Models
{
    public class Siparis
    {
        public int siparisKodu { get; set; }
        public double tutar { get; set; }
        public string durum { get; set; }
        public string tarih { get; set; }
        public int musteriID { get; set; }

        public List<Urun> urunler = new List<Urun>();

        public Siparis() {

        }
        public Siparis(DataTable veri) {
            siparisKodu = Convert.ToInt32(veri.Rows[0]["siparisKodu"].ToString() );
            tutar = Convert.ToDouble(veri.Rows[0]["tutar"].ToString());
            durum = veri.Rows[0]["durum"].ToString();
            tarih = veri.Rows[0]["tarih"].ToString();
            musteriID =Convert.ToInt32(veri.Rows[0]["musteriID"].ToString());
        }

       
    }
}