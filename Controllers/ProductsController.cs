﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test1.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Categories()
        {
            return View();
        }
    }
}