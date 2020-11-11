using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabExameWebsite.Controllers
{
    public class AreaRestritaController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Logout()
        {            
            return RedirectToAction("Login", "AreaRestrita");
        }
    }
}