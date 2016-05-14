using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace teknolojiMarket.Models
{
    public class Yonetici
    {
        public string adi { get; set; }
        public string soyadi { get; set; }
        public string nik { get; set; }
        public string sifre { get; set; }
        public string eposta { get; set; }
        public string tcno { get; set; }
        public int kullaniciID { get; set; }
        public Yonetici(DataTable dt)
        {
            if (dt.IsInitialized && dt != null)
            {
                adi = dt.Rows[0]["adi"].ToString();
                soyadi = dt.Rows[0]["soyadi"].ToString();
                nik = dt.Rows[0]["nik"].ToString();
                sifre = dt.Rows[0]["sifre"].ToString();
                eposta = dt.Rows[0]["eposta"].ToString();
                tcno = dt.Rows[0]["tcno"].ToString();
                kullaniciID = Convert.ToInt32(dt.Rows[0]["kullaniciID"].ToString());
            }

        }

    }
}