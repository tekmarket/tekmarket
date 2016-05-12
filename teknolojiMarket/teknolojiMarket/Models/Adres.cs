using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace teknolojiMarket.Models
{
    public class Adres
    {
        public string baslik { get; set; }
        public string enlem { get; set; }
        public string boylam { get; set; }
        public int adres_id { get; set; }
        public int kid { get; set; }

        public Adres(DataRow dtr) {
            baslik = dtr["baslik"].ToString();
            enlem = dtr["enlem"].ToString();
            boylam = dtr["boylam"].ToString();
            adres_id = Convert.ToInt32(dtr["adres_id"].ToString());
            kid = Convert.ToInt32(dtr["kid"].ToString());
        }
    }
}