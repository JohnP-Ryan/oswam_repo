using System;
using System.Linq;
using System.Web.Mvc;
using Oswam2015.Models;

namespace Oswam2015.Controllers
{
    public class SimController : Controller
    {
        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        private int stationNum;

        public ActionResult Index()
        {
            stationNum = Convert.ToInt32(dataContext.GetPreferenceValue("NumPlacedStations").ToList().FirstOrDefault());


            System.Diagnostics.Debug.WriteLine("Run Sim");
            runSimSet();

            return View();
        }


        public void runSimSet()
        {
            System.Diagnostics.Debug.WriteLine("Run Sim Set");

            for(var i = 0; i < stationNum; i++)
            {
                //dataContext.
            }

        }
    }
}