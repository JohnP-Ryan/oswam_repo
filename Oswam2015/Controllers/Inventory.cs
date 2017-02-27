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
        /*
        private readonly OSWAM_DataContext _context;

        public Inventory(OSWAM_DataContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            return View(await _context.Product.ToListAsync());
        }
        */
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Report(string textboxID, string textboxName, int weightLow, int weightHigh, int priceLow, int priceHigh)
        {
            System.Diagnostics.Debug.WriteLine(textboxID);
            System.Diagnostics.Debug.WriteLine(textboxName);
            System.Diagnostics.Debug.WriteLine(weightLow);
            System.Diagnostics.Debug.WriteLine(weightHigh);
            System.Diagnostics.Debug.WriteLine(priceLow);
            System.Diagnostics.Debug.WriteLine(priceHigh);









            return RedirectToAction("Index");
        }
    }
}
