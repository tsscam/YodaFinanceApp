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
        //A model stores data that is retrieved according to commands from the controller and the instance of an object is displayed in the view.
        
        //A controller can send commands to the model to update the model's state (e.g., editing a document). It can also send commands to its associated view to change the view's presentation of the model (e.g., scrolling through a document).
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                ApplicationDbContext userInfo = new ApplicationDbContext();
                string fname = userInfo.Users.Find(User.Identity.GetUserId()).FirstName;
                ViewBag.FirstName = fname; 
            }


            ViewBag.RandomQuote = GetRandomQuote();

            return View();
        }
        //Action used to send NewUser obtained info from user and sent to Model
        public ActionResult AddUser(ApplicationUser NewUser)
        {

            return View(NewUser);
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
            //key is used to gain access to the IP
            request.Headers.Add("X-Mashape-Key", "JO4bkhuxbXmshFnHFjLnocW3jFA9p1bgnIbjsnd2uZBpdOEv5y");
            request.ContentType = "application/x-www-form-urlencoded";
            //file type of from the website
            request.Accept = "application/json";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            //Getting the response as an object
            StreamReader rd = new StreamReader(response.GetResponseStream());
            //turns object to a string
            string data = rd.ReadToEnd();
            //creates object new quote of string
            JObject RandomQuote = JObject.Parse(data);

            //displays the string
            return RandomQuote["quote"].ToString();

        }
        
        public ActionResult Survey(string Question1, string Question2, string Question3, string Question4)
        {
            //the below is creating objects
            YodaUserDBEntities DB = new YodaUserDBEntities();
            //the below is how the information is parsed and only numeric values are used
            string input1 = Question1.Substring(0, 2);
            string input2 = Question2.Remove(0, 1);
            string input3 = Question3.Remove(0, 1);
            string input4 = Question4.Substring(0, 2);


            // call a method 
            ViewBag.Message1 = GetMessage1(input1);
            ViewBag.Message2 = GetMessage2(input2);
            ViewBag.Message3 = GetMessage3(input3);
            ViewBag.Message4 = GetMessage4(input4);

            // Sent to the ViewBag.....

            //object
            SurveyTable Temp = new SurveyTable();
            //assigning new variable (user ID)
            Temp.UserID = User.Identity.GetUserId();
            
            //assigning the user input to a variable
            Temp.Question1 = input1;
            Temp.Question2 = input2;
            Temp.Question3 = input3;
            Temp.Question4 = input4;
            //creates each variable within the database
            DB.SurveyTables.Add(Temp);
            //saves to the database
            DB.SaveChanges();

            return View("SurveyResult");
        }
        //each of these (Message1,2,3,4)are called on by the constructors in the models
        public string GetMessage1(string input1)
        {
            int value1 = Convert.ToInt32(input1);
            
            if (value1 <= 20)
            {
               return  ("Unfortunantly, if you only pay $20 (the min) at 15% interest it will take 40 months.");
            }
            else if (value1 == 40)
            {
                return ("You are Amazing. Correct, Answer! Owing $500 will take 40 months, if you only pay $20 a month. Total payments = $ 634.25.");
            }
            else
            {
                return ("While your thought process is on the right track, 60 months is too long. Correct answer is 40 months.");
            }
        }
        public string GetMessage2(string input2)
        {
            int value2 = Convert.ToInt32(input2);
            if (value2 == 100)
            {
                return ("Saving $100 a month, would be a great goal to get you on your way to a saving goal. In 10 years you could have $10,000. According to Fidelity.Com if you are only saving 5 % is acceptable, but 15 % should be saved towards retirement.Keep in mind. By age 30, $50,000 total saved is advised. By age 35, you should have 2X's your annual salary saved. By age 40, you should have 3X's your annual salary should be saved. By age 45, 4X times your annual salary should be saved. GOOD LUCK!");
            }
            else if (value2 <= 60 )
            {
                return ("Keep in mind, even if you earn $10 an hour & work 30 hrs a week, you will be bringing home about $1000 a month. Always save a minimum of 10 % of your earnings.According to Fidelity.Com if you are only saving 5 % is acceptable, but 15 % should be saved towards retirement.Keep in mind. By age 30, $50,000 total saved is advised. By age 35, you should have 2X's your annual salary saved.");
            }
            else
            {
                return ("More Power to you! If you can save 1/2 of what you earn, you will be a YODA MASTER of YOUR FINANCE! According to Fidelity.Com if you are only saving 5 % is acceptable, but 15 % should be saved towards retirement.Keep in mind. By age 30, $50,000 total saved is advised. By age 35, you should have 2X's your annual salary saved. By age 40, you should have 3X's your annual salary should be saved. By age 45, 4X times your annual salary should be saved. GOOD LUCK!");
            }
        }
        public string GetMessage3(string input3)
        {
            int value3 = Convert.ToInt32(input3);
            if (value3 == 50)
            {
                return ("$50 would only cover Car Maintenence or possibly Car Registration Fees and Taxes on your car. What about gas & insurance? This could result in hundreds more.");
            }
            else if (value3 == 200 || value3 == 300)
            {
               return ("Close. On average costs of insurance are $80-$200 a month, with $100 approximately for maintenance, registration, fees and taxes. What about gas expense? Gas typically runs $50-200 a month");
            } 
               else 
            {
                return ("Correct, 'Yoda Finance Master'. It is advised to budget for for $500 a month. According to AAA, your budget should include $50 - $60 a month for maintenance. Insurance alone ranges $80-200 a month, or more. Registration, fees and taxes are about $40 a month. Fuel expense is $50 - $250 amonth.");
            }
        }
        public string GetMessage4(string input4)
        {
            int value4 = Convert.ToInt32(input4);
            if (value4 == 20 || value4 == 30)
            {
                return ("According to the Bureau of Labor Statistics, the mean wage for 20 to 24 year olds across all education levels in 2016 was $625 a week, or $32, 500 a year.");
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