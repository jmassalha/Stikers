using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Stikers.Models
{
    public class CustomHeaderModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.PreSendRequestHeaders += OnPreSendRequestHeaders;
        }

        public void Dispose() { }

        void OnPreSendRequestHeaders(object sender, EventArgs e)
        {
            // removes "Server" details from response header
            if (HttpContext.Current != null)
            {
                HttpContext.Current.Response.Headers.Remove("Server");
                HttpContext.Current.Response.Headers.Remove("X-AspNetWebPages-Version");
                HttpContext.Current.Response.Headers.Remove("X-AspNet-Version");
                HttpContext.Current.Response.Headers.Remove("X-Powered-By");
                HttpContext.Current.Response.Headers.Remove("X-AspNetMvc-Version");
                HttpContext.Current.Response.Headers.Remove("Access-Control-Allow-Origin");
                HttpContext.Current.Response.Headers.Remove("Access-Control-Allow-Credentials");
                HttpContext.Current.Response.Headers.Remove("Access-Control-Allow-Methods");
                HttpContext.Current.Response.Headers.Remove("Access-Control-Allow-Headers");
                //http://srv-apps-prod:564
                //http://localhost:4200
#if DEBUG
                HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:4200");
#else
                HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Origin", "http://srv-apps-prod:564");
#endif
                //HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Headers", "*");
                HttpContext.Current.Response.Headers.Add("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT");
            }

        }
    }
}