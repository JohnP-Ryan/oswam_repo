﻿using System;
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

        public ActionResult Index()
        {

            var preferenceList = dataContext.GetAllPreferences();
            
            return View(preferenceList.ToList());
        }
    }
}
