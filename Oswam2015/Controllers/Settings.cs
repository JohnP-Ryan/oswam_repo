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

    public class SettingsController : Controller
    {
        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        [OutputCache(Duration = 0)]
        public ActionResult Index()
        {

            var preferenceList = dataContext.GetAllPreferences();
            
            return View(preferenceList.ToList());
        }

        [HttpPost]
        public ActionResult SetPreference(int preferenceID, int newPreferenceValue)
        {
            dataContext.SetPreferenceValue(preferenceID, newPreferenceValue);

            //System.Diagnostics.Debug.WriteLine("ID: " + preferenceID + "  Value: " + newPreferenceValue);

            return RedirectToAction("Index");
        }

        public string GetAveOrderFillTime()
        {
            String html = "";

            var calcTimeReturnList = dataContext.GetAverageFillTime().ToList();
            html += "Average Order Completion Time: &nbsp;" + calcTimeReturnList[0].Value;

            return html;
        }

        public string GetTotalItemNum()
        {
            String html = "";

            var itemNumReturnList = dataContext.GetTotalStoredItemNum().ToList();
            html += "Total Items Stored: &nbsp;" + itemNumReturnList[0].Value;

            return html;
        }
    }
}
