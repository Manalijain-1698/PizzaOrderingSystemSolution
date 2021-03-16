using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaOrderingSystem.Models;

namespace PizzaOrderingSystem.Controllers
{
    public class LoginCustomerController : Controller
    {
        // GET: LoginCustomer
        PizzaOrderingSystemEntities db = new PizzaOrderingSystemEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginDetail logindetails)
        {
            try
            {
                var user = db.RegisterDetails.Single(u => u.R_Username == logindetails.L_Username && u.R_Password == logindetails.L_Password);
                if (user != null)
                {
                    if (user.R_Role == "Customer")
                    {
                        Session["Username"] = user.R_Username;
                        RedirectToAction("CustomerNavigate", "Customer");
                        TempData["UserId"] = user.R_Id;
                        TempData["Username"] = user.R_Username;
                        TempData["Address"] = user.R_Address;
                        
                        
                        return RedirectToAction("CustomerNavigate", "Customer");
                    }
                    if (user.R_Role == "Admin")
                    {
                        Session["Username"] = user.R_Username;
                        
                        return RedirectToAction("AdminNavigate", "Admin");
                    }
                }


            }
            catch (Exception)
            {
                ViewBag.message = "User Not Found !! Please Enter the correct Username and Password !!";

            }
            return View();
        }




    }
}