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
    public class FloorGridController : Controller
    {

        private OSWAM_DataEntities dataContext = new OSWAM_DataEntities();

        // GET: /<controller>/
        public ActionResult Index()
        {
            
            CanvasModel floorGrid = new CanvasModel();

            return View(floorGrid);
        }

        public void editCell(int locX, int locY, int CellType)
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
            else
            {
                //System.Diagnostics.Debug.WriteLine("Delete");
                dataContext.DeleteShelf(null, locX, locY);
            }
        }

    }
}
