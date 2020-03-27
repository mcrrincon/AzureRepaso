using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Formatting;
using ConsoleAppOrdenarFiltrar.Model;

namespace ConsoleAppOrdenarFiltrar
{
    class Program
    {
        static void Main(string[] args)
        {
            var http = new HttpClient();
            var respuesta = http.GetAsync("http://northwind.demos.network/api/invoices?type=json").Result;

            if (respuesta.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var datos = respuesta.Content.ReadAsAsync<List<Invoices>>().Result;
                //Filtrado de colecciones
                // C# -> Where y funciones de agregación
                // Python -> función filter()
                // JS -> función .filter()

                //Método o funciones de Agregación -> Count, Max, Min, Average y Sum               
                Console.WriteLine("Número de registros: {0}", datos.Count);

                Console.WriteLine("Total Pedido 10248: {0}", datos.Where(r => r.OrderID == 10248).Sum(r => r.Quantity * r.UnitPrice).ToString("N2"));
                
                Console.WriteLine("Número de registros 10248: {0}", datos.Count(r => r.OrderID == 10248));
                Console.WriteLine("Número de registros 10248: {0}", datos.Where(r => r.OrderID == 10248).Count());

                Console.WriteLine("Número de registros ANTON: {0}", datos.Count(r => r.CustomerID == "ANTON"));

                Console.WriteLine("Mayor Precio: {0}", datos.Max(r => r.UnitPrice));

                //Cuantas unidad se han vendido del producto 3
                Console.WriteLine("Numero de unidades vendidas Producto 3: {0}", datos.Where(r => r.ProductID == 3).Sum(r => r.Quantity));

                //Podemos filtrar también utilizando el contiene, comienza o finaliza
                // Contains -> contiene
                // StartsWith -> comienza
                // EndsWith -> finaliza
                var quesos = datos
                    .Where(r => r.ProductName.Contains("Queso"))
                    .Select(r => new { r.ProductID, r.ProductName })
                    .Distinct()
                    .ToList();
                foreach (var p in quesos)
                {
                    Console.WriteLine("{0} {1}", p.ProductID, p.ProductName);
                }

                var clientes = datos.Where(r => r.CustomerName.StartsWith("Ana")).ToList();
                foreach (var p in clientes)
                {
                    Console.WriteLine("{0}", p.CustomerName);
                }

                var clientes1 = datos.Where(r => r.CustomerName.EndsWith("das")).ToList();
                foreach (var p in clientes1)
                {
                    Console.WriteLine("{0}", p.CustomerName);
                }

                //Ordenación de la información
                // Primera regla de ordenación con OrderBy o OrderByDescending
                // Reglas de ordenación consecutivas ThenBy o ThenByDescending
                // Siempre se recomienda ordenar después de filtrar la información
                // C# -> OrderBy, OrderByDescending, ThenBy y ThenByDescending
                // Python -> sort()
                // JS -> .sort()
                var lineas = datos
                    .Where(r => r.Country == "Spain")
                    .OrderBy(r => r.City)
                    .ThenBy(r => r.OrderDate)
                    .ThenBy(r => r.OrderID)
                    .ToList();

                var lineas2 = datos
                    .Where(r => r.Country == "Spain")
                    .OrderByDescending(r => r.City)
                    .ToList();

                //Extracción de información
                // Para extraer información utilizamos Select
                // C# -> Select
                // Python -> map()
                // JS -> .map()
                var lineas3 = datos
                    .Where(r => r.OrderID == 10248)
                    .Select(r => new { r.ProductID, r.ProductName, r.UnitPrice, r.Quantity })
                    .ToList();

                var lineas4 = datos
                    .Where(r => r.OrderID == 10248)
                    .Select(r => new { 
                        r.ProductID,
                        r.ProductName, 
                        r.UnitPrice, 
                        r.Quantity, 
                        subtotal = (r.UnitPrice * r.Quantity) })
                    .ToList();
            }
            else
            {
                Console.WriteLine("Error de HTTP: {0}", respuesta.StatusCode);
            }

            Console.ReadKey();
        }
    }

}
