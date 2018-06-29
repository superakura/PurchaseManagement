using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PurchaseManagement.Controllers;
using PurchaseManagement.Models;
using System.Web;

namespace PurchaseManagement.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void GetAjax()
        {
            TestController ctrl = new TestController();
            var equal = "okxxx";
            ctrl.Request.Form.Add("test", "xxx");
            var result = ctrl.GetTest();
            Assert.AreEqual(result, equal);
        }

        public void GetStr()
        {
            TestController ctrl = new TestController();
            var equal = "okxxx";
            Assert.AreEqual(ctrl.GetTest(), equal);
        }
    }
}
