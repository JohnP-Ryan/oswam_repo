using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Oswam2015.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OSWAM.Controllers
{
    public class InventoryController : Controller
    {

        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        public ActionResult Index()
        {
            //check if one value is null, (is new session)
            if(!(Session["fInvWeightLow"] != null))
            {
                System.Diagnostics.Debug.WriteLine("Session not filled");
                Session["fInvWeightLow"] = 0;
                Session["fInvWeightHigh"] = 0;
                Session["fInvPriceLow"] = 0;
                Session["fInvPriceHigh"] = 0;
            }

            /* Debug form data printouts
            System.Diagnostics.Debug.WriteLine(Session["fInvID"]);
            System.Diagnostics.Debug.WriteLine(Session["fInvName"]);
            System.Diagnostics.Debug.WriteLine(Session["fInvWeightLow"]);
            System.Diagnostics.Debug.WriteLine(Session["fInvWeightHigh"]);
            System.Diagnostics.Debug.WriteLine(Session["fInvPriceLow"]);
            System.Diagnostics.Debug.WriteLine(Session["fInvPriceHigh"]);
            */

            var data = dataContext.GetInventoryProducts((string)(Session["fInvID"]), (string)(Session["fInvName"]), (int)Session["fInvWeightLow"], (int)Session["fInvWeightHigh"], (int)Session["fInvPriceLow"], (int)Session["fInvPriceHigh"]);

            return View(data.ToList());
        }

        [HttpPost]
        public ActionResult Report(string textboxID = null, string textboxName = null, int weightLow = 0, int weightHigh = 0, int priceLow = 0, int priceHigh = 0)
        {

            Session["fInvID"] = textboxID;
            Session["fInvName"] = textboxName;
            Session["fInvWeightLow"] = weightLow;
            Session["fInvWeightHigh"] = weightHigh;
            Session["fInvPriceLow"] = priceLow;
            Session["fInvPriceHigh"] = priceHigh;

            return RedirectToAction("Index");
        }
    }
}
