using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace bot2
{
    class AntiCaptcha
    {
        private WebClient client = new WebClient();
        private string antigateKey="";

        public AntiCaptcha() { }
        public AntiCaptcha(string APIantigateKey)
        {
            antigateKey = APIantigateKey;
        }

        public static bool isCaptha(string html)
        {
            if (html.IndexOf("captchaimg?") > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public string getCode(string base64Img) 
        {
            var data = new NameValueCollection();   
            data.Add("is_russian", "1");
            data.Add("method", "base64");
            data.Add("key", this.antigateKey);
            data.Add("body", base64Img);
            return "";
        }
    }
}
