using Oswam2015;
using Oswam2015.Models;
using Oswam2015.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Oswam2015.Tests
{

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            var testController = new UtilityController();

            int result = testController.GetFillTime("D0C88DB0-1BC9-4229-97ED-4986B87DDC69");

            Assert.AreEqual(result, 42);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var testController = new UtilityController();

            List<GetShelfContents_Result> itemList = testController.GetShelfContents(22, 13);

            Assert.IsTrue(itemList.Count > 0);
        }

    }
}
