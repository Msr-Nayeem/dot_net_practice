using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Entity;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var db = new StudentEntities2();
            var data = db.Products.ToList();

            return View(data);
        }  


        public ActionResult NewCustomer()
        {
            return View();      
        }

        [HttpPost]
        public ActionResult NewCustomer(Customer info, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();
                int randomNumber = random.Next(10000, 99999);


                var extension = Path.GetExtension(file.FileName);
                string newFileName = info.CustomerName + randomNumber.ToString() + extension;

                var path = Path.Combine(Server.MapPath("~/UploadedFiles/Customer"), newFileName);
                file.SaveAs(path);

                info.ImageLink = newFileName;



                var db = new StudentEntities2();
                db.Customers.Add(info);
                int rowsAffected = db.SaveChanges();
                if (rowsAffected > 0)
                {
                    // the operation was successful
                    TempData["Result"] = "Registered";
                    return View();
                }
                else
                {
                    // the operation failed
                    // handle the error here, for example by displaying an error message to the user
                    TempData["Result"] = "failed to register";
                    return View(info);
                }
            }
            else
            {
                return View(info);
            }
            
        }



        public ActionResult Customers()
        {
            var db = new StudentEntities2();
            var list = db.Customers.ToList();
            return View(list);
        }

        public ActionResult CustomerInfo(int? id)
        {
            var db = new StudentEntities2();
            var data = db.Customers.Where(s => s.Id == id).SingleOrDefault();

            if (data != null)
            {

                return View(data);
            }
            else
            {
                TempData["result"] = "Connection error";
                return RedirectToAction("Customers");
            }
        }

        public ActionResult AllProducts()
        {
            return View();
        }        

        public ActionResult ProductDetails(int? id)
        {
            var db = new StudentEntities2();
            var data = db.Products.Include("Cetegory").Where(p => p.Id == id).SingleOrDefault();
            return View(data);
        }
        public ActionResult Cetegory()
        {
            var db = new StudentEntities2();
            var cet = db.Cetegories.ToList();
            return View(cet);
        }        
        public ActionResult OrderHistory()
        {
            return View();
        }

        public ActionResult NewProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewProduct(Product NewProduct,HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    //Method 2 Get file details from HttpPostedFileBase class    

                    if (file != null)
                    {
                        Random random = new Random();
                        int randomNumber = random.Next(10000, 99999);


                        var extension = Path.GetExtension(file.FileName);
                        string newFileName = "ProductPicture" + randomNumber.ToString() + extension;

                        var path = Path.Combine(Server.MapPath("~/UploadedFiles"), newFileName);
                        file.SaveAs(path);

                        NewProduct.ImageLink = newFileName;



                        var db = new StudentEntities2();
                        db.Products.Add(NewProduct);
                        int rowsAffected = db.SaveChanges();
                        if (rowsAffected > 0)
                        {
                            // the operation was successful
                            TempData["msg"] = "Product Added";
                            return View();
                        }
                        else
                        {
                            // the operation failed
                            // handle the error here, for example by displaying an error message to the user
                            TempData["msg"] = "failed to add product";
                            return View(NewProduct);
                        }

                        
                    }
                    else
                    {
                        ViewBag.FileStatus = "Error.";
                        return View(NewProduct);
                    }
                   
                }
                catch (Exception)
                {

                    ViewBag.FileStatus = "Error while file uploading.";
                    return View(NewProduct);
                }
            }
            else
            {
                return View(NewProduct);
            }
            

        }
        


        public ActionResult BuySingle(int? id)
        {
            var db = new StudentEntities2();
            var data = db.Products.Where(p => p.Id == id).SingleOrDefault();
           
            return View(data);
        }
    }
}