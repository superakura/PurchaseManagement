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
    }
}