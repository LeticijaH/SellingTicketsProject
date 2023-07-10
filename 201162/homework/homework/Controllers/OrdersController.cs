using eBoxOffice.Domain;
using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Repository.Interface;
using eBoxOffice.Service.Interface;
using GemBox.Document;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            ComponentInfo.SetLicense("FREE-LIMITED-KEY");
            _orderService = orderService;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(_orderService.GetAllOrders().ToList());
        }

        public IActionResult GetOrderDetails(int id)
        {

            BaseEntity baseEntity = new BaseEntity
            {
                Id = id
            };
            Order order = _orderService.GetOrderDetails(baseEntity);

            return View(order);
        }

        public FileResult SavePdf(int id)
        {
            BaseEntity baseEntity = new BaseEntity
            {
                Id = id
            };
            Order order = _orderService.GetOrderDetails(baseEntity);

            var directoryPath = Directory.GetCurrentDirectory();
            var templatePath = Path.Combine(directoryPath, "Invoice.docx");
            var document = DocumentModel.Load(templatePath);

            document.Content.Replace("{{OrderNumber}}", order.Id.ToString());
            document.Content.Replace("{{Username}}", order.OrderedBy.Email.ToString());

            StringBuilder sb = new StringBuilder();
            int totalPrice = 0;

            foreach (var item in order.TicketsInOrder)
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
