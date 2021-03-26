using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Nexmo.Api;
using PizzaOrderingSystem.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Message = PizzaOrderingSystem.Models.Message;

namespace PizzaOrderingSystem.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        PizzaOrderingSystemEntities db = new PizzaOrderingSystemEntities();
        public ActionResult AdminNavigate()
        {
            if (Session["Username"] != null)
            {
                Session["Uname"]= Session["Username"].ToString();
                
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
            
        }

        public ActionResult AddPizzas()
        {
            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
        }
        [HttpPost]
        public ActionResult AddPizzas(PizzaDetail pizzadetails)
        {

            try
            {
                int id = db.PizzaDetails.Max(x => x.PizzaId);
                pizzadetails.PizzaId = id + 1;
                db.PizzaDetails.Add(pizzadetails);
                db.SaveChanges();
                ViewBag.addmessage = "Pizza Added Successfully!";
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                
            }
            return View();
        }

        public ActionResult ViewPizzas()
        {
            if (Session["Uname"] != null)
            {
                var viewpizzadata = db.PizzaDetails.ToList();
                return View(viewpizzadata);
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
        }

        public ActionResult AddIngradients()
        {

            if (Session["Username"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }


        }
        [HttpPost]
        public ActionResult AddIngradients(IntegradientDetail integradientdetails)
        {
            try
            {
                int id = db.IntegradientDetails.Max(x => x.IngradientId);
                integradientdetails.IngradientId = id + 1;
                db.IntegradientDetails.Add(integradientdetails);
                db.SaveChanges();
                ViewBag.addmessage = "Pizza Ingredients Added Successfully!";
            }
            catch (Exception e)
            {
                ViewBag.message = e.Message;
                
            }
            return View();
        }

        public ActionResult ViewStatusofIngradients()
        {
            if (Session["Uname"] != null)
            {
                var statusofIngradientsdata = db.IntegradientDetails.ToList();
                return View(statusofIngradientsdata);
            }
            else
            {
                return RedirectToAction("Login", "LoginCustomer");
            }
        }

        

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login", "LoginCustomer");
        }

        //[HttpGet]
        //public ActionResult SendMessage()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult SendMessage(Message message)
        //{
        //    var results = SMS.Send(new SMS.SMSRequest
        //    {
        //        from = Configuration.Instance.Settings["appsettings:NEXMO_FROM_NUMBER"],
        //        to = message.PhoneNumber,
        //        text = message.ContentMsg
        //    });


        //    return View();
        //}

        public ActionResult GetOrderDetails(OrderDetail orderdetails)
        {
            var ordereddata = db.OrderDetails.ToList();
            //TempData["Phonenumber"] = orderdetails.PhoneNumber;
           
            return View(ordereddata);
        }

        public ActionResult SendSMS()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendSMS(Message sms)
        {
            if (ModelState.IsValid)
            {
                // Find your Account Sid and Auth Token at twilio.com/console
                const string accountSid = "AC71e61b2fa9bf7ede2f7a325624fa28b2";
                const string authToken = "02c27d68c8bea76ec861dca65349ced7";

                TwilioClient.Init(accountSid, authToken);

                

                //https://support.twilio.com/hc/en-us/articles/223183068-Twilio-international-phone-number-availability-and-their-capabilities
                var to = new PhoneNumber("+91" + sms.PhoneNumber);
                
                var message = MessageResource.Create(
                    to,
                    //First navigate to your test credentials https://www.twilio.com/user/account/developer-tools/test-credentials
                    // then you need to get Twilio number from https://www.twilio.com/console/voice/numbers
                    from: new PhoneNumber("+16179344684"), //  From number, must be an SMS-enabled Twilio number ( This will send sms from ur "To" numbers ). 
                    body: $"Hello " + sms.Name + " !! Your Order has been confirmed! Thank You from Pizza Mania!!");
                    

                ViewBag.Message = "Message Sent";
                return RedirectToAction("SendSMS");
            }
            else
            {
                return View();
            }
        }


    }
}