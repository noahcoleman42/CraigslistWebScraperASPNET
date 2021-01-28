// Written by Noah Coleman
// 11/19/2020

using System.Collections.Generic;
using System.IO;

namespace CSWebScraperASPNET.Models
{
    public class FileGateway
    {
        public void SaveCraigslistPosts(string aPath, List<CraigslistPost> craigslistPosts)
        {
            // Adding column headers
            string contents = "Date, Price, Title, Link\n";

            // Adding posts to contents
            foreach (var p in craigslistPosts)
            {
                contents += p.ToString();
            }

            // Write contents to file location aPath. aPath should be a .csv file
            File.WriteAllText(aPath, contents);
        }
    }
}
