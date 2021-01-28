// Written by Noah Coleman
// 11/19/2020

using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace CSWebScraperASPNET.Models
{
    public class CraigslistScraper
    {
        // This method takes two strings: a search and a city, and returns a list of Craigslist Post objects
        public List<CraigslistPost> ScrapeCraigslist(string aSearch, string aCity)
        {
            // Inizializing WebDriver
            ChromeDriver driver = new ChromeDriver("C:\\Program Files (x86)\\Mine\\Apps");

            // Making a List of Craigslist Posts;
            List<CraigslistPost> aListOfCraigslistPosts = new List<CraigslistPost>();

            // Fix city url errors
            string city = aCity.Replace(" ", "").ToLower();

            // Navigate to the URL
            driver.Navigate().GoToUrl("https://" + city + ".craigslist.org/");

            // Maximize te window
            driver.Manage().Window.Maximize();

            // Implicitly wait for a second before timing out.
            // Timeouts occur when there is no price, or a listing is deleted, so none of the elements are on the page.
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(1000);

            // Creating a wait object to until expected elements appear on the page
            WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 5));

            // Enter the search into the search box and press enter
            IWebElement searchBox = wait.Until(condition => condition.FindElement(By.Id("query")));
            searchBox.Clear();
            searchBox.SendKeys(aSearch);
            searchBox.Submit();

            // Defining variables that will be in try/catch blocks
            IWebElement date;
            IWebElement post;
            IWebElement postTitle;
            IWebElement description;
            IWebElement price;
            string dateText;
            string linkText;
            string titleText;
            string descriptionText;
            double priceDouble;

            // Figure out how many posts we'll have to go through
            IReadOnlyCollection<IWebElement> posts = wait.Until(condition => condition.FindElements(By.ClassName("result-row")));
            int numberOfPosts = posts.Count();

            // Go through all posts on a page and scrape useful information
            for (int i = 1; i < numberOfPosts + 1; i++)
            {
                post = wait.Until(condition => condition.FindElement(By.XPath("//*[@id=\"sortable-results\"]/ul/li[" + Convert.ToString(i) + "]/a")));
                // Link
                linkText = post.GetAttribute("href");
                post.Click();

                try
                {
                    // Date
                    date = driver.FindElementByXPath("/html/body/section/section/section/div[2]/p[2]/time");
                    // Get rid of this if deleted posts are causing problems
                    //date = wait.Until(condition => condition.FindElement(By.XPath("/html/body/section/section/section/div[2]/p[2]/time")));
                    date.Click();
                    dateText = date.Text;

                    // Title
                    postTitle = driver.FindElementById("titletextonly");
                    titleText = postTitle.Text.Replace(",", " ");

                    // Description
                    description = driver.FindElementById("postingbody");
                    descriptionText = description.Text;
                    descriptionText = description.Text.Replace(",", " ");

                    // try to get price information
                    try
                    {
                        price = driver.FindElementByClassName("price");
                        priceDouble = Convert.ToDouble(price.Text.Replace(",", "").Substring(1));

                        // Make a new Craigslist Post Object and add it to the list
                        CraigslistPost aCraigslistPost = new CraigslistPost(dateText, priceDouble, titleText, descriptionText, linkText);
                        aListOfCraigslistPosts.Add(aCraigslistPost);
                    }
                    // If there is no price, catch the exception
                    catch (NoSuchElementException)
                    {
                        CraigslistPost aCraigslistPost = new CraigslistPost(dateText, titleText, descriptionText, linkText);
                        aListOfCraigslistPosts.Add(aCraigslistPost);
                    }
                    finally
                    {
                        driver.Navigate().Back();
                    }
                }
                // If the post was deleted, catch the exception
                catch (NoSuchElementException)
                {
                    driver.Navigate().Back();
                }
            }

            // Close the opened tab
            driver.Quit();

            // Return the list of craigslist posts
            return aListOfCraigslistPosts;
        }
    }
}
