using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Data;

namespace teknolojiMarket.Models
{
    public class CodeDB
    {
        //variable connection
        protected SqlConnection con;
        private bool state = false;
        //open connection

        public bool isOpen() {
            return state;
        }

        public bool Open(string Connection = "DefaultConnection") {
            con = new SqlConnection(@WebConfigurationManager.ConnectionStrings[Connection].ToString());

            try
            {
                bool b = true;
                if (con.State.ToString() != "Open") {
                    con.Open();
                }
                state = true;
                return b;
            }
            catch (SqlException ex) {
                return false;
            }

        }

        //close Connection
        public bool Close() {
            try
            {
                con.Close();
                state = false;
                return true;
            }
            catch (Exception ex)
            {
                state = false;
                return false;
            }
        }

        public bool SqlKomut(string komut) {
            if (!state)
            {
                Open();
            }
            SqlCommand sorgu = new SqlCommand(komut, con);
            try
            {
                sorgu.ExecuteNonQuery();
                Close();
                return true;
            }
            catch (Exception ex) {
                return false;     
            } 
                    
        }

        public DataTable SqlSorgu(string komut) {
            DataTable sonuc = new DataTable();
            if (!state) {
                Open();
            }
            try
            {
                SqlDataAdapter adptr = new SqlDataAdapter(komut, con);
                adptr.Fill(sonuc);
            }
            catch (SqlException ex) {
                sonuc = null;
            }
            
            
            Close();
            return sonuc;
        }
          
    }
}