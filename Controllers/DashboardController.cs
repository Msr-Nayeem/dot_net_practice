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
using Newtonsoft.Json;
using System.Data.Entity;

namespace WebApplication1.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
           
            
            return View();
        }

        [AuthFilter]
        public ActionResult MyProfile()
        {  
            if(Session["email"].ToString() != null)
            {
                string Email = Session["email"].ToString();

                var db = new StudentEntities1();
                var profile = db.Students.Include("Dept").Where(s =>s.Email == Email).SingleOrDefault();

                if (profile != null)
                {
                    return View(profile);
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

      //  [AuthFilter]
        public ActionResult Students(int? id = null, int? result = null)
        {
            
            @ViewBag.Title = "Students";
            if (id != null && result == 1)
            {
                TempData[key: "id"] = id;
                TempData[key: "result"] = "Deleted Successfully";
            }
            else if(result == 0)
            {
                TempData[key: "result"] = "Failed to delete";
            }

            var db = new StudentEntities1();
            var students = db.Students.Include("Dept").OrderBy(c => c.Dept.DeptName).ToList();
            if (students != null)
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
             var data = db.Students.Include("Dept").Where(s => s.Id == id).SingleOrDefault();

            if (data != null)
            {
                var dbs = new StudentEntities1();
                var courses = dbs.CourseStudents.Include("Cours").Where(cs => cs.StudentId == id).ToList();
                if (courses.Any())
                {
                    ViewBag.courses = courses;
                }
                else
                {
                    ViewBag.courses = null;
                }
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
        public ActionResult Update(Student  model)
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
            catch (DbEntityValidationException)
            {
                return View(model);
            }
            catch (Exception)
            {
                return View(model);
            }
        }


//DEPARTMENT
        [HttpGet]
        public ActionResult Department()
        {
            var db = new StudentEntities1();
            var dept = db.Depts.OrderBy(c => c.DeptName).ToList();
            if (dept != null)
            {
                return View(dept);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        public ActionResult AddDepartment()
        {
            return  View();
        }

        [HttpPost]
        public ActionResult AddDepartment(Dept info)
        {
            if(ModelState.IsValid)
            {
                var db = new StudentEntities1();
                db.Depts.Add(info);
                int rowsAffected = db.SaveChanges();
                if (rowsAffected > 0)
                {
                    // the operation was successful
                    TempData["result"] = "New Department Added";
                    return RedirectToAction("Department");
                }
                else
                {
                    // the operation failed
                    // handle the error here, for example by displaying an error message to the user
                    TempData["result"] = "failed";
                    return View(info);
                }
            }
            else
            {
                return View(info);
            }
            
        }
        //END DEPARTMENT


        //COURSE
        public ActionResult CourseList(int? id)
        {
            var db = new StudentEntities1();
            if (id != null)
            {
                
                var courses = db.Courses.Include("Dept").Where(c => c.DeptId == id).ToList();

                if (courses != null)
                {
                    return View(courses);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            else
            {
                var courses = db.Courses.Include("Dept").OrderBy(c => c.Dept.DeptName).ToList();
                return View(courses);
            }

        }

        public ActionResult AddCourse() 
        {
            var db = new StudentEntities1();

            var dept = db.Depts.ToList();
            var jsonData = new SelectList(dept, "Id", "DeptName");
            //ViewBag.JsonData = JsonConvert.SerializeObject(jsonData);
            ViewBag.JsonData = JsonConvert.SerializeObject(db.Depts.Select(x => new { Value = x.Id, Text = x.DeptName }).ToList());

            return View();
        }

        [HttpPost]
        public ActionResult AddCourse(Cours info)
        {
            var db = new StudentEntities1();
            if (info == null)
            {
                return View(info);
            }
            if (ModelState.IsValid)
            {
                db.Courses.Add(info);
                int rowsAffected = db.SaveChanges();
                if (rowsAffected >0)
                {
                    TempData["result"] = "Course Added";
                    return View();
                }
                else
                {
                    TempData["result"] = "failed";
                    return View();
                }
            }
            else
            {
                return View(info);
            }
            
        }


        public ActionResult CourseDetailes(int? id)
        {
            var db = new StudentEntities1();
            var students = db.CourseStudents.Include("Student").Include("Cours").Where(s => s.CourseId == id).ToList();
            return View(students);
        }


        //END COURSE


        //SET_COURSE
        public ActionResult SetCourse()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SetCourse(CourseStudent info)
        {
            if (info == null)
            {
                return View();
            }

            if (ModelState.IsValid)
            {
                var db = new StudentEntities1();
                db.CourseStudents.Add(info);

                int rowsAffected = db.SaveChanges();
                if (rowsAffected > 0)
                {
                    TempData["result"] = "Registered";
                    return View();
                }
                else
                {
                    TempData["result"] = "failed";
                    return View();
                }
            }
            else
            {
                return View();
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