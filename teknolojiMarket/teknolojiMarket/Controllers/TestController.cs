﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using teknolojiMarket.Models;

namespace teknolojiMarket.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        protected CodeDB DB = new CodeDB();
        public ActionResult Index()
        {
            bool i = DB.Open();
            if (i)
            {
                return Content(i.ToString());
            }
            else {
                return Content(i.ToString());
            }
            return View();
        }
    }
}