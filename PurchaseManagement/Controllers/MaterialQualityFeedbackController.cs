using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PurchaseManagement.Controllers
{
    public class MaterialQualityFeedbackController : Controller
    {
        private Models.DB db = new Models.DB();

        //视图--质量反馈单添加
        public ViewResult Add()
        {
            return View();
        }

        //视图--质量反馈单接收
        public ViewResult accept()
        {
            return View();
        }

        //视图--质量反馈单回复
        public ViewResult Reply()
        {
            return View();
        }

        //视图--质量反馈单统计
        public ViewResult Statistic()
        {
            return View();
        }

        //视图--质量反馈领导审核
        public ViewResult LeaderCheck()
        {
            return View();
        }

        //直接提交或第一次保存操作
        [HttpPost]
        [ValidateInput(false)]
        public string Insert()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                var feedbackTitle = infoList["feedbackTitle"].ToString();
                var supplierName = infoList["supplierName"].ToString();
                var materialNum = infoList["materialNum"].ToString();
                var erpOrderNum = infoList["erpOrderNum"].ToString();
                var feedbackContent = infoList["feedbackContent"].ToString();

                //判断【直接提交】或【第一次保存】，数据库状态为【待审核】、【待接收】
                var clickState = infoList["clickState"].ToString();

                var materialQualityTypeID = 0;
                int.TryParse(infoList["materialQualityTypeID"].ToString(), out materialQualityTypeID);
                var person = App_Code.Commen.GetUserFromSession();

                var info = new Models.MaterialQualityFeedback();
                info.FeedbackTitle = feedbackTitle;
                info.SupplierName = supplierName;
                info.MaterialNum = materialNum;
                info.ErpOrderNum = erpOrderNum;
                info.MaterialQualityTypeID = materialQualityTypeID;
                info.FeedbackContent = feedbackContent;
                info.FeedBackState = clickState == "提交" ? "待接收" : "待提交";
                info.InputDateTime = DateTime.Now;
                info.InputPersonID = person.UserID;
                info.InputPersonPhone = person.UserPhone;
                info.InputPersonMobile = person.UserMobile;
                info.InputDeptID = person.UserDeptID;
                info.InputPersonName = person.UserName;
                db.MaterialQualityFeedback.Add(info);
                db.SaveChanges();

                var log = new Models.Log();
                log.InputDateTime = DateTime.Now;
                log.InputPersonID = person.UserID;
                log.InputPersonName = person.UserName;
                log.LogType = "物资质量反馈";
                log.LogContent = clickState == "提交" ? "提交质量反馈单" : "保存质量反馈单";
                log.LogDataID = info.ID;
                db.Log.Add(log);
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex )
            {
                return ex.Message;
            }
        }

        //修改未提交问题反馈单信息
        [HttpPost]
        [ValidateInput(false)]
        public string Update()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                var feedbackID = 0;
                int.TryParse(infoList["feedbackID"].ToString(), out feedbackID);
                var feedbackTitle = infoList["feedbackTitle"].ToString();
                var supplierName = infoList["supplierName"].ToString();
                var materialNum = infoList["materialNum"].ToString();
                var erpOrderNum = infoList["erpOrderNum"].ToString();
                var feedbackContent = infoList["feedbackContent"].ToString();
                var materialQualityTypeID = 0;
                int.TryParse(infoList["materialQualityTypeID"].ToString(), out materialQualityTypeID);

                //判断【直接提交】或【第一次保存】，数据库状态为【待提交】、【待接收】
                var clickState = infoList["clickState"].ToString();
                var person = App_Code.Commen.GetUserFromSession();

                var info = db.MaterialQualityFeedback.Where(w=>w.ID==feedbackID&&w.FeedBackState=="待提交").FirstOrDefault();
                info.FeedbackTitle = feedbackTitle;
                info.SupplierName = supplierName;
                info.MaterialNum = materialNum;
                info.ErpOrderNum = erpOrderNum;
                info.FeedbackContent = feedbackContent;
                info.MaterialQualityTypeID = materialQualityTypeID;
                info.FeedBackState = clickState== "提交" ? "待接收" : "待提交";
                info.InputDateTime = DateTime.Now;
                info.InputPersonID = person.UserID;
                info.InputPersonPhone = person.UserPhone;
                info.InputPersonMobile = person.UserMobile;
                info.InputDeptID = person.UserDeptID;
                info.InputPersonName = person.UserName;

                if (clickState == "提交")
                {
                    var log = new Models.Log();
                    log.InputDateTime = DateTime.Now;
                    log.InputPersonID = person.UserID;
                    log.InputPersonName = person.UserName;
                    log.LogType = "物资质量反馈";
                    log.LogContent = "提交质量反馈单";
                    log.LogDataID = feedbackID;
                    db.Log.Add(log);
                }
                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //删除接收回退的反馈单
        public string Del()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                var feedbackID = 0;
                int.TryParse(infoList["feedbackID"].ToString(), out feedbackID);

                var info = db.MaterialQualityFeedback
                    .Where(w => w.ID == feedbackID && (w.FeedBackState == "接收回退"||w.FeedBackState== "待提交"))
                    .FirstOrDefault();
                db.MaterialQualityFeedback.Remove(info);

                var log = db.Log.Where(w => w.LogType == "物资质量反馈" && w.LogDataID == feedbackID).FirstOrDefault();
                if (log!=null)
                {
                    db.Log.Remove(log);
                }

                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //获得反馈单列表
        public JsonResult GetList()
        {
            try
            {
                var limit = Convert.ToInt32(Request.Form["limit"]);
                var offset = Convert.ToInt32(Request.Form["offset"]);
                var feedbackTitle = Request.Form["feedbackTitle"];
                var materialQualityState = Request.Form["materialQualityState"];
                var materialQualityTypeID = 0;
                int.TryParse(Request.Form["materialQualityTypeID"],out materialQualityTypeID);
                var result = from f in db.MaterialQualityFeedback
                             join t in db.MaterialQualityType on f.MaterialQualityTypeID equals t.MaterialQualityTypeID
                             join d in db.DeptInfo on f.InputDeptID equals d.DeptID
                             orderby f.InputDateTime descending
                             select new
                             {
                                 f.ID,
                                 f.FeedbackTitle,
                                 f.SupplierName,
                                 f.ErpOrderNum,
                                 f.MaterialNum,
                                 f.FeedbackContent,
                                 f.FeedBackState,
                                 f.InputDateTime,
                                 f.InputPersonName,
                                 f.InputPersonMobile,
                                 f.InputPersonPhone,
                                 f.MaterialQualityTypeID,
                                 f.ReplyDateTime,
                                 f.ReplyPersonName,
                                 f.CheckDateTime,
                                 f.CheckPersonName,
                                 t.MaterialQualityTypeName,
                                 d.DeptName
                             };

                if (!string.IsNullOrEmpty(feedbackTitle))
                {
                    result = result.Where(w => w.FeedbackTitle.Contains(feedbackTitle));
                }
                if (!string.IsNullOrEmpty(materialQualityState))
                {
                    result = result.Where(w => w.FeedBackState==materialQualityState);
                }
                if (materialQualityTypeID != 0)
                {
                    result = result.Where(w => w.MaterialQualityTypeID== materialQualityTypeID);
                }
                return Json(new { total = result.Count(), rows = result.Skip(offset).Take(limit).ToList() });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}