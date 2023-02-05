using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Models;

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
       public ActionResult SignUpSubmitted()
        {
            return View();
        }
        public ActionResult Info() { 
        Home h = new Home();
            h.Email = "msrnayeem@gmail.com";
            h.Password= "password";
            h.Name= "Nayeem";
        return View(h);
        }
        
        public ActionResult List() {
            List<Home> homes = new List<Home>();
            for(int i = 0; i<10; i++)
            {
                Home h = new Home()
                {
                    Email = "msr" + (i + 1) + "@gmail.com",
                Name = "nayeem" + (i + 1)
                };
            homes.Add(h);
            }
            return View(homes);
        }
    }
}