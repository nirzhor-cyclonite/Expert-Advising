using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace trial7.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Browse()
        {
            return RedirectToAction("Index", "Browse");
        }
        public ActionResult Expert_Details()
        {
            return View();
        }
        public ActionResult profile()
        {
            return View();
        }
    }
    
}
