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
    public class APIProductController : Controller
    {
        // GET: APIProduct
        public ActionResult GetCetegoryList()
        {
            var db = new StudentEntities2();
            var cet = db.Cetegories.Select(d => new { d.Id, d.CetegoryName }).ToList();
            return Json(cet, JsonRequestBehavior.AllowGet);
        }
    }
}