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

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Email, string Password)
        {

            string query = "select * from Students where Email = '" + Email + "' and password = '" + Password + "' ";

            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection connection = databaseConnection.GetConnection();
            SqlCommand cmd = new SqlCommand(query, connection);
            connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                Session["email"] = Email;
                HttpCookie cookie = new HttpCookie("email", Email);
                cookie.Expires = DateTime.Now.AddDays(7);
                Response.Cookies.Add(cookie);
                return RedirectToAction("myProfile", "Dashboard");
            }
            else
            {
                TempData[key: "loginError"] = "Email/Password not matched";
                return View();
            }

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
       // public ActionResult Registration(string Namee,string Emaill,string Passwordd)
        public ActionResult Registration(StudentsDashboard s)
        {
            if (ModelState.IsValid)
            {
                String query = "INSERT INTO Students (Name, Email, Password, Phone, Gender, Dob) VALUES ('" + s.Name + "', '" + s.Email + "', '" + s.Password + "', '" + s.Phone + "', '" + s.Gender + "', '" + s.Dob + "')";
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection connection = databaseConnection.GetConnection();
                SqlCommand cmd = new SqlCommand(query, connection);
                connection.Open();

                var effectedRow = cmd.ExecuteNonQuery();
                if (effectedRow > 0)
                {
                    TempData["msg"] = "Inserted";
                }
                else
                {
                    TempData["msg"] = "failed";
                }
                connection.Close();
                return View();
            }
            else
            {
                return View(s);
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