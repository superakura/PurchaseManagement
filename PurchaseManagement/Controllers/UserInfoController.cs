using Newtonsoft.Json;
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

        [HttpPost]
        public string Insert()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                #region 添加用户基本信息
                var userName = infoList["userName"].ToString();
                var userNum = infoList["userNum"].ToString();
                var userEmail = infoList["userEmail"].ToString();
                var deptID = 0;
                int.TryParse(infoList["deptID"].ToString(), out deptID);
                var userDuty = infoList["userDuty"].ToString();
                var userPhone = infoList["userPhone"].ToString();
                var userMobile = infoList["userMobile"].ToString();
                var userRemark = infoList["userRemark"].ToString();

                Models.UserInfo userInfo = new Models.UserInfo();
                userInfo.UserName = userName;
                userInfo.UserNum = userNum;
                userInfo.UserDuty = userDuty;
                userInfo.UserState = 0;
                userInfo.UserDeptID = deptID;
                userInfo.UserEmail = userEmail==string.Empty ? null : userEmail;
                userInfo.UserPhone = userPhone;
                userInfo.UserRemark = userRemark;
                userInfo.UserMobile = userMobile;

                db.UserInfo.Add(userInfo);
                db.SaveChanges();
                #endregion

                #region 删除用户已经存在的权限和管理部门
                var userDeptExist = db.UserDept.Where(w => w.UserID == userInfo.UserID).ToList();
                if (userDeptExist.Count != 0)
                {
                    db.UserDept.RemoveRange(userDeptExist);
                }
                var userRoleExist = db.UserRole.Where(w => w.UserID == userInfo.UserID).ToList();
                if (userRoleExist != null)
                {
                    db.UserRole.RemoveRange(userRoleExist);
                }
                db.SaveChanges();
                #endregion

                #region 添加用户所拥有的角色
                Dictionary<string, object> roleList =
                    JsonConvert.DeserializeObject<Dictionary<String, Object>>(infoList["roleList"].ToString());
                foreach (var item in roleList)
                {
                    Models.UserRole userRole = new Models.UserRole();
                    var roleID = 0;
                    int.TryParse(item.Value.ToString(), out roleID);
                    userRole.RoleID = roleID;
                    userRole.UserID = userInfo.UserID;
                    db.UserRole.Add(userRole);
                    db.SaveChanges();
                }
                #endregion

                #region 添加用户管理的部门
                Dictionary<string, object> deptList =
                    JsonConvert.DeserializeObject<Dictionary<String, Object>>(infoList["deptList"].ToString());
                foreach (var item in deptList)
                {
                    Models.UserDept userDept = new Models.UserDept();
                    var deptIDManagerment = 0;
                    int.TryParse(item.Value.ToString(), out deptIDManagerment);
                    userDept.DeptID = deptIDManagerment;
                    userDept.UserID = userInfo.UserID;
                    db.UserDept.Add(userDept);
                    db.SaveChanges();
                }
                #endregion
                return "ok";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}