using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace YodaProject.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.RandomQuote = GetRandomQuote();

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
        //public ActionResult Admin()
        //{
        //    ViewBag.Message = "Administration";

        //     YodaUserDB DB = new YodaUserDB();

        //    //select * from customers
        //    List<Item> ItemList = DB.Items.ToList();

        //    ViewBag.ItemList = ItemList;

        //    return View();
        //}

        //public ActionResult Add()
        //{
        //    return View();
        //}

    //    public ActionResult AddNewItem(Item NewItem)
    //    {
    //        YodaUserDB DB = new YodaUserDB();

    //        DB.Items.Add(NewItem);

    //        DB.SaveChanges();

    //        return RedirectToAction("Admin");
    //    }

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


    //    public ActionResult SaveUpdates(Item ToBeUpdated)
    //    {
    //        YodaUserDB DB = new YodaUserDB();
    //        //find the original customer record
    //        Item ToFind = DB.Items.Find(ToBeUpdated.Name);

    //        ToFind.Name = ToBeUpdated.Name;

    //        ToFind.Description = ToBeUpdated.Description;

    //        ToFind.Quantity = ToBeUpdated.Quantity;

    //        ToFind.Price = ToBeUpdated.Price;

    //        DB.SaveChanges();
    //        return RedirectToAction("Admin");
    
    //    }
    //}
//}