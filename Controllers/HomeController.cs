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
       public ActionResult SignUpSubmitted(SignUp datas)
        {
            //auto class 
            //SignUp datas = new SignUp();

            // from HttpRequestBase Request -- SignUpSubmitted()

            //datas.Namee = Request["name"];
            //datas.Emaill = Request["email"];
            //datas.Passwordd = Request["password"];

            //form collection -- SignUpSubmitted(FormCollection form)
            //datas.Namee = form["name"];
            //datas.Emaill = form["email"];
            //datas.Passwordd = form["password"];

            // form direct variables -- SignUpSubmitted(string Name, string Email, string Password)
            //datas.Namee = Name;
            //datas.Emaill = Email;
            //datas.Passwordd = Password;

            

            return View(datas);
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


        public ActionResult Registration()
        {
            ViewBag.Message = "Registration page.";

            return View();
        }
        [HttpPost]
        public ActionResult Registration(SignUp datas)
        {

            return View();
        }
    }
}