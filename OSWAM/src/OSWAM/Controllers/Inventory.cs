using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OSWAM.Controllers
{
    public class Inventory : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Report(string textboxID, string textboxName, int weightLow, int weightHigh, int priceLow, int priceHigh)
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
