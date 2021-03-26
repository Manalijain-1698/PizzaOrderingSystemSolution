using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PizzaOrderingSystem.Models;

namespace PizzaOrderingSystem.Controllers
{
    public class RegisterCustomerController : Controller
    {
        // GET: RegisterCustomer
        PizzaOrderingSystemEntities db = new PizzaOrderingSystemEntities();
        public ActionResult Index()
        {
            return View();
        }
        //[Route("Registration")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterDetail registerdetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //List<string> ids = registerdetails.R_Id.ToString(); 
                    //registerdetails.R_Id=

                    int id = db.RegisterDetails.Max(x => x.R_Id);
                    registerdetails.R_Id = id + 1;
                    db.RegisterDetails.Add(registerdetails);
                    ViewBag.registermsg = "Registered Successfully!";
                    db.SaveChanges();
                    
                    var user = db.RegisterDetails.Single(u => u.R_Id == registerdetails.R_Id);
                    if (user.R_Role == "Customer")
                    {
                        CustomerDetail customerdetails = new CustomerDetail(user.R_Id, user.R_Username);
                        db.CustomerDetails.Add(customerdetails);
                        db.SaveChanges();
                        ViewBag.registermsg = "Registered Successfully!";
                    }
                    if (user.R_Role == "Admin")
                    {
                        AdminDetail Admindetails = new AdminDetail(user.R_Id, user.R_Username);
                        db.AdminDetails.Add(Admindetails);
                        db.SaveChanges();
                    }

                }
                catch (Exception e)
                {

                    ViewBag.message = e.Message;
                }
            }
            return View();
        }


        public JsonResult IsUserNameExits(string R_Username)
        {
            return Json(!db.RegisterDetails.Any(u => u.R_Username == R_Username), JsonRequestBehavior.AllowGet);
        }

    }

}
