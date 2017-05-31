using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YodaProject.Models;
using Microsoft.AspNet.Identity;
namespace YodaProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.RandomQuote = GetRandomQuote();

            return View();
        }


        public ActionResult SurveyView()
        {
            return View();
        }
        public ActionResult SurveyResult()
        {
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public string GetRandomQuote()

        {
            //GETTING THE RAW TEXT DATA from JSON
            HttpWebRequest request = WebRequest.CreateHttp("https://andruxnet-random-famous-quotes.p.mashape.com/");

            //request.UserAgent = @"User-Agent: Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.116 Safari/537.36";
            request.Headers.Add("X-Mashape-Key", "JO4bkhuxbXmshFnHFjLnocW3jFA9p1bgnIbjsnd2uZBpdOEv5y");
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader rd = new StreamReader(response.GetResponseStream());

            string data = rd.ReadToEnd();

            JObject RandomQuote = JObject.Parse(data);


            return RandomQuote["quote"].ToString();

        }
        public ActionResult Survey(string Question1, string Question2, string Question3, string Question4)
        {
            YodaUserDBEntities DB = new YodaUserDBEntities();
            string input1 = Question1.Substring(0, 2);
            string input2 = Question2.Remove(0, 1);
            string input3 = Question3.Remove(0, 1);
            string input4 = Question4.Substring(0, 2);


            // call a method 
            ViewBag.Message1 = GetMessage1(input1);
            ViewBag.Message2 = GetMessage2(input2);
            ViewBag.Message3 = GetMessage3(input3);
            ViewBag.Message4 = GetMessage4(input4);

            // ViewBag.....


            SurveyTable Temp = new SurveyTable();

            Temp.UserID = User.Identity.GetUserId();

            Temp.Question1 = input1;
            Temp.Question2 = input2;
            Temp.Question3 = input3;
            Temp.Question4 = input4;

            DB.SurveyTables.Add(Temp);
            DB.SaveChanges();

            return View("SurveyResult");
        }

        public string GetMessage1(string input1)
        {
            int value1 = Convert.ToInt32(input1);
            
            if (value1 != 40)
            {
               return  ("Unfortunantly, paying $20 (4%) of the $500 balance, is the minimum payment and will take 40 months.");
            }
            else
            {
                return ("Correct.Owing $500 will take 40 months, if you only pay $20 a month. Total payments = $ 634.25.");
            }
        }
        public string GetMessage2(string input2)
        {
            int value2 = Convert.ToInt32(input2);
            if (value2 != 100)
            {
                return ("Saving $100 a month, would be a great goal to get you on your way to having over $10,000. According to Fidelity.Com if you are only Short - Term saving 5 % is acceptable, but 15 % should be saved towards retirement.Keep in mind. By age 30, $50,000 in savings is optimal.By age 35, Twice your annual salary should be saved. By age 40, Three times your annual salary should be saved. By age 45, Four times your annual salary should be saved.");
            }
            else
            {
                return ("Earning $10 an hour while working 30 hrs a week, you will be bringing home about $1000 a month. Always save a minimum of 10 % of your earnings.");
            }
        }
        public string GetMessage3(string input3)
        {
            int value3 = Convert.ToInt32(input3);
            if (value3 == 50)
            {
                return ("$50 would only cover Maintenence or possibly Registration, fees and taxes for your car, on average.What about gas & insurance?");
            }
            else if (value3 == 200 || value3 == 300)
            {
               return ("Close! As average costs of insurance are $80 -$200 a month, with $100 approximately for maintenance, registration, fees and tases.What about gas ?");
            } 
               else 
            {
                return ("Correct.It is advised to budget for for $500 a month. AAA estimates maintenance costs at around 4 cents per mile driven, which is about $622 a year if you’re driving 15,000 miles.This means your budget should set aside $50 - 60 / month for maintenance. Insurance your Budget should include $80 - 200 / month. Registration, fees, taxes budget should be $40 / month. Fuel Budget $100 - 250 / month.");
            }
        }
        public string GetMessage4(string input4)
        {
            int value4 = Convert.ToInt32(input4);
            if (value4 == 20 || value4 == 30)
            {
                return ("According to the Bureau of Labor Statistics, the mean wage for 20 - to 24 - year - olds across all education levels in the third quarter of 2016 was $625 a week, or $32, 500 a year.");
            }
            else 
            {
                return ("It's never to start saving and/or spending within your budget. For 25 - to 34 - year - olds, it was $966 a week, or $50,232 a year. The average salary for a recent college graduate with a bachelor’s degree was $50, 219 a year in 2015, according to the National Association of Colleges and Employers.");
            }
        } 
        
    }
}
        //public ActionResult Register()
        //{
        //    ViewBag.Message = "Welcome to YODA Finance!";
        //    return View();
        //}
        //public ActionResult AddUser(UserInfo NewUser)
        //{
        //    // VALIDATION ! //
        //    //USED TO CALL/ADD DATA FROM THE MODEL to the DATABASE//

        //    return View(NewUser);//pass this NewUser model to the AddUser View     

        //}
        

    //    public ActionResult Delete(string Name)
    //    {
    //         YodaUserDB DB = new YodaUserDB();

    //        //step #1 "find the custoemr that I need to delete....//
    //        Item ToDelete = DB.Items.Find(Name);

    //        if (ToDelete == null)
    //        {
    //            ViewBag.ErrorMessage = "Unavailable";
    //            return View("ErrorMessage");
    //        }

    //        //removes OBJECTS from DATABASE

    //        DB.Items.Remove(ToDelete);

    //        //to SAVE EVERYTHING TO DATABASE.....
    //        DB.SaveChanges();

    //        return RedirectToAction("Admin");
    //    }
    //    public ActionResult Update(string Name)
    //    {
    //        //first thing...TO SHOW DATA

    //        YodaUserDB DB = new YodaUserDB();

    //        //update will SHOW up & THE MODEL will be updated......
    //        Item ToFind = DB.Items.Find(Name);

    //        return View("Edit", ToFind);
    //    }


    //    
    //}
//}