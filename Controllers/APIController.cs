using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class APIController : Controller
    {
        // GET: API
        public ActionResult GetDeptList()
        {
            var db = new StudentEntities1();
            var dept = db.Depts.Select(d => new { d.Id, d.DeptName }).ToList();
            return Json(dept, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetCourseList(int? id)
        {
            var db = new StudentEntities1();
           
            if (id.HasValue)
            {
             var   courses = db.Courses
                    .Where(c => c.DeptId == id)
                    .Select(d => new { d.Id, d.CourseName })
                    .ToList();
                return Json(courses, JsonRequestBehavior.AllowGet);
            }
            else
            {
               var courses = db.Courses
                    .Select(d => new { d.Id, d.CourseName })
                    .ToList();
                return Json(courses, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult GetStudentList(int? id)
        {
            var db = new StudentEntities1();

            if (id.HasValue)
            {
                var students = db.Students
                       .Where(c => c.DeptId == id)
                       .Select(d => new { d.Id, d.Name })
                       .ToList();
                return Json(students, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var students = db.Students
                    .Select(d => new { d.Id, d.Name })
                    .ToList();
                return Json(students, JsonRequestBehavior.AllowGet);
            }
        }

        //public ActionResult GetStudentsCourseList(int? id)
        //{
        //    if (id.HasValue)
        //    {
        //        using (var db = new StudentEntities1())
        //        {
        //            var courss = db.CourseStudents.Include("Cours").Where(s => s.StudentId == id).ToList();
        //            return Json(courss, JsonRequestBehavior.AllowGet);
        //        }
        //    }
        //    else
        //    {
        //        var db = new StudentEntities1();
        //        var students = db.Students
        //            .Select(d => new { d.Id, d.Name })
        //            .ToList();
        //        return Json(students, JsonRequestBehavior.AllowGet);
        //    }
           
        //} 
        public ActionResult GetStudentsCourseList(int? id)
        {
            if(id.HasValue)
            {
                var db = new StudentEntities1();
                var CourseDetails = db.CourseStudents.ToList();

                return Json(CourseDetails, JsonRequestBehavior.AllowGet);
                    
            // return Json(new { status = "ok", message = id }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { status = "error", message = "Invalid student ID" }, JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult QuantityIncrease(int? productId,int? customerId)
        {
            if (productId.HasValue && customerId.HasValue)
            {
                var db = new StudentEntities2();
                var data = db.OrderCarts.Where(i => i.CustomerId == customerId && i.ProductId == productId).SingleOrDefault();
                data.Quantity += 1;
                db.SaveChanges();
                return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { result = "Failed to Update" }, JsonRequestBehavior.AllowGet);
            }
            
        }   
        public ActionResult QuantityDecrease(int? productId,int? customerId)
        {
            if (productId.HasValue && customerId.HasValue)
            {
                var db = new StudentEntities2();
                var data = db.OrderCarts.Where(i => i.CustomerId == customerId && i.ProductId == productId).SingleOrDefault();
                    data.Quantity -= 1;
                    db.SaveChanges();
                    return Json(new { result = "success" }, JsonRequestBehavior.AllowGet);

               
            }
            else
            {
                return Json(new { result = "Failed to Update" }, JsonRequestBehavior.AllowGet);
            }
            
        }
    

    }
}