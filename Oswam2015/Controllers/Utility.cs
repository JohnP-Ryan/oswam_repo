using System;
using System.Linq;
using System.Web.Mvc;
using Oswam2015.Models;

namespace Oswam2015.Controllers
{
    public class UtilityController : Controller
    {
        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        // GET: Utility
        public ActionResult Index() //about page
        {
            return View();
        }

        public ActionResult About()
        {
            return View("~/Views/Utility/About.cshtml");
        }

        public ActionResult Terms()
        {
            return View("~/Views/Utility/Terms.cshtml");
        }

        public ActionResult Contact()
        {
            return View("~/Views/Utility/Contact.cshtml");
        }


        //For unit testing only 
        public double GetProductWeight(String id)
        {
            var result = dataContext.GetInventoryProducts(id, null, 0, 0, 0, 0);
            var resultList = result.ToList();

            System.Diagnostics.Debug.WriteLine("" + resultList.FirstOrDefault().Weight);

            double resultNum = Convert.ToDouble(resultList.FirstOrDefault().Weight);
            return resultNum;
        }
    }
}