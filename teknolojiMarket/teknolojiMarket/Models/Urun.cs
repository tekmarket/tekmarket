using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace teknolojiMarket.Models
{
    public class Urun
    {
        public int kodu { get; set; }
        public string baslik { get; set; }
        public string aciklama { get; set; }
        public string marka { get; set; }
        public double fiyat { get; set; }
        public string resim { get; set; }
        public int stok { get; set; }
        
        public int adet { get; set; } //sepette kaç adet var ?

        public Urun(System.Data.DataRow dtc) {
            kodu =Convert.ToInt32(dtc["kodu"].ToString());
            baslik = dtc["baslik"].ToString();
            aciklama = dtc["aciklama"].ToString();
            marka = dtc["marka"].ToString();
            fiyat = Convert.ToDouble(dtc["fiyat"].ToString());
            resim = dtc["resim"].ToString();
            stok = Convert.ToInt32(dtc["stok"].ToString());
            adet = Convert.ToInt32(dtc["adet"].ToString());
        }
    }
}