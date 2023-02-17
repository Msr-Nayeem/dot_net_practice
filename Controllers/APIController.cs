using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;

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
    }
}