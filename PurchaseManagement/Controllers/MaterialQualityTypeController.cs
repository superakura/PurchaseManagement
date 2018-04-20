using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PurchaseManagement.Controllers
{
    public class MaterialQualityTypeController : Controller
    {
        private Models.DB db = new Models.DB();

        //视图--质量反馈单类别管理
        public ViewResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            var typeName = Request.Form["searchTypeName"];
            var list = db.MaterialQualityType.AsQueryable();
            if (typeName!=string.Empty)
            {
                list = list.Where(w => w.MaterialQualityTypeName.Contains(typeName));
            }
            return Json(list.ToList());
        }

        public string Insert()
        {
            try
            {
                var typeName = Request.Form["MaterialQualityTypeName"];
                var typeInfo = new Models.MaterialQualityType();
                typeInfo.MaterialQualityTypeName = typeName;
                typeInfo.InputPerson = App_Code.Commen.GetUserFromSession().UserID;
                typeInfo.InputDateTime = DateTime.Now;
                db.MaterialQualityType.Add(typeInfo);
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Update()
        {
            try
            {
                var typeName = Request.Form["MaterialQualityTypeName"];
                var typeID = 0;
                int.TryParse(Request.Form["MaterialQualityTypeID"],out typeID);
                var typeInfo = db.MaterialQualityType.Find(typeID);
                typeInfo.MaterialQualityTypeName = typeName;
                typeInfo.InputPerson = App_Code.Commen.GetUserFromSession().UserID;
                typeInfo.InputDateTime = DateTime.Now;
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string Del()
        {
            try
            {
                var typeID = 0;
                int.TryParse(Request.Form["MaterialQualityTypeID"], out typeID);
                db.MaterialQualityType.Remove(db.MaterialQualityType.Find(typeID));
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost]
        public JsonResult GetListAll()
        {
            return Json(db.MaterialQualityType.ToList());
        }
    }
}