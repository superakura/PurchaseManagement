using PurchaseManagement.App_Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PurchaseManagement.Controllers
{
    public class UserInfoController : Controller
    {
        private Models.DB db = new Models.DB();

        public ViewResult Index()
        {
            return View();
        }

        public JsonResult GetList()
        {
            var limit = 0;
            int.TryParse(Request.Form["limit"], out limit);

            var offset = 0;
            int.TryParse(Request.Form["offset"], out offset);

            var userName = Request.Form["userName"];
            var userNum = Request.Form["userNum"];

            var deptID = 0;
            int.TryParse(Request.Form["deptID"], out deptID);

            var userID = Commen.GetUserFromSession().UserID;
            var userDept = db.UserDept.Where(w => w.UserID == userID).Select(s => s.DeptID).ToArray();

            var result = (from u in db.UserInfo
                          join d in db.DeptInfo on u.UserDeptID equals d.DeptID
                          where u.UserState == 0
                          //where userDept.Contains(d.DeptID) & u.UserState == 0
                          orderby u.UserNum
                          select new
                          {
                              u.UserDuty,
                              u.UserName,
                              u.UserID,
                              u.UserNum,
                              u.UserPhone,
                              u.UserEmail,
                              d.DeptName,
                              u.UserDeptID,
                              u.UserMobile,
                              u.UserRemark
                          });
            //if (deptID != 1)
            //{
            //    var count = db.UserInfo.Where(w => w.UserDeptID == deptID).Count();
            //    if (count == 0)
            //    {
            //        result = result.Where(w => db.DeptInfo.Where(t => t.DeptFatherID == deptID).Select(s => s.DeptID).Contains(w.UserDeptID));
            //    }
            //    else
            //    {
            //        result = result.Where(w => w.UserDeptID == deptID);
            //    }
            //}

            if (!string.IsNullOrEmpty(userName))
            {
                result = result.Where(w => w.UserName.Contains(userName));
            }
            if (!string.IsNullOrEmpty(userNum))
            {
                result = result.Where(w => w.UserNum.Contains(userNum));
            }
            return Json(new { total = result.Count(), rows = result.Skip(offset).Take(limit).ToList() });
        }
    }
}