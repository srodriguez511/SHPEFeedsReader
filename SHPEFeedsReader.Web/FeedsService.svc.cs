using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using System.Xml;
using System.Xml.Serialization;
using System.Web;

namespace SHPEFeedsReader.Web
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FeedsService : IFeedsService
    {
        public IEnumerable<FeedItem> GetJobs2WebFeed(string uri)
        {
            XmlReaderSettings sett = new XmlReaderSettings();
            sett.XmlResolver = null;
            sett.DtdProcessing = DtdProcessing.Parse;
            XmlReader reader = XmlReader.Create(uri, sett);
            XmlSerializer serializer = new XmlSerializer(typeof(source));
            source temp = (source)serializer.Deserialize(reader);

            var _jobs = temp.jobs.job.ToList<source.jobclass>();
            var items = from q in _jobs
                        select new FeedItem
                        {
                            Title = q.title,
                            Description = HttpUtility.HtmlDecode(q.description),
                            Url = q.url,
                            Date = q.date
                        };

            return items;
        }

        [XmlRoot]
        public class source
        {
            [XmlElement]
            public string publisher { get; set; }
            [XmlElement]
            public string publisherurl { get; set; }
            [XmlElement("jobs")]
            public jobsclass jobs { get; set; }


            public class jobsclass
            {
                [XmlElement("job")]
                public jobclass[] job { get; set; }
            }


            public class jobclass
            {
                [XmlElement]
                public string title { get; set; }
                [XmlElement]
                public string date { get; set; }
                [XmlElement]
                public string referencenumber { get; set; }
                [XmlElement]
                public string url { get; set; }
                [XmlElement]
                public string company { get; set; }
                [XmlElement]
                public string city { get; set; }
                [XmlElement]
                public string state { get; set; }
                [XmlElement]
                public string country { get; set; }
                [XmlElement]
                public string postalcode { get; set; }
                [XmlElement]
                public string dept { get; set; }
                [XmlElement]
                public string department { get; set; }
                [XmlElement]
                public string description { get; set; }
                [XmlElement]
                public string categories { get; set; }

            }
        }
    }
}
