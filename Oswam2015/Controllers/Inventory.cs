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
                Session["fInvWeightLow"] = 0;
                Session["fInvWeightHigh"] = 0;
                Session["fInvPriceLow"] = 0;
                Session["fInvPriceHigh"] = 0;
            }

            var productList = dataContext.GetInventoryProducts((string)(Session["fInvID"]), (string)(Session["fInvName"]), (int)Session["fInvWeightLow"], (int)Session["fInvWeightHigh"], (int)Session["fInvPriceLow"], (int)Session["fInvPriceHigh"]);

            return View(productList.ToList());
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

        public string DetailsPartial(string productID)
        {
            String html = "";

            var productDetails = dataContext.GetInventoryProducts(productID, null, 0, 0, 0, 0);
            var detailList = productDetails.ToList();

            html += "<tr><td><h6>" + "ID" + "</h6></td><td><h6>" + detailList[0].ID + "</h6></td></tr>";
            html += "<tr><td><h6>" + "Name" + "</h6></td><td><h6>" + detailList[0].ItemName + "</h6></td></tr>";
            html += "<tr><td><h6>" + "Weight" + "</h6></td><td><h6>" + detailList[0].Weight + "</h6></td></tr>";
            html += "<tr><td><h6>" + "Length" + "</h6></td><td><h6>" + detailList[0].DimLength + "</h6></td></tr>";
            html += "<tr><td><h6>" + "Width" + "</h6></td><td><h6>" + detailList[0].DimWidth + "</h6></td></tr>";
            html += "<tr><td><h6>" + "Height" + "</h6></td><td><h6>" + detailList[0].DimHeight + "</h6></td></tr>";
            html += "<tr><td><h6>" + "Price" + "</h6></td><td><h6>" + detailList[0].Price + "</h6></td></tr>";

            return html;
        }
    }
}
