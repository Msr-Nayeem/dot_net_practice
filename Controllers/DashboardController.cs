using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WebApplication1.Models;
using static WebApplication1.Controllers.DashboardController;
using System.Diagnostics;

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
            @ViewBag.Title = "My Profile";
                ViewBag.name = "Shahidur rahman nayeem";
                ViewBag.section = "B";
               ViewBag.id = "18-38037-2";
               
            HttpCookie nameCookie = Request.Cookies["email"];
            if(nameCookie != null)
            {
                string Email = nameCookie.Value.Split('=')[0];
                string query = "select * from Students where Email='" + Email + "' ";
 
                StudentsDashboard student = GetInformation(query);
                if (student != null)
                {
                    return View(student);
                }
                else
                {
                    TempData[key: "Msg"] = "error";
                    return RedirectToAction("Login", "Home");
                }
                
            }
            else
            {
                TempData[key: "Msg"] = "Login First";
                return RedirectToAction("Login", "Home");
            }
            
        }

        public ActionResult Students(int? id = null, int? result = null)
        {
            @ViewBag.Title = "Students";
            if (id != null && result == 1)
            {
                TempData[key: "id"] = id;
                TempData[key: "result"] = "Deleted Successfully";
            }
            else
            {
                TempData[key: "result"] = "Failed to delete";
            }
           
            string query = "select * from Students";


            List<StudentsDashboard> list = new List<StudentsDashboard>();

            DB db = new DB(query);
            try
            {
                using (SqlDataReader reader = db.ExecuteReader())
                {
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
                    return View(list);
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                TempData[key: "Msg"] = ex.Message;
                return RedirectToAction("Login", "Home");
            }
            finally
            {
                db.Dispose();
            } 

        }

       
        public ActionResult Details(int Id, int? result = null)
        {
            if (result == 1)
            {
                TempData[key: "result"] = "Information Updated Successfully";
            }
            else if (result == 0)
            {
                TempData[key: "result"] = "Failed ! try again.";
            }
            else
            {
                TempData[key: "result"] = null;
            }
            string query = "select * from Students where Id='"+Id+"'";
           
            StudentsDashboard student = GetInformation(query);
            if (student != null)
            {
                return View(student);
            }
            else
            {
                TempData[key: "Msg"] = "Error";
                return RedirectToAction("Login", "Home");
            }
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            string query = "DELETE FROM Students WHERE Id=@Id";

            DatabaseConnection databaseConnection = new DatabaseConnection();
            SqlConnection connection = databaseConnection.GetConnection();
            SqlCommand cmd = new SqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@Id", id);
            connection.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            connection.Close();
            if (rowsAffected > 0)
            {
                return RedirectToAction("Students", new { Id = id, result = 1 });
            }
            else
            {
                return RedirectToAction("Students", new { Id = id, result = 0 });
            }

            // ViewBag.id = id;
            //  return RedirectToAction("Students", new { Id = id });
        }

        [HttpGet]
        public ActionResult Update(int Id)
        {
            string query = "select * from Students where Id='" + Id + "'";

            StudentsDashboard student = GetInformation(query);
            if (student != null)
            {
                return View(student);
            }
            else
            {
                TempData[key: "Msg"] = "Error";
                return RedirectToAction("Login", "Home");
            }
        }

        [HttpPost]
        public ActionResult Update(StudentsDashboard s)
        {
            if (ModelState.IsValid)
            {
                string query = "UPDATE Students SET Name=@Name, Email=@Email, Phone=@Phone, Dob=@Dob, Gender=@Gender WHERE Id=@Id";
                DatabaseConnection databaseConnection = new DatabaseConnection();
                SqlConnection connection = databaseConnection.GetConnection();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", s.Id);
                cmd.Parameters.AddWithValue("@Name", s.Name);
                cmd.Parameters.AddWithValue("@Email", s.Email);
                cmd.Parameters.AddWithValue("@Phone", s.Phone);
                cmd.Parameters.AddWithValue("@Dob", s.Dob);
                cmd.Parameters.AddWithValue("@Gender", s.Gender);
                connection.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                connection.Close();

                if (rowsAffected > 0)
                {
                    // Update was successful
                    return RedirectToAction("Details", new { s.Id, result = 1 });
                }
                else
                {
                    // Update failed
                    return View(s);
                }
            }
            else
            {
                return View(s);
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


        public StudentsDashboard GetInformation(String query)
        {
           
            StudentsDashboard student = null;
            DB db = new DB(query);
            try
            {
                using (SqlDataReader reader = db.ExecuteReader())
                {
                    while (reader.Read())
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
                    
                }
            }
            catch (Exception ex)
            {
                // Handle exception
                TempData[key: "Msg"] = ex.Message;
                
            }
            finally
            {
                db.Dispose();
            }


            return student;
        }
    }
}