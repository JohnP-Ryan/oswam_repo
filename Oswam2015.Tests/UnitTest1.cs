using Oswam2015.Models;
using Oswam2015.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Oswam2015.Tests
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void TestMethod1()
        {
            Assert.AreEqual(true, false);
        }
    }

    [TestClass]
    public class UnitTest2
    {
        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        [TestMethod]
        public void TestMethod2()
        {

            //double funcResult = Oswam2015.Controllers.UtilityController.GetProductWeight("106171327X");

            //Assert.AreEqual(funcResult, 0.05);
        }

    }
}
