using Awesomium.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bot2
{
    class ResourceInterceptor : IResourceInterceptor
    {
        public bool NoImages { get; set; }

        private static string[] _imagesFileTypes = { ".png", ".jpg", ".jpeg", ".gif", ".bmp", ".swf" };
        //private static string[] _imagesFileTypes = { ".gif", ".swf" };

        public ResourceResponse OnRequest(ResourceRequest request)
        {
            string ext = System.IO.Path.GetExtension(request.Url.ToString()).ToLower();

            if (NoImages && _imagesFileTypes.Contains(ext))
            {
                request.Cancel();
            }            

            return null;
        }

        public bool OnFilterNavigation(NavigationRequest request)
        {            
            return false;
        }
    }
}
