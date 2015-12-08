using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace bot2
{
    class Tasker
    {
        private WebClient client = new WebClient();

        public string status = "Ready";

        private string workerId = "";
        public string getWorkerId() 
        {
            return this.workerId;
        }
        public string register()
        {
            status = "register";
            
            string data = getString(new Uri("http://:3333/register"));

            string id = "";
            string host = "";
            string port = "";

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(data);


            XmlNode idNode = doc.SelectSingleNode("/tasks/task/worker")["id"];

            if (idNode != null)
            {
                id = idNode.InnerText;
            }


            XmlNode hostNode = doc.SelectSingleNode("/tasks/task/proxy")["host"];
            if (hostNode != null)
            {
                host = hostNode.InnerText;
            }

            XmlNode portNode = doc.SelectSingleNode("/tasks/task/proxy")["port"];
            if (portNode != null)
            {
                port = portNode.InnerText;
            }            

            string proxy = "";
            if (host != "" && port != "")
            {
                proxy = host + ":" + port;
            }

            workerId = id;
            status = "registerFinish";
            return proxy;
        }

        public string taskId = "";
        public string word = "";
        public int regionId = 0;
        public string region = "";        
        public bool getNewTask() 
        {
            isTaskComplete = false;
            try
            {
                string data = getString(new Uri("http://:3333/task/" + workerId));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                XmlNode xn = doc.SelectSingleNode("/tasks/task")["id"];
                if (xn == null || xn.InnerText == "")
                {
                    throw new Exception("Нет id Задачи");
                }
                taskId = xn.InnerText;


                xn = doc.SelectSingleNode("/tasks/task");
                if (xn == null || xn["query"].InnerText == "")
                {
                    throw new Exception("Нет ключевого");
                }
                word = xn["query"].InnerText;

                xn = doc.SelectSingleNode("/tasks/task/region");
                if (xn == null)
                {
                    throw new Exception("Нет региона");
                }

                region = xn["userfriendly"].InnerText;
                regionId = Convert.ToInt32(xn["id"].InnerText);
                
                return true;
            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
                isTaskComplete = true;
                return false;
            }
            
        }

        public bool isTaskComplete = true;
        

        private string getString(Uri uri)
        {            
            byte[] bytes = Encoding.Default.GetBytes(client.DownloadString(uri));
            string data = Encoding.UTF8.GetString(bytes);
            return data;
        }

        private string toBase64(string data)
        {
            var bytes = Encoding.UTF8.GetBytes(data);
            var base64 = Convert.ToBase64String(bytes);
            Debug.WriteLine(base64);
            return base64;
        }

        public string sendResult(List<string> data)
        {
            try
            {
               HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://:3333/result/" + taskId);
               using (MultiPartForm multiPart = new MultiPartForm(webRequest))
               {
                   //multiPart.AddData("method", "post");
                   //multiPart.AddData("key", "e19567160d1dd900b436978ef3c0c060");
                   switch (data.Count)
                   {
                       case 2:
                           multiPart.AddData("serpa", toBase64(data[0]));
                           multiPart.AddData("serpb", toBase64(data[1]));
                           break;
                       case 1:
                           multiPart.AddData("serpa", toBase64(data[0]));
                           multiPart.AddData("serpb", "");
                           break;
                       default:
                           multiPart.AddData("serpa", "");
                           multiPart.AddData("serpb", "");
                           break;
                   }
                   //multiPart.AddFile("file", @"");
               }

               Debug.WriteLine("Try send");
               var result=webRequest.GetResponse().ToString();
               webRequest.GetResponse().Close();                
                return result;
            }catch(Exception e)
            {
                Debug.WriteLine("-------------------" + e.Message);
                return  e.Message;
            }
        }
    }
}
