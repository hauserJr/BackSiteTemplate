using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BackSiteTemplate.Models
{
    public class UserIPContainer
    {
        public string ip { get; set; }
    }
    public class GetUserPublicIP
    {
        private string myip = string.Empty;
        public string ip
        {
            get
            {
                var url = "https://api.ipify.org?format=json";
                var request = WebRequest.Create(url);
                var response = request.GetResponse() as HttpWebResponse;
                var responseStream = response.GetResponseStream();
                var reader = new StreamReader(responseStream, Encoding.GetEncoding("utf-8"));
                var srcString = reader.ReadToEnd();
                var myip = Newtonsoft.Json.JsonConvert.DeserializeObject<UserIPContainer>(srcString);
                return myip.ip;
            }
        }
    }

}
