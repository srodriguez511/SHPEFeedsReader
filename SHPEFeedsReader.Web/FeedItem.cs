using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SHPEFeedsReader.Web
{
    public class FeedItem
    {
        public string Title { get; set; }
        public string Date { get; set; }
        public string Url { get; set; }
        public string Company { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostaslCode { get; set; }
        public string Department { get; set; }
        public string Description { get; set; }
        public string Categories { get; set; }
        public string Logo { get; set; }
    }
}