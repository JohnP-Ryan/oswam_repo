using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Oswam2015.Controllers
{
    public class UtilityController : Controller
    {
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
    }
}