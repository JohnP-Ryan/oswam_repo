using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using Oswam2015.Models;
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

            var result = dataContext.GetInventoryProducts("106171327X", null, 0, 0, 0, 0);
            var resultList = result.ToList();

            //System.Diagnostics.Debug.WriteLine("" + resultList[0].DimWidth);

            Assert.AreEqual(resultList[0].DimWidth, 0.10);
        }

        [TestMethod]
        public void TestMethod3()
        {
            Assert.AreEqual(2, 2);
        }
    }
}
