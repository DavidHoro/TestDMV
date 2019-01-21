using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BL
{
    public class CalculetDistance
    {
        public CalculetDistance(string Target, string Origin)
        {
            this.Target = Target;
            this.Origin = Origin;
        }
        private string KEY = "WAgoN8tjC8czDMMReR5JwxrEqTAoHc6s‏";
        private string BaseURL = @"https://www.mapquestapi.com/directions/v2/route";
        private string EndURL = @"&outFormat=xml&ambiguities=ignore&routeType=fastest&doReverseGeocode=false&enhancedNarrative=false&avoidTimedConditions=false";
        public string Target { get; set; }
        public string Origin { get; set; }

        public double Calc()
        {
            string url = string.Format("{0}?key={1}&from={2}&to={3}{4}", this.BaseURL, this.KEY, this.Origin, this.Target,this.EndURL);
            //request from MapQuest service the distance between the 2 addresses 
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader sreader = new StreamReader(dataStream);
            string responsereader = sreader.ReadToEnd();
            response.Close(); //the response is given in an XML format 
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(responsereader);

            if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "0")
            {
                XmlNodeList distance = xmldoc.GetElementsByTagName("distance");
                double distInMiles = Convert.ToDouble(distance[0].ChildNodes[0].InnerText);
                return distInMiles * 1.609344;
            }
            else if (xmldoc.GetElementsByTagName("statusCode")[0].ChildNodes[0].InnerText == "402")
            {
                return -1;
            }
            else //busy network or other error... 
            {
                return -2;
            }
        }
    }
}
