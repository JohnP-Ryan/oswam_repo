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
            var data = dataContext.GetInventoryProducts(null, null, 0, 1, 0, 4);

            if (TempData["FormDataArray"] == null)
            {
                data = dataContext.GetInventoryProducts(null, null, 0, 1, 0, 200);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("Data preserved: ");
                String[] formData = (String[]) TempData["FormDataArray"];

                for(int i = 0; i > 6; i++)
                {
                    System.Diagnostics.Debug.WriteLine("Temp: " + formData[i]);
                }

                data = dataContext.GetInventoryProducts(formData[0], formData[1], Int32.Parse(formData[2]), Int32.Parse(formData[3]), Int32.Parse(formData[4]), Int32.Parse(formData[5]));
            }


            return View(data.ToList());
        }

        [HttpPost]
        public ActionResult Report(string textboxID = null, string textboxName = null, int weightLow = 0, int weightHigh = 0, int priceLow = 0, int priceHigh = 0)
        {
            /*
            System.Diagnostics.Debug.WriteLine("RUNNING");

            

            System.Diagnostics.Debug.WriteLine(fData.getFormId());
            System.Diagnostics.Debug.WriteLine(fData.getFormName());
            System.Diagnostics.Debug.WriteLine(fData.getFormWeightLow());
            System.Diagnostics.Debug.WriteLine(fData.getFormWeightHigh());
            System.Diagnostics.Debug.WriteLine(fData.getFormPriceLow());
            System.Diagnostics.Debug.WriteLine(fData.getFormPriceHigh());


            InventoryFormData fData = new InventoryFormData(null, null, 0, 1, 0, 4);

            fData.setFormId(textboxID);
            fData.setFormName(textboxName);
            fData.setFormWeightLow(weightLow);
            fData.setFormWeightHigh(weightHigh);
            fData.setFormPriceLow(priceLow);
            fData.setFormPriceHigh(priceHigh);

            var data = dataContext.GetInventoryProducts(fData.getFormId(), fData.getFormName(), fData.getFormWeightLow(), fData.getFormWeightHigh(), fData.getFormPriceLow(), fData.getFormPriceHigh());
            System.Diagnostics.Debug.WriteLine(fData.getFormId());
            System.Diagnostics.Debug.WriteLine(fData.getFormName());
            System.Diagnostics.Debug.WriteLine(fData.getFormWeightLow());
            System.Diagnostics.Debug.WriteLine(fData.getFormWeightHigh());
            System.Diagnostics.Debug.WriteLine(fData.getFormPriceLow());
            System.Diagnostics.Debug.WriteLine(fData.getFormPriceHigh());
            */

            String[] formData = new String[6];
            formData[0] = textboxID;
            formData[1] = textboxName;
            formData[2] = weightLow.ToString();
            formData[3] = weightHigh.ToString();
            formData[4] = priceLow.ToString();
            formData[5] = priceHigh.ToString();

            TempData["FormDataArray"] = formData;

            return RedirectToAction("Index");
        }
    }
}
