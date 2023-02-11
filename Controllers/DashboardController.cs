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

        string connString = @"server=DESKTOP-AVD9M43;Database=Student;Integrated Security=true";

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

        public ActionResult Students(int? id = null, int? result = null)
        {
            @ViewBag.Title = "Students";
            if(id != null)
            {
                @ViewBag.Id = id;
                @ViewBag.result = result;
            }
           
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

                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Phone = reader.GetInt32(reader.GetOrdinal("Phone")).ToString(),
                            Dob = reader.GetDateTime(reader.GetOrdinal("Dob")).Date,
                            Gender = reader.GetString(reader.GetOrdinal("Gender"))
                            
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

        public ActionResult Details(int Id)
        {
            string query = "select * from Students where Id=@Id";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@Id", Id);
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    StudentsDashboard student = null;

                    if (reader.Read())
                    {
                        student = new StudentsDashboard()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            Email = reader.GetString(reader.GetOrdinal("Email")),
                            Phone = reader.GetInt32(reader.GetOrdinal("Phone")).ToString(),
                            Dob = reader.GetDateTime(reader.GetOrdinal("Dob")).Date,
                            Gender = reader.GetString(reader.GetOrdinal("Gender"))
                        };
                    }
                    conn.Close();
                    return View(student);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return View();
            }

        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            string deleteQuery = "DELETE FROM Students WHERE Id=@Id";
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    SqlCommand cmd = new SqlCommand(deleteQuery, conn);
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                    conn.Close();
                    if (rowsAffected > 0)
                    {
                        return RedirectToAction("Students", new { Id = id, result = 1 });
                    }
                    else
                    {
                        return RedirectToAction("Students", new { Id = id, result = 0 });
                    }
                   
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Students", new { Id = id });
            }

            // ViewBag.id = id;
            //  return RedirectToAction("Students", new { Id = id });
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