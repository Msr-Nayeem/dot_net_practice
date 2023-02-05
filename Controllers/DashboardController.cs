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

        public class Student
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string CGPA { get; set; }
        }
        public ActionResult Index()
        {
            Student single = new Student();
            
                single.Id = 38037;
                single.Name = "Nayeem";
                single.CGPA = "3.30";

           
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
            @ViewBag.Title = "Dashboard";
            ViewBag.List = single;
            ViewBag.multipleList = multipleStudent;
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