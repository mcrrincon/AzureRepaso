using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleAppRefuerzo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Instanciar el cliente HttpClient del espacio de nombres System.Net.Http
            var http = new HttpClient();
            
            //Iniciar la comunicación GET, POST, PUT, DELETE utilizando el método correspondiente
            var respuesta = http.GetAsync("http://northwind.demos.network/api/customers/ANATR?type=json").Result;
            
            //Analizacmos el código de respuesta utilizando la propiedad StatusCode
            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                //Lectura y Deserialización
                var client2 = respuesta.Content.ReadAsAsync<dynamic>().Result;
                Console.WriteLine("Empresa 2: {0}", client2.CompanyName);
                Console.WriteLine("País 2: {0}", client2.Country);

                //Leemos el contenido utilizando los métodos Read....
                var datos = respuesta.Content.ReadAsStringAsync().Result;
                //Console.WriteLine("Contenido: " + datos);
                Console.WriteLine("Contenido: {0}", datos);

                //Desealizamos para convertir de JSON o XML a objeto
                var client = JsonConvert.DeserializeObject<dynamic>(datos);
                Console.WriteLine("Empresa: {0}", client.CompanyName);
                Console.WriteLine("País: {0}", client.Country);
            }
            else 
            {
                Console.WriteLine("Error de HTTP: {0}", respuesta.StatusCode);
            }

            Console.ReadKey();
        }
    }
}
