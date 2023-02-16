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
using WebApplication1.CustomValidation;
using WebApplication1.Entity;
using System.Data.Entity.Validation;

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
            for(int i = 0;i<3; i++)
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
            Student[] ss = new Student[3];
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

        [AuthFilter]
        public ActionResult MyProfile()
        {
            @ViewBag.Title = "My Profile";
                ViewBag.name = "Shahidur rahman nayeem";
                ViewBag.section = "B";
               ViewBag.id = "18-38037-2";
               
            
            if(Session["email"].ToString() != null)
            {
                string Email = Session["email"].ToString();


                var db = new StudentEntities1();
                
                var data = (from d in db.Students where d.Email== Email select d).SingleOrDefault();
                
                if (data != null)
                {
                    return View(data);
                }
                else
                {
                    TempData[key: "Msg"] = "Connection error, can't login";
                    return RedirectToAction("Login", "Home");
                }
                
            }
            else
            {
                TempData[key: "Msg"] = "Login error";
                return RedirectToAction("Login", "Home");
            }
            
        }

        [AuthFilter]
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

            var db = new StudentEntities1();
            var students = db.Students.ToList();
            if(students != null)
            {
                return View(students);
            }
            else
            {
                return RedirectToAction("Login", "Home");
            }
            
        }

       
        public ActionResult Details(int id, int? result = null)
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
            var db = new StudentEntities1();

            var data = (from d in db.Students where d.Id == id select d).SingleOrDefault();

            if (data != null)
            {
                return View(data);
            }
            else
            {
                TempData[key: "Msg"] = "Connection error, can't login";
                return RedirectToAction("Students");
            }
        }



        [HttpGet]
        public ActionResult Delete(int id)
        {
            var db = new StudentEntities1();

            var data = (from d in db.Students where d.Id == id select d).SingleOrDefault();
            db.Students.Remove(data);
            int rowsAffected =  db.SaveChanges();

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
            var db = new StudentEntities1();
            var data = (from d in db.Students where d.Id == Id select d).SingleOrDefault();

            if (data != null)
            {
                return View(data);
            }
            else
            {
                TempData[key: "Msg"] = "Connection error, can't login";
                return RedirectToAction("Students");
            }
        }

        [HttpPost]
        public ActionResult Update(WebApplication1.Entity.Student model)
        {
            var db = new StudentEntities1();

            try
            {
                var data = db.Students.Single(d => d.Id == model.Id);
               data.Name= model.Name;
                data.Email= model.Email;
                data.Phone  = model.Phone;
                data.Dob = model.Dob;
                data.Gender= model.Gender;
               
                int numRows = db.SaveChanges();
                if (numRows >0)
                {
                    return RedirectToAction("Details", new { model.Id, result = 1 });
                }
                else
                {
                    TempData[key: "Msg"] = "failed to update";

                    return View(model);
                }
                // Update was successful
               
            }
            catch (DbEntityValidationException ex)
            {
                // Handle validation errors
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

                TempData[key: "Msg"] = exceptionMessage;

                return View(model);
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                TempData[key: "Msg"] = ex.Message;

                return View(model);
            }
        }
        public ActionResult Logout()
        {
            //session clear
            Session["email"] = null;

            return RedirectToAction("Login", "Home");
        }
    }
}