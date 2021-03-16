using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PizzaOrderingSystem.Models;

namespace PizzaOrderingSystem.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        PizzaOrderingSystemEntities db = new PizzaOrderingSystemEntities();

        public ActionResult CustomerNavigate()
        {
            if (Session["Username"] != null)
            {
                Session["Uname"] = Session["Username"].ToString();
                int UserId = (int)TempData.Peek("UserId");
                TempData["CustomerIdForOrder"] = UserId;
                string Username = TempData.Peek("Username").ToString();
                TempData["CustomerName"] = Username;
                string Address = TempData.Peek("Address").ToString();
                TempData["CustomerAddress"] = Address;
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
        }
        public ActionResult ViewAllPizzas(string searchingpizza)
        {
            if (Session["Uname"] != null)
            {
                var pizzadata = from pizza in db.PizzaDetails select pizza;
                if (!string.IsNullOrEmpty(searchingpizza))
                {
                    pizzadata = pizzadata.Where(p => p.PizzaName.Contains(searchingpizza));
                }
                return View(pizzadata.ToList());
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
        }

        public ActionResult Edit()
        {
            if (Session["Uname"] != null)
            {
                var pizzalist = db.PizzaDetails.ToList();
                ViewBag.pizzanames = new SelectList(pizzalist, "PizzaName", "PizzaName");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
        }
        [HttpPost]
        public ActionResult Edit(PizzaDetail pizzadetails)
        {
           
            try
                {
                var updatedata = db.PizzaDetails.Single(x => x.PizzaName == pizzadetails.PizzaName);
                var pizzalist = db.PizzaDetails.ToList();
                ViewBag.pizzanames = new SelectList(pizzalist, "PizzaName", "PizzaName");
                updatedata.PizzaName = pizzadetails.PizzaName;
       
                updatedata.Quantity = pizzadetails.Quantity;
                updatedata.TotalPrice =updatedata.PizzaPrice*updatedata.Quantity;
                TempData["PizzaName"] = updatedata.PizzaName;
                TempData["OrderedPizzaId"] = updatedata.PizzaId;
                TempData["TotalAmount"] = updatedata.TotalPrice;
                db.SaveChanges();
                ViewBag.Updatemessage = "Quantity updated Succesfully!";
                
            }
            catch (Exception e)
            {
                ViewBag.Message = e.Message;
            }

            return View();
            
        }


       
        

        public ActionResult ViewAllPizzaImages()
        {
            if (Session["Uname"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
                
        }

        public ActionResult OrderNow()
        {
            if (Session["Uname"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
        }
        [HttpPost]
        public ActionResult OrderNow(OrderDetail orderdetails)
        {
           
            
            try
            {
                var PizzaId = (int)TempData["OrderedPizzaId"];
                var CustomerId = (int)TempData.Peek("CustomerIdForOrder");
                orderdetails.PizzaId = PizzaId;
                orderdetails.CustomerId = CustomerId;
                
                var ordereddata = db.OrderDetails.Add(orderdetails);
                TempData["OrderId"] = ordereddata.OrderId;
                db.SaveChanges();
                ViewBag.ordermessage = "Order placed Successfully!";
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                
            }
            
            return View();
        }

        public ActionResult Paymentdetails()
        {
            if (Session["Uname"] != null)
            {
                //var paymentmodelist = db.PaymentDetails.Distinct().ToList();
                //ViewBag.payamodemethod = new SelectList(paymentmodelist, "Paymentmode", "Paymentmode");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
        }

        [HttpPost]
        public ActionResult Paymentdetails(PaymentDetail paymentdetails)
        {
            try
            {
                var OrderId = (int)TempData["OrderId"];
                paymentdetails.OrderId = OrderId;
                var paymentdata = db.PaymentDetails.Add(paymentdetails);
                
                //var paymentmodelist = db.PaymentDetails.Distinct().ToList();
                //ViewBag.payamodemethod = new SelectList(paymentmodelist, "Paymentmode", "Paymentmode");
                db.SaveChanges();
                ViewBag.paymentmessage = "Payment Successfull!";
            }
            catch (Exception e)
            {

                ViewBag.message = e.Message;
            }

            return View();
        }

        public ActionResult ViewOrderDetails()
        {

            try
            {

                var PizzaId = TempData["OrderedPizzaId"];
                var PizzaName = TempData["PizzaName"];
                var TotalAmount = TempData["TotalAmount"];
                string CustomerName = TempData.Peek("CustomerName").ToString();
                string CustomerAddress = TempData.Peek("CustomerAddress").ToString();
                var CustomerId = TempData.Peek("CustomerId");
                var OrderId = TempData["OrderId"];
                ViewBag.pid = PizzaId;
                ViewBag.cname = CustomerName;
                ViewBag.Cadd = CustomerAddress;
                ViewBag.pname = PizzaName;
                ViewBag.totalamount = TotalAmount;
                ViewBag.CustomerId = CustomerId;
                ViewBag.OrderId = OrderId;

            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;

            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "LoginCustomer");
        }


    }
}