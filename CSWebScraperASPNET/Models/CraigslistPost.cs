// Written by Noah Coleman
// 11/19/2020

namespace CSWebScraperASPNET.Models
{
    public class CraigslistPost
    {
        // Class variables
        private string date = "n/a";
        private double price = -1.0;
        private string title = "n/a";
        private string description = "n/a";
        private string link = "n/a";

        // Accessors
        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public string Link
        {
            get { return link; }
            set { link = value; }
        }

        // Constructors
        public CraigslistPost(string aDate, double aPrice, string aTitle, string aDescription, string aLink)
        {
            this.Date = aDate;
            this.Price = aPrice;
            this.Title = aTitle;
            this.Description = aDescription;
            this.Link = aLink;
        }
        public CraigslistPost(double aPrice, string aTitle, string aDescription, string aLink) : this("n/a", aPrice, aTitle, aDescription, aLink)
        {
            // Chained constructor for no date
        }
        public CraigslistPost(string aDate, string aTitle, string aDescription, string aLink) : this(aDate, -1, aTitle, aDescription, aLink)
        {
            // Chained constructor for no price
        }
        public CraigslistPost(string aTitle, string aDescription, string aLink) : this("n/a", -1, aTitle, aDescription, aLink)
        {
            // Chained constructor for no date and no price
        }
        public CraigslistPost()
        {
            // empty constructor
        }

        // Methods
        public override string ToString()
        {
            string message = "";
            message += this.Date + ", ";
            message += this.Price + ", ";
            message += this.Title + ", ";
            //message += this.Description + ", ";
            message += this.Link + "\n";
            return message;
        }
    }
}
