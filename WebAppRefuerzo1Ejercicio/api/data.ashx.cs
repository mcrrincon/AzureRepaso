using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace WebAppRefuerzo1Ejercicio.api
{
    /// <summary>
    /// Descripción breve de data
    /// </summary>
    public class data : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                string id = context.Request.Params["id"];
                string key = (context.Request.Headers.AllKeys.Contains("API-Key") ? context.Request.Headers.Get("API-Key") : "");

                if (id.ToLower() == "all" && key.ToLower() == "ce4def9a-beb1-4225-82fb-897109f5d778")
                {
                    string json = File.ReadAllText(context.Request.PhysicalApplicationPath + "api\\data.json");
                    context.Response.ContentType = "text/plain";
                    context.Response.Write(json);
                }
                else
                {
                    context.Response.ContentType = "text/plain";
                    context.Response.Write("No Autorizado o Petición Erronea.");
                }
            }
            catch (Exception e)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Error: " + e.Message);
            }
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