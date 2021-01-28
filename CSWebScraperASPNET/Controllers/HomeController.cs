// Written by Noah Coleman
// 11/19/2020

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using CSWebScraperASPNET.Models;

namespace CSWebScraperASPNET.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        // Default action result before searching for something
        public ActionResult ScrapeCraigslist()
        {
            return View();
        }

        [HttpPost]
        // HttpPost is an "Action Selector". We use Post bc we are creating the page's database.
        // When we only want to get things from the database and change what we see, we will use HttpGet
        // Note: HttpPut, HttpDelete, HttpMerge etc are methods as well.
        // The method below executes if the request method is HttpPost
        public ActionResult ScrapeCraigslist(string aSearch, string aCity)
        {
            CraigslistScraper aCraigslistScraper = new CraigslistScraper();
            List<CraigslistPost> aListOfCraigslistPosts;
            FileGateway aFileGateway = new FileGateway();

            // Search Craislist
            aListOfCraigslistPosts = aCraigslistScraper.ScrapeCraigslist(aSearch, aCity);

            // Sorting by price
            var priceLowToHigh = from p in aListOfCraigslistPosts
                                 orderby p.Price ascending
                                 select p;

            // Sorting by date
            var dateNewToOld = from p in aListOfCraigslistPosts
                               orderby p.Date descending
                               select p;

            ViewBag.ListOfCraigslistPosts = dateNewToOld;
            //ViewBag.ListOfCraigslistPosts = priceLowToHigh;
            //ViewBag.ListOfCraigslistPosts = aListOfCraigslistPosts;

            ViewBag.Check = "true";

            return View();
        }
    }
}
