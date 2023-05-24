using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace test1.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}