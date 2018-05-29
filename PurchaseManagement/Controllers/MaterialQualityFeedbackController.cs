using Aspose.Words;
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

        #region view
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
        #endregion

        #region crud
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
                var personFatherDeptID = db.DeptInfo.Where(w => w.DeptID == person.UserDeptID).Select(s => s.DeptFatherID).FirstOrDefault();

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
                info.InputDeptID = personFatherDeptID;
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
            catch (Exception ex)
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

                var info = db.MaterialQualityFeedback.Where(w => w.ID == feedbackID && w.FeedBackState == "待提交").FirstOrDefault();
                info.FeedbackTitle = feedbackTitle;
                info.SupplierName = supplierName;
                info.MaterialNum = materialNum;
                info.ErpOrderNum = erpOrderNum;
                info.FeedbackContent = feedbackContent;
                info.MaterialQualityTypeID = materialQualityTypeID;
                info.FeedBackState = clickState == "提交" ? "待接收" : "待提交";
                info.InputDateTime = DateTime.Now;
                info.InputPersonID = person.UserID;
                info.InputPersonPhone = person.UserPhone;
                info.InputPersonMobile = person.UserMobile;
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
                    .Where(w => w.ID == feedbackID && (w.FeedBackState == "接收回退" || w.FeedBackState == "待提交"))
                    .FirstOrDefault();
                db.MaterialQualityFeedback.Remove(info);

                var log = db.Log.Where(w => w.LogType == "物资质量反馈" && w.LogDataID == feedbackID).FirstOrDefault();
                if (log != null)
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
                int.TryParse(Request.Form["limit"], out var limit);
                int.TryParse(Request.Form["offset"], out var offset);
                var feedbackTitle = Request.Form["feedbackTitle"];//反馈单名称
                var materialQualityState = Request.Form["materialQualityState"];//反馈单状态
                int.TryParse(Request.Form["materialQualityTypeID"], out var materialQualityTypeID);//物资质量问题类型ID

                var userInfo = App_Code.Commen.GetUserFromSession();

                //用户所属部门的父级部门ID
                var userFatherID = db.DeptInfo
                    .Where(w => w.DeptID == userInfo.UserDeptID)
                    .Select(s => s.DeptFatherID)
                    .FirstOrDefault();

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
                                 f.FeedBackState,
                                 f.InputDeptID,
                                 f.InputDateTime,
                                 f.InputPersonName,
                                 f.InputPersonMobile,
                                 f.InputPersonPhone,
                                 f.MaterialQualityTypeID,
                                 f.AcceptDateTime,
                                 f.AcceptPersonName,
                                 f.ReplyDateTime,
                                 f.ReplyPersonName,
                                 f.ReplyPersonID,
                                 f.CheckDateTime,
                                 f.CheckPersonName,
                                 f.AppraiseBillState,
                                 t.MaterialQualityTypeName,
                                 d.DeptName
                             };
                if (User.IsInRole("物资质量反馈--领导审核"))
                {
                    List<string> list = new List<string>();
                    //包含：回复完毕、审核通过、考核单已生成
                    list.Add("回复完毕");
                    list.Add("审核通过");
                    list.Add("考核单已生成");
                    result = result.Where(w => list.Contains(w.FeedBackState));
                }
                if (User.IsInRole("物资质量反馈--添加"))
                {
                    //List<string> list = new List<string>();
                    ////包含：待提交、待接收、接收回退、审核通过
                    //list.Add("待提交");
                    //list.Add("待接收");
                    //list.Add("接收回退");
                    //list.Add("审核通过");
                    //result = result.Where(w => list.Contains(w.FeedBackState)&&w.InputDeptID==userFatherID);
                    result = result.Where(w => w.InputDeptID == userFatherID);
                }
                if (User.IsInRole("物资质量反馈接收"))
                {
                    result = result.Where(w => w.FeedBackState != "待提交");
                }
                if (User.IsInRole("物资质量反馈--回复"))
                {
                    List<string> list = new List<string>();
                    //不包含：待提交、待接收、接收回退、回复回退
                    list.Add("待提交");
                    list.Add("待接收");
                    list.Add("接收回退");
                    list.Add("回复回退");
                    result = result.Where(w => !list.Contains(w.FeedBackState) && w.ReplyPersonID == userInfo.UserID);
                }
                if (!string.IsNullOrEmpty(feedbackTitle))
                {
                    result = result.Where(w => w.FeedbackTitle.Contains(feedbackTitle));
                }
                if (!string.IsNullOrEmpty(materialQualityState))
                {
                    result = result.Where(w => w.FeedBackState == materialQualityState);
                }
                if (materialQualityTypeID != 0)
                {
                    result = result.Where(w => w.MaterialQualityTypeID == materialQualityTypeID);
                }
                return Json(new { total = result.Count(), rows = result.Skip(offset).Take(limit).ToList() });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        [HttpPost]
        public JsonResult GetContent()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));
                int.TryParse(infoList["id"].ToString(), out var infoID);

                var userInfo = App_Code.Commen.GetUserFromSession();

                var info = db.MaterialQualityFeedback.Find(infoID);
                if (User.IsInRole("物资质量反馈--添加"))
                {
                    if (info.FeedBackState != "审核通过")
                    {
                        info.ReplyContent = "";
                    }
                }
                return Json(new { FeedbackContent = info.FeedbackContent, ReplyContent = info.ReplyContent });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        //获取物资质量反馈回复人员列表
        [HttpPost]
        public JsonResult GetReplyPerson()
        {
            var result = from u in db.UserInfo
                         join r in db.UserRole on u.UserID equals r.UserID
                         join a in db.RoleAuthority on r.RoleID equals a.RoleID
                         where a.AuthorityID == 27//物资质量反馈回复权限
                         select new { u.UserName, u.UserID };
            return Json(result);
        }

        //接收通过
        [HttpPost]
        public string AcceptOk()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                int.TryParse(infoList["replyPersonID"].ToString(), out var replyPersonID);
                int.TryParse(infoList["feedBackID"].ToString(), out var feedBackID);

                var replyPerson = db.UserInfo.Where(w => w.UserID == replyPersonID).FirstOrDefault();
                var person = App_Code.Commen.GetUserFromSession();
                var info = db.MaterialQualityFeedback.Find(feedBackID);
                info.FeedBackState = "接收完成";
                info.AcceptDateTime = DateTime.Now;
                info.AcceptPersonID = person.UserID;
                info.AcceptPersonName = person.UserName;
                info.ReplyPersonID = replyPersonID;
                info.ReplyPersonName = replyPerson.UserName;

                var log = new Models.Log();
                log.InputDateTime = DateTime.Now;
                log.InputPersonID = person.UserID;
                log.InputPersonName = person.UserName;
                log.LogType = "物资质量反馈";
                log.LogContent = "接收物资质量反馈单--指定回复人【" + replyPerson.UserName + "】";
                log.LogDataID = info.ID;
                db.Log.Add(log);

                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //接收回退
        [HttpPost]
        public string AcceptBack()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                int.TryParse(infoList["feedBackID"].ToString(), out var feedBackID);
                var backReason = infoList["backReason"].ToString();

                var person = App_Code.Commen.GetUserFromSession();
                var info = db.MaterialQualityFeedback.Find(feedBackID);
                info.FeedBackState = "接收回退";
                //info.AcceptDateTime = DateTime.Now;
                //info.AcceptPersonID = person.UserID;
                //info.AcceptPersonName = person.UserName;

                var log = new Models.Log();
                log.InputDateTime = DateTime.Now;
                log.InputPersonID = person.UserID;
                log.InputPersonName = person.UserName;
                log.LogType = "物资质量反馈";
                log.LogContent = "接收人员回退物资质量反馈单";
                log.LogReason = backReason;
                log.LogDataID = info.ID;
                db.Log.Add(log);

                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //回复通过
        [HttpPost]
        [ValidateInput(false)]
        public string ReplyOk()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                var replyContent = infoList["replyContent"].ToString();
                int.TryParse(infoList["feedBackID"].ToString(), out var feedBackID);

                var person = App_Code.Commen.GetUserFromSession();
                var info = db.MaterialQualityFeedback.Find(feedBackID);
                info.FeedBackState = "回复完毕";
                info.ReplyDateTime = DateTime.Now;
                info.ReplyPersonID = person.UserID;
                info.ReplyPersonName = person.UserName;
                info.ReplyContent = replyContent;

                var log = new Models.Log();
                log.InputDateTime = DateTime.Now;
                log.InputPersonID = person.UserID;
                log.InputPersonName = person.UserName;
                log.LogType = "物资质量反馈";
                log.LogContent = "物资质量反馈--问题回复完毕";
                log.LogDataID = info.ID;
                db.Log.Add(log);

                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //回复回退
        [HttpPost]
        public string ReplyBack()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                int.TryParse(infoList["feedBackID"].ToString(), out var feedBackID);
                var backReason = infoList["backReason"].ToString();

                var person = App_Code.Commen.GetUserFromSession();
                var info = db.MaterialQualityFeedback.Find(feedBackID);
                info.FeedBackState = "回复回退";
                //info.ReplyDateTime = DateTime.Now;
                //info.ReplyPersonID = person.UserID;
                //info.ReplyPersonName = person.UserName;

                var log = new Models.Log();
                log.InputDateTime = DateTime.Now;
                log.InputPersonID = person.UserID;
                log.InputPersonName = person.UserName;
                log.LogType = "物资质量反馈";
                log.LogContent = "回复人员回退物资质量反馈单";
                log.LogReason = backReason;
                log.LogDataID = info.ID;
                db.Log.Add(log);

                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //领导审核通过
        [HttpPost]
        public string CheckOk()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                int.TryParse(infoList["feedBackID"].ToString(), out var feedBackID);

                var person = App_Code.Commen.GetUserFromSession();
                var info = db.MaterialQualityFeedback.Find(feedBackID);
                info.FeedBackState = "审核通过";
                info.CheckDateTime = DateTime.Now;
                info.CheckPersonID = person.UserID;
                info.CheckPersonName = person.UserName;
                info.AppraiseBillState = "待生成";

                var log = new Models.Log();
                log.InputDateTime = DateTime.Now;
                log.InputPersonID = person.UserID;
                log.InputPersonName = person.UserName;
                log.LogType = "物资质量反馈";
                log.LogContent = "物资质量反馈--领导审核完毕";
                log.LogDataID = info.ID;
                db.Log.Add(log);

                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //领导审核回退
        [HttpPost]
        public string CheckBack()
        {
            try
            {
                var infoList =
   JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                int.TryParse(infoList["feedBackID"].ToString(), out var feedBackID);
                var backReason = infoList["backReason"].ToString();

                var person = App_Code.Commen.GetUserFromSession();
                var info = db.MaterialQualityFeedback.Find(feedBackID);
                info.FeedBackState = "审核回退";
                //info.CheckDateTime = DateTime.Now;
                //info.CheckPersonID = person.UserID;
                //info.CheckPersonName = person.UserName;

                var log = new Models.Log();
                log.InputDateTime = DateTime.Now;
                log.InputPersonID = person.UserID;
                log.InputPersonName = person.UserName;
                log.LogType = "物资质量反馈";
                log.LogContent = "领导审核回退";
                log.LogReason = backReason;
                log.LogDataID = info.ID;
                db.Log.Add(log);

                db.SaveChanges();
                return "ok";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        //获得审批过程信息
        [HttpPost]
        public JsonResult GetLogList()
        {
            try
            {
                var infoList =
  JsonConvert.DeserializeObject<Dictionary<String, Object>>(HttpUtility.UrlDecode(Request.Form.ToString()));

                int.TryParse(infoList["feedBackID"].ToString(), out var feedBackID);
                var list = db.Log.Where(w => w.LogType == "物资质量反馈" && w.LogDataID == feedBackID).ToList();
                return Json(list);
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }

        public FileResult GetAppraiseBill(int feedBackID)
        {
            string fileName = Server.MapPath(@"/template/供应商日常考核记录单.doc");
            Aspose.Words.Document doc = new Aspose.Words.Document(fileName);
            Aspose.Words.DocumentBuilder builder = new Aspose.Words.DocumentBuilder(doc);

            var info = db.MaterialQualityFeedback.Find(feedBackID);
            builder.MoveToBookmark("SupplierName");
            builder.Write(info.SupplierName);

            var lastNum = db.MaterialQualityFeedback.Max(m => m.AppraiseBillNum);
            lastNum = lastNum == 0 ? 1 : lastNum+1;
            var appraiseBillNum = "000" + (info.AppraiseBillNum == 0 ? lastNum : info.AppraiseBillNum).ToString();
            var fileNameWord = "供应商日常考核记录单--" + appraiseBillNum.Substring(appraiseBillNum.Length-4,4);
            string fileToSave = System.IO.Path.Combine(Server.MapPath("/"), "FileOutput/" + fileNameWord + ".doc");
            if (System.IO.File.Exists(fileToSave))
            {
                System.IO.File.Delete(fileToSave);
            }
            doc.Save(fileToSave, SaveFormat.Doc);

            info.AppraiseBillNum = info.AppraiseBillNum ==0?lastNum: info.AppraiseBillNum;
            info.AppraiseBillInputTime = DateTime.Now;
            info.AppraiseBillState = "已生成";

            var person = App_Code.Commen.GetUserFromSession();
            var log = new Models.Log();
            log.InputDateTime = DateTime.Now;
            log.InputPersonID = person.UserID;
            log.InputPersonName = person.UserName;
            log.LogType = "物资质量反馈";
            log.LogContent = "生成考核单";
            log.LogDataID = info.ID;
            db.Log.Add(log);

            db.SaveChanges();
            return File(fileToSave, "application/msword", fileNameWord + ".doc");
        }
        #endregion
    }
}