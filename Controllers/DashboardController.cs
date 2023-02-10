using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication1.Models;
//using System.Diagnostics;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {


        // GET: Dashboard
       

        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CGPA { get; set; }
        }
        public ActionResult Index()
        {
            ViewBag.Title = "Dashboard";

            Student single = new Student
            {
                Id = 38037,
                Name = "Nayeem",
                CGPA = "3.30"
            };
            ViewBag.List = single;

            //list
            List<Student> multipleStudent = new List<Student>();  
            for(int i = 0;i<10; i++)
            {
                string name="student"+(i+1);
                int id = i+1;
                string cgpa = "3.3"+(i);

                multipleStudent.Add(new Student()
                {
                    Id = id, CGPA= cgpa, Name= name
                }) ;
            }
            ViewBag.multipleList = multipleStudent;

            //Array
            Student[] ss = new Student[10];
            for(int i = 0;i<ss.Length; i++)
            {
                ss[i] = new Student();
                ss[i].Id = i + 1;
                ss[i].CGPA = "3.3" + (i);
                ss[i].Name = "msr" + (i + 1);
            }
            ViewBag.Students = ss;
            
            
            return View();
        }
        public ActionResult MyProfile()
        {
            HttpCookie nameCookie = Request.Cookies["email"];

            //If Cookie exists fetch its value.
            string name = nameCookie != null ? nameCookie.Value.Split('=')[0] : "undefined";


           // Debug.WriteLine(name);
            @ViewBag.Title = "My Profile";
            ViewBag.name = "Shahidur rahman nayeem";
            ViewBag.section = "B";
            ViewBag.id = "18-38037-2";
            ViewBag.username = name;
         //   Console.WriteLine(name);
            return View();
        }
      
        public ActionResult Students()
        {
            @ViewBag.Title = "Students";

            string connString = @"server=DESKTOP-AVD9M43;Database=Student;Integrated Security=true";
            string query = "select * from Students";
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                List<StudentsDashboard> list = new List<StudentsDashboard>();

                    while (reader.Read())
                    {
                        StudentsDashboard s = new StudentsDashboard()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name")),
                            Email = reader.GetString(reader.GetOrdinal("email"))
                            
                    };
                    list.Add(s);    
                    }
                    conn.Close();
                    return View(list);
                }
            }
            catch (SqlException ex)
            {
                TempData[key: "loginError"] = ex.Message;
                return View();
            }

          
        }
        public ActionResult Logout()
        {
            //session clear
            Session["email"] = null;

            //cookie clear
            var email = Request.Cookies["email"];
            email.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(email);


            return RedirectToAction("Login", "Home");
        }
    }
}