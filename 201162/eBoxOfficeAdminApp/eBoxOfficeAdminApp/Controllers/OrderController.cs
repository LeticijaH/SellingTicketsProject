

using ClosedXML.Excel;
using eBoxOfficeAdminApp.Models;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace eBoxOfficeAdminApp.Controllers
{
    public class OrderController : Controller
    {
        public OrderController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        public IActionResult Index()
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44308/api/admin/GetAllActiveOrders/";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Order>>().Result;

            return View(data);
        }

        public IActionResult GetOrderDetails(int id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44308/api/admin/GetOrderDetails/";

            var model = new
            {
                Id = id
            };

            var jsonObject = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(URL, content).Result;
            Order result = response.Content.ReadAsAsync<Order>().Result;

            return View(result);
        }

        public FileResult SavePdf(int id)
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44308/api/admin/GetOrderDetails/";

            var model = new
            {
                Id = id
            };

            var jsonObject = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(jsonObject, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(URL, content).Result;
            Order result = response.Content.ReadAsAsync<Order>().Result;
            
            var directoryPath = Directory.GetCurrentDirectory();
            var templatePath = Path.Combine(directoryPath, "Invoice.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}}", result.Id.ToString());
            document.Content.Replace("{{Username}}", result.OrderedBy.Username.ToString());

            StringBuilder sb = new StringBuilder();
            int totalPrice = 0;

            foreach(var item in result.TicketsInOrder)
            {
                int ticketsPrice = item.Ticket.TicketPrice * item.Quantity;
                
                totalPrice += ticketsPrice;

                string line = item.Ticket.Id + ", quantity: " + item.Quantity + ", price: $" + ticketsPrice;
                sb.AppendLine(line);
                
            }

            document.Content.Replace("{{TicketsInOrder}}", sb.ToString());
            document.Content.Replace("{{TotalPrice}}", "$" + totalPrice.ToString());

            var stream = new MemoryStream();
            document.Save(stream, new PdfSaveOptions());

            return File(stream.ToArray(), new PdfSaveOptions().ContentType, "ExportInvoice.pdf");
        }

        

    }
}
