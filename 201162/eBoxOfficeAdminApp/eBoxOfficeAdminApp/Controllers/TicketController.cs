using ClosedXML.Excel;
using eBoxOfficeAdminApp.Models;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eBoxOfficeAdminApp.Controllers
{
    public class TicketController : Controller
    {

        public TicketController()
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
        }

        // GET: Tickets
        public IActionResult Index()
        {
            ViewData["Category"] = new SelectList(Enum.GetValues(typeof(Category)).Cast<Category>().ToList());

            HttpClient client = new HttpClient();
            string URL = "https://localhost:44308/api/admin/GetAllTickets/";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var data = response.Content.ReadAsAsync<List<Ticket>>().Result;

            return View(data);
        }



        [HttpPost]
        public FileContentResult ExportAllTickets(string category)
        {
            HttpClient client = new HttpClient();
            string URL = "https://localhost:44308/api/admin/GetAllTickets/";
            HttpResponseMessage response = client.GetAsync(URL).Result;

            var tickets = response.Content.ReadAsAsync<List<Ticket>>().Result;

            string fileName = category + "Tickets_" + DateTime.Now.ToString() + ".xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Tickets");

                worksheet.Cell(1, 1).Value = "Movie";
                worksheet.Cell(1, 2).Value = "Category";
                worksheet.Cell(1, 3).Value = "Date and Time";
                worksheet.Cell(1, 4).Value = "Ticket Price";

                var ticketsByCategory = tickets.Where(t => t.Movie.Category.Equals(category)).ToList();

                for (int i = 0; i < ticketsByCategory.Count(); i++)
                {
                    var item = ticketsByCategory[i];

                    worksheet.Cell(i + 2, 1).Value = item.Movie.MovieName.ToString();
                    worksheet.Cell(i + 2, 2).Value = item.Movie.Category.ToString();
                    worksheet.Cell(i + 2, 3).Value = item.DateTime;
                    worksheet.Cell(i + 2, 4).Value = item.TicketPrice;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content, contentType, fileName);
                }
            }
        }
    }
}
