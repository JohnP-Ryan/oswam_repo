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
using Newtonsoft.Json;

using Oswam2015.Controllers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OSWAM.Controllers
{
    public class FloorGridController : Controller
    {

        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        // GET: /<controller>/
        public ActionResult Index()
        {
            SortingController.test();
            CanvasModel floorGrid = new CanvasModel();

            return View(floorGrid);
        }

        public String editCell(int locX, int locY, int CellType)
        {
            if (CellType == 0)
            {
                //System.Diagnostics.Debug.WriteLine("Cell 0");
                dataContext.CreateShelf(locX, locY, null, null, false);
            }
            else if (CellType == 1)
            {
                //System.Diagnostics.Debug.WriteLine("Cell 1");
                dataContext.CreateShelf(locX, locY, null, null, true);
            }
            else if (CellType == 2) //janky code ftw
            {
                //initial num placed display, no code necessary
            }
            else
            {
                //System.Diagnostics.Debug.WriteLine("Delete");
                dataContext.DeleteShelf(null, locX, locY);
            }

            var stringArray = new string[2];
            stringArray[0] = "" + Convert.ToInt32(dataContext.GetPreferenceValue("NumPlacedShelves").ToList().FirstOrDefault());
            stringArray[1] = "" + Convert.ToInt32(dataContext.GetPreferenceValue("NumPlacedStations").ToList().FirstOrDefault());

            return JsonConvert.SerializeObject(stringArray);
        }

    }
}
