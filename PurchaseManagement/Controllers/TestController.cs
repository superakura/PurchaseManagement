using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PurchaseManagement.Controllers
{
    public class TestController : Controller
    {
        private Models.DB db = new Models.DB();

        public ViewResult Form()
        {
            return View();
        }

        public ViewResult Chat()
        {
            return View();
        }

        public ViewResult AngularJsCrud()
        {
            return View();
        }

        public JsonResult List()
        {
            return Json(db.Crud.ToList(), JsonRequestBehavior.AllowGet);
        }
    }
}