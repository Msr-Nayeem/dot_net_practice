using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            @ViewBag.Title = "Dashboard";
            ViewBag.List = new string[] { "shahidur ", "Nayeem" };
            return View();
        }
        public ActionResult myProfile()
        {
            @ViewBag.Title = "My Profile";
            ViewBag.name = "Shahidur rahman nayeem";
            ViewBag.section = "B";
            ViewBag.id = "18-38037-2";
            
            return View();
        }
        public ActionResult Setting()
        {
            @ViewBag.Title = "Setting";
            return View();
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Home");
        }
    }
}