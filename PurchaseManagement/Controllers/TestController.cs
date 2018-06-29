using Newtonsoft.Json;
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

        public ViewResult NgBsTable()
        {
            return View();
        }

        public ViewResult NgBsTableDB()
        {
            return View();
        }

        public ViewResult NgBsTableDBPost()
        {
            return View();
        }

        public JsonResult List()
        {
            int.TryParse(Request.Form["limit"], out var limit);
            int.TryParse(Request.Form["offset"], out var offset);
            var nameSearch = Request.Form["nameSearch"].ToString();
            var dutySearch = Request.Form["dutySearch"].ToString();

            var result = db.Crud.AsEnumerable();
            if (!string.IsNullOrEmpty(nameSearch))
            {
                result = result.Where(w => w.Name.Contains(nameSearch));
            }
            if (!string.IsNullOrEmpty(dutySearch))
            {
                result = result.Where(w => w.Duty.Contains(dutySearch));
            }

            return Json(new { total = result.Count(), rows = result.Skip(offset).Take(limit).ToList() },JsonRequestBehavior.AllowGet);
        }

        public string GetTest()
        {
            return "okxxx";
        }

        public string GetTestAjax()
        {
            var infoList =
  JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));
            var userNum = infoList["test"].ToString();
            return "ok" + userNum;
        }

        [HttpPost]
        public string CrudInsert()
        {
            try
            {
                var crud = new Models.Crud();
                crud.Name = Request.Form["Name"].ToString();
                crud.Sex = Request.Form["Sex"].ToString();
                var age = 0;
                int.TryParse(Request.Form["Age"],out age);
                crud.Age = age;
                crud.Duty = Request.Form["Duty"].ToString();
                crud.Phone = Request.Form["Phone"].ToString();
                crud.Email= Request.Form["Email"].ToString();
                crud.Remark = Request.Form["Remark"].ToString();
                db.Crud.Add(crud);
                db.SaveChanges();

                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}