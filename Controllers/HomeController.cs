using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult LoginSubmit()
        {
            ViewBag.Message = "Your login submit page.";
            // if ok
            TempData["msg"] = "Login success";
            return RedirectToAction("Index", "Dashboard");
        }
        public ActionResult Login()
        {
            ViewBag.Message = "Your login page.";

            return View();
        }
        public ActionResult SignUp()
        {
            ViewBag.Message = "Your sign up page.";

            return View();
        }
    }
}