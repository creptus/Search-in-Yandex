using Awesomium.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace bot2
{
    class Bot : IDisposable
    {
        public Awesomium.Windows.Forms.WebControl browser;

        private int maxCountPageVisit = 2;        

        public Bot(string proxy, bool cookies = false)
        {

            WebPreferences wp;
            if (proxy != null)
            {
                wp = new WebPreferences()
                {
                    ProxyConfig = proxy//"62.122.100.90:8080"
                };

            }
            else
            {
                wp = new WebPreferences();
            }
            WebSession session;
            if (cookies)
            {
                session = WebCore.CreateWebSession(@"C:\SessionDataPath", wp);
            }
            else
            {
                session = WebCore.CreateWebSession(wp);
            }

            this.browser = new Awesomium.Windows.Forms.WebControl();
            this.browser.WebSession = session;

            this.browser.Location = new System.Drawing.Point(12, 12);
            this.browser.Size = new System.Drawing.Size(806, 700);
            this.browser.TabIndex = 0;

            this.browser.LoadingFrameComplete += browser_LoadingFrameComplete;
            this.browser.DocumentReady += browser_DocumentReady;

            this.browser.ConsoleMessage += browser_ConsoleMessage;
            
             ResourceInterceptor ResInt = new ResourceInterceptor();
             WebCore.ResourceInterceptor = ResInt;
        }


        private void browser_ConsoleMessage(object sender, ConsoleMessageEventArgs e)
        {
            Debug.WriteLine(e.Message);
        }

        /*
         <summary></summary>
         */
        protected string curentStep = "start";

        public string getCurentStep() 
        {
            return this.curentStep;
        }

        public List<string> getResult() 
        {
            this.curentStep = "searchResultGetted";
            return this.pageHtml;
        }

        
        private void browser_DocumentReady(object sender, UrlEventArgs e)
        {
            
            /*if (this.browser == null || !this.browser.IsLive)
            {
                Debug.WriteLine("Not Live");
                return; 
            }

            if (!this.browser.IsDocumentReady) 
            {
                Debug.WriteLine("Document not ready");
                return;
            }
            
            

            //MessageBox.Show("Ready "+e.Url.ToString());
            Debug.WriteLine(e.Url.ToString());
            this.browser.Focus();
            switch (curentStep)
            {
                //Смена региона
                case "RegionChangeStart":
                    curentStep = "RegionChangeFillForm";
                    fillForm();
                    break;
                case "RegionChangeFillFormTry":
                    curentStep = "RegionChangeFillFormComplete";
                    //готово
                    break;
                //Смена количества результатов на странице
                case "CountResultOnPageChangeStart":
                    goToChangePageSearchResult();
                    break;
                case "CountResultOnPageChangeLoad":
                    fillFormPageSerchPreference();
                    break;
                case "CountResultOnPageChangeReturnToSearch":
                    curentStep = "CountResultOnPageChangeComplete";
                    //готово
                    break;
                //Собственно сам поиск
                case "searchStart":
                    searchFormFill();
                    break;
                case "searchAnaliz":
                    //Резуультат первой страницы
                    //Возможна капча
                    Analyze();
                    break;
                case "searchAnalizComplete":
                    break;
                case "searchResultGetted":
                    break;
            }*/

        }

        private void browser_LoadingFrameComplete(object sender, FrameEventArgs e)
        {
            if (!e.IsMainFrame) { return; }
            if (this.browser == null || !this.browser.IsLive)
            {
                Debug.WriteLine("Not Live");
                return;
            }

            if (!this.browser.IsDocumentReady)
            {
                Debug.WriteLine("Document not ready");
                return;
            }

            var html = this.browser.ExecuteJavascriptWithResult("document.documentElement.outerHTML").ToString();
            if(AntiCaptcha.isCaptha(html))
            {
                return;//-------------------------------!!!!!!!!!!!!!!!!---------------------------------
                AntiCaptcha ac = new AntiCaptcha();
                string captchaCode = ac.getCode("");
            }

            //MessageBox.Show("Ready "+e.Url.ToString());
            Debug.WriteLine(e.Url.ToString());
            this.browser.Focus();
            switch (curentStep)
            {
                //Смена региона
                case "RegionChangeStart":
                    curentStep = "RegionChangeFillForm";
                    fillForm();
                    break;
                case "RegionChangeFillFormTry":
                    curentStep = "RegionChangeFillFormComplete";
                    //готово
                    break;
                //Смена количества результатов на странице
                case "CountResultOnPageChangeStart":
                    goToChangePageSearchResult();
                    break;
                case "CountResultOnPageChangeLoad":
                    fillFormPageSerchPreference();
                    break;
                case "CountResultOnPageChangeReturnToSearch":
                    curentStep = "CountResultOnPageChangeComplete";
                    //готово
                    break;
                //Собственно сам поиск
                case "searchStart":
                    searchFormFill();
                    break;
                case "searchAnaliz":
                    //Резуультат первой страницы
                    //Возможна капча
                    Analyze();
                    break;
                case "searchAnalizComplete":
                    break;
                case "searchResultGetted":
                    break;
            }
            
        }


        public void goStartPage(string url = "http://yandex.ru")
        {
            browser.Source = new Uri(url);

        }

        protected string[] region = new string[2];
        public void changeRegion(string Name, int region_id)
        {
            this.curentStep = "RegionChangeStart";
            this.region[0] = Name;
            this.region[1] = region_id.ToString();
            goStartPage("http://tune.yandex.ru/region/?retpath=http%3A%2F%2Fwww.yandex.ru%2F%3Fclid%3D1771359-000008006625%26domredir%3D1");


        }

        public string getCurentRegionId() 
        {
           return region[1];          
        }

        /*<summary>Заполняет форму для смены региона</summary>*/
        protected void fillForm()
        {
            //проверяем надо ли менять регион
            var _region = this.browser.ExecuteJavascriptWithResult("$('.b-form-input__input').val();").ToString();
            if(_region==region[0])
            {
                curentStep = "RegionChangeFillFormTry";
                this.browser.ExecuteJavascript("setTimeout( function(){ $('input.b-form-button__input').eq(1).focus().click(); },1000);");
                return;
            }

            //меняем регион
            this.browser.ExecuteJavascript("$('.b-form-input__input').focus(); $('.b-form-input__input').val('');");

            for (var i = 0; i < this.region[0].Length; i++)
            {
                this.browser.ExecuteJavascript("setTimeout(function(){ $('.b-form-input__input').val($('.b-form-input__input').val()+'" + this.region[0][i] + "');},300*" + i + ");");
            }

            string code = "setTimeout(function(){ var f=$('<form action=\"http://tune.yandex.ru/pages/region/do/save.xml\" name=\"region\" method=\"post\"></form>'); $('.b-form input[name=\"region\"]').val(\"" + this.region[0] + "\"); $('.b-form input[name=\"region_id\"]').val(\"" + this.region[1] + "\");$('.b-form input').each(function(){ console.log($(this).attr('name')+'  '+$(this).val()); f.append($(this)); }); $(\"body\").append(f); f.submit();},(" + this.region[0].Length.ToString() + "*300+5000));";
            this.browser.ExecuteJavascript(code);
            curentStep = "RegionChangeFillFormTry";



        }

        /*<summary>Переходит на страницу настройки</summary>*/
        public void setCountResultOnPage()
        {
            curentStep = "CountResultOnPageChangeStart";
            goStartPage("http://tune.yandex.ru/?retpath=http%3A%2F%2Fwww.yandex.ru%2F%3Fclid%3D1771359-000008006625%26domredir%3D1");
        }

        /*<summary>Переходит на страницу настройки результатов поиска</summary>*/
        protected void goToChangePageSearchResult()
        {
            string code = "setTimeout(function(){ var o=document.getElementsByClassName(\"b-link_type_retpath\")[2]; cp('index.serp-customize',o);},4000);";
            this.browser.ExecuteJavascript(code);

            curentStep = "CountResultOnPageChangeLoad";

            //Thread.Sleep(5000);            
            //goStartPage("http://yandex.ru/search/customize?retpath=http%3A%2F%2Fwww.yandex.ru%2F%3Fdomredir%3D1");
            code = "setTimeout(function(){ window.location='http://yandex.ru/search/customize?retpath=http%3A%2F%2Fwww.yandex.ru%2F%3Fdomredir%3D1';},4000);";
            this.browser.ExecuteJavascript(code);
        }

        /*<summary>Заполняет форму на странице настройки результатов поиска</summary>*/
        protected void fillFormPageSerchPreference()
        {
            //50
            string code = "setTimeout(function(){  var els=document.getElementsByClassName(\"b-form-radio__radio\"); for(var i=0;i<els.length;i++){ console.log(els[i].name+\"  \"+els[i].value); if(els[i].name==\"numdoc\" && els[i].value==\"50\"){ els[i].click();}} },1000); ";
            this.browser.ExecuteJavascript(code);
            //не учитывать персональный поиск
            code = "setTimeout(function(){var els=document.getElementsByClassName(\"b-form-checkbox__checkbox\"); for(var i=0;i<els.length;i++){ if(els[i].name==\"person\"){ if (els[i].checked) els[i].click();}} },3000);";
            this.browser.ExecuteJavascript(code);
            //Сохранить и вернуться к поиску
            code = "setTimeout(function(){var o=document.getElementsByClassName(\"b-form-button__input\")[0]; o.click();},10000);";
            this.browser.ExecuteJavascript(code);
            curentStep = "CountResultOnPageChangeReturnToSearch";
        }


        /*<summary>Выполняет произвольный JavaScript код</summary>*/
        public void evulateJavaScript(string code)
        {
            this.browser.Focus();
            this.browser.ExecuteJavascript(code);
        }

        private string keyWord = "";//текущий запрос
        protected List<string> pageHtml = new List<string>();//Результаты поиска
        protected int curentPage = 1;
        /*<summary>Начинает поиск результаов для ключевого слова</summary>*/
        public void startSearch(string word)
        {
            keyWord = word;
            pageHtml.Clear();
            curentPage = 1;
            if (curentStep == "Start" || curentStep == "CountResultOnPageChangeComplete" || curentStep == "RegionChangeFillFormComplete")
            {
                curentStep = "searchStart";
                searchFormFill();
            }
            else
            {
                curentStep = "searchStart";
                goStartPage();
            }
        }

        /*<summary>Заполняет поисковую форму и жмет кнопку "Найти"</summary>*/
        protected void searchFormFill()
        {
            curentStep = "searchAnaliz";
            this.browser.ExecuteJavascript("document.getElementById(\"text\").focus();document.getElementById(\"text\").value='';");
            string code = "";
            for (var i = 0; i < keyWord.Length; i++)
            {
                code += "setTimeout(function(){ document.getElementById(\"text\").value+='" + keyWord[i] + "';},300*" + i + ");";
            }
            this.browser.ExecuteJavascript(code);
            this.browser.ExecuteJavascript("setTimeout(function(){  document.getElementsByClassName(\"b-form-button__input\")[0].click(); },300*" + keyWord.Length + "+2000);");

        }


        /*<summary>Анализирут выдачу и переходит на следующу страницу</summary>*/
        protected void Analyze()
        {
            while (!this.browser.IsDocumentReady) { }            

            WebCore.Update();
            /*----------------Иногода не успевает рендериться--------------------*/
            var html = this.browser.ExecuteJavascriptWithResult("document.documentElement.outerHTML").ToString();
            //var html = this.browser.ExecuteJavascriptWithResult("$('html').html()").ToString();

           pageHtml.Add(html);

            if (curentPage != -1)
            {
                if (curentPage == this.maxCountPageVisit)
                {
                    Debug.WriteLine("searchAnalizComplete");
                    this.curentStep = "searchAnalizComplete";
                }
                else
                {                    
                    //переходим на следующую страницу
                    //this.browser.ExecuteJavascript("var o=document.getElementsByClassName('b-pager__page'); var curentPage="+page.ToString()+"; for(var i=0;i<o.length;i++){ if((curentPage+1)==o[i].innerHTML){ var onmousedown=o[i].getAttribute('onmousedown'); onmousedown = onmousedown.replace(/\\D.\\D/g, ''); w(o[i], onmousedown);  o[i].click();   } }");                    
                    this.browser.ExecuteJavascript("setTimeout(function(){ var o=document.getElementsByClassName('b-pager__page');  var curentPage=" + curentPage.ToString() + ";  for(var i=0;i<o.length;i++){     if((curentPage+1)==o[i].innerHTML){        var onmousedown=o[i].getAttribute('onmousedown');        if (onmousedown){       onmousedown = onmousedown.replace(/[^0-9.]/g, '');       console.log(onmousedown);       w(o[i], onmousedown);        }               window.location=o[i].getAttribute('href');            break;     }} },3000); ");
                    curentPage++;
                    /*dynamic button = (JSObject)this.browser.ExecuteJavascriptWithResult("var findButtonFunc=function(){ var o=document.getElementsByClassName('b-pager__page'); var curentPage=" + page.ToString() + "; for(var i=0;i<o.length;i++){   if((curentPage+1)==o[i].innerHTML){    var onmousedown=o[i].getAttribute('onmousedown');      onmousedown = onmousedown.replace(/\\D.\\D/g, '');       w(o[i], onmousedown);       return o[i];    }}return null;} findButtonFunc();");
                    using (button)
                    {
                        if (button != null)
                        {
                            button.click();
                        }
                        else
                        {
                            this.curentStep = "searchAnalizComplete";
                        }
                    }*/


                }
            }
            else
            {
                //Ничего не найдено
                Debug.WriteLine("Curent page = -1");
                this.curentStep = "searchAnalizComplete";
            }
        }

        public void Dispose()
        {
            this.browser.Dispose();
        }
    }
}
