using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace tekmarket_MVC.Models
{
    public class CodeDB
    {
        //variable connection
        protected SqlConnection con;

        //open connection
        public bool Open(string Connection = "DefaultConnection") {
            con = new SqlConnection(@WebConfigurationManager.ConnectionStrings[Connection].ToString());
           
            try
            {
                bool b = true;
                if (con.State.ToString() != "Open") {
                    con.Open();
                }
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
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}