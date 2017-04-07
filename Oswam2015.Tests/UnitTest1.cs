using Oswam2015;
using Oswam2015.Models;
using Oswam2015.Controllers;
using OSWAM.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using System.Collections.Generic;

namespace Oswam2015.Tests
{

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1() //Single Order fill time
        {
            var testController = new UtilityController();

            int result = testController.GetFillTime("D0C88DB0-1BC9-4229-97ED-4986B87DDC69");

            Assert.AreEqual(result, 42);
        }

        [TestMethod]
        public void TestMethod2() //Shelf contents
        {
            var testController = new UtilityController();

            List<GetShelfContents_Result> itemList = testController.GetShelfContents(22, 13);

            Assert.IsTrue(itemList.Count > 0);
        }

        [TestMethod]
        public void TestMethod3() //Average Order Fill time 
        {
            var testController = new SettingsController();

            Assert.AreEqual(testController.GetAveOrderFillTime(), 175.50);
        }

        [TestMethod]
        public void TestMethod4() //Total items stored
        {
            var testController = new SettingsController();

            Assert.AreEqual(testController.GetTotalItemNum(), 11021);
        }

    }
}
