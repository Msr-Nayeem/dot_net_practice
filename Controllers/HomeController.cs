using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication1.Models;
using System.Data.SqlClient;
using System.Web.Helpers;
using System.Xml.Linq;
using System.Data;
using WebApplication1.Entity;
using Login = WebApplication1.Models.Login;
using Student = WebApplication1.Entity.Student;
using System.Runtime.InteropServices;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(Login info)
        {
            if (ModelState.IsValid)
            {

                var submit = new StudentEntities1();
                var match = (from d in submit.Students where d.Email== info.Email && d.Password == info.Password select d).SingleOrDefault();

                if (match != null)
                {
                    //set value in cookie
                    


                    if (info.Remember != null)
                    {
                        Response.Cookies["Email"].Value = info.Email.Trim();
                        Response.Cookies["Password"].Value = info.Password.Trim();
                        Response.Cookies["Remember"].Value = "checked";

                        Response.Cookies["Email"].Expires = DateTime.Now.AddDays(7);
                        Response.Cookies["Password"].Expires = DateTime.Now.AddDays(7);
                        Response.Cookies["Remember"].Expires = DateTime.Now.AddDays(7);
                        

                        //HttpCookie cookie1 = new HttpCookie("Email", info.Email);
                        //cookie1.Expires = DateTime.Now.AddDays(7);
                        //Response.Cookies.Add(cookie1);

                        //HttpCookie cookie2 = new HttpCookie("Password", info.Password);
                        //cookie2.Expires = DateTime.Now.AddDays(7);
                        //Response.Cookies.Add(cookie2);
                    }
                    else
                    {
                        
                            Response.Cookies["Email"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["Password"].Expires = DateTime.Now.AddDays(-1);
                            Response.Cookies["Remember"].Expires = DateTime.Now.AddDays(-1);
                        
                        
                    }
                    
                   
                    
                    Session["email"] = info.Email;

                    return RedirectToAction("MyProfile", "Dashboard");
                }
                else
                {
                    TempData["Msg"] = "Email/Password not matched";
                    return View();
                }
            }
            else
            {
                return View(info);
            }
                
        }
   
        public ActionResult SignUp()
        {
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
            return View();
        }
        [HttpPost]
       // public ActionResult Registration(string Namee,string Emaill,string Passwordd)
        public ActionResult Registration(Student info)
        {
            if (ModelState.IsValid)
            {
                var db = new StudentEntities1();
                db.Students.Add(info);
                
                int rowsAffected = db.SaveChanges();
                if (rowsAffected > 0)
                {
                    // the operation was successful
                    TempData["msg"] = "Inserted";
                    return View();
                }
                else
                {
                    // the operation failed
                    // handle the error here, for example by displaying an error message to the user
                    TempData["msg"] = "failed";
                    return View(info);
                }
               
            }
            else
            {
                return View(info);
            }
           

        }

        [HttpGet]
        public ActionResult Reg()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Reg(RegModel reg)
        {
            ViewBag.name = reg.Name;

            if (ModelState.IsValid)
            {
                // return RedirectToAction("Login");
                return View(reg);
            }
            //List<string> hobbies = reg.Hobbies;
            //foreach (string hobby in hobbies)
            //{
            //    Console.WriteLine(hobby);
            //}
            else
            {
                return View(reg);
            }
            
        }
    }
}