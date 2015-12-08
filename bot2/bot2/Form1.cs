using Awesomium.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace bot2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private Bot bot;
        private WebClient client = new WebClient();
        private Tasker tasker = new Tasker();

        private void register()
        {
            //client.DownloadStringCompleted += client_DownloadStringCompleted;
            //client.DownloadStringAsync(new Uri("http://:3333/register"));

            string proxy=tasker.register();
            Debug.WriteLine(proxy + "    " + tasker.getWorkerId());
            createBot(proxy);
        }

        void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Debug.WriteLine("ERROR: " + e.Error.ToString());
                return;
            }

            byte[] bytes = Encoding.Default.GetBytes(e.Result.ToString());
            var data = Encoding.UTF8.GetString(bytes);

           // MessageBox.Show(data);
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


           // MessageBox.Show(id + " " + host + ":" + port);

            string proxy = "";
            if (host != "" && port != "")
            {
                proxy = host + ":" + port;
            }
            createBot(proxy);
        }



        private void createBot(string proxy)
        {
            //Bot bot = new Bot("62.122.100.90:8080",true);

            bot = new Bot(null, true);

            /*if (proxy != "")
            {
                bot = new Bot(proxy, true);
            }
            else
            {
                bot = new Bot(null, true);
            }*/

           

            this.Controls.Add(bot.browser);

            //bot.goStartPage("http://2ip.ru/");

            bot.goStartPage();

            /* Awesomium.Windows.Forms.WebControl b = new Awesomium.Windows.Forms.WebControl();
              b.LoadingFrameComplete += b_LoadingFrameComplete;
              b.DocumentReady += b_DocumentReady;
              b.ConsoleMessage += b_ConsoleMessage;*/
        }

        private void testButton_Click(object sender, EventArgs e)
        {                       
            createBot("");


        }

        void b_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            //            e.Message
        }

        void b_DocumentReady(object sender, UrlEventArgs e)
        {

        }

        void b_LoadingFrameComplete(object sender, FrameEventArgs e)
        {

            e.Url.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            bot.changeRegion("Москва", 213);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bot.setCountResultOnPage();

        }

        private void jsButton_Click(object sender, EventArgs e)
        {
            if (JStextBox.Text != "")
            {
                bot.evulateJavaScript(JStextBox.Text);
            }

        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            bot.startSearch("Кактусы");
        }

       
        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (bot != null && bot.browser != null)
            {
                Debug.WriteLine(bot.getCurentStep());
                string curentStep = bot.getCurentStep();
                Debug.WriteLine(curentStep);
                switch (curentStep)
                {
                    case "searchAnalizComplete":                        
                        var result = bot.getResult();
                        /*using (StreamWriter w = File.AppendText("log.txt"))
                        {
                            foreach (string r in result)
                            {
                                Log(r, w);
                            }


                        }*/
                        timer1.Stop();
                        tasker.sendResult(result);                        
                        tasker.isTaskComplete = true;
                        timer1.Start();
                        break;
                    case "start":
                        bot.setCountResultOnPage();
                        break;
                    case "CountResultOnPageChangeComplete":
                    case "RegionChangeFillFormComplete":
                    case "searchResultGetted":
                        if (tasker.isTaskComplete)
                        {
                            timer1.Stop();
                            if (!tasker.getNewTask())
                            {
                                MessageBox.Show("Bad new task!");
                            }
                            if (bot.getCurentRegionId() != tasker.regionId.ToString())
                            {
                                bot.changeRegion(tasker.region, tasker.regionId);
                            }
                            timer1.Start();
                        }
                        else 
                        {
                            bot.startSearch(tasker.word);
                        }
                                               
                       
                        break;
                }
                
            }
        }

        public static void Log(string logMessage, TextWriter w)
        {
            w.Write("\r\nLog Entry : ");
            w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                DateTime.Now.ToLongDateString());
            w.WriteLine("  :");
            w.WriteLine("  :{0}", logMessage);
            w.WriteLine("-------------------------------");
        }

        private void AutoButton_Click(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Start();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            register();
        }
    }
}
