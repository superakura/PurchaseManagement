using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PurchaseManagement.Controllers
{
    public class HomeController : Controller
    {
        public ViewResult Login()
        {
            return View();
        }

        //[HttpPost]
        //public ViewResult Login()
        //{
        //    return View();
        //}
    }
}