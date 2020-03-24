using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppRefuerzo1.api
{
    /// <summary>
    /// Descripción breve de eco
    /// </summary>
    public class eco : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("ECO: " + context.Request.QueryString["p"]);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}