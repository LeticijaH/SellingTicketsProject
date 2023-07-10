using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using eBoxOffice.Service.Interface;
using eBoxOffice.Domain.dto;
using eBoxOffice.Domain.Domain_models;
using ClosedXML.Excel;
using System.IO;
using eBoxOffice.Domain.Enumeration;

namespace homework.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IMovieService _movieService;

        public TicketsController(ITicketService ticketService, IMovieService movieService)
        {
            _ticketService = ticketService;
            _movieService = movieService;
        }

        // GET: Tickets
        public async Task<IActionResult> Index(DateTime? dateFilter)
        {
            ViewData["Category"] = new SelectList(Enum.GetValues(typeof(Category)).Cast<Category>().ToList());

            var tickets = _ticketService.GetAllTickets();
            if (dateFilter != null)
            {
                tickets = tickets.Where(t => t.DateTime.Date == dateFilter.Value.Date).ToList();
            }
            
            return View(tickets);
        }

        [HttpPost]
        public async Task<IActionResult> FilterTickets(DateTime dateFilter)
        {
            //TODO: ako klikni bez datum napraj default site da idat
            if(dateFilter == new DateTime())
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index", "Tickets", new { @dateFilter = dateFilter });
        }


        public async Task<IActionResult> AddToCart(int ticketId)
        {
            var ticket = _ticketService.GetDetailsForTicket(ticketId);

            var model = new AddToShoppingCartDto();
            model.SelectedTicket = ticket;
            model.Quantity = 0;

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToShoppingCart(AddToShoppingCartDto model)
        {
            var loggedInUser = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _ticketService.AddToShoppingCart(model, loggedInUser);

            return RedirectToAction("Index");
        }


        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTicket(id ?? 0);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        public IActionResult Create()
        {
            ViewData["MovieId"] = new SelectList(_movieService.GetAll(), "Id", "MovieName");

            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _ticketService.CreateNewTicket(ticket);
                return RedirectToAction(nameof(Index));
            }
            ViewData["MovieId"] = new SelectList(_movieService.GetAll(), "Id", "MovieName", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTicket(id ?? 0);
            if (ticket == null)
            {
                return NotFound();
            }

            ViewData["MovieId"] = new SelectList(_movieService.GetAll(), "Id", "MovieName", ticket.MovieId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _ticketService.UpdateExistingTicket(ticket);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["MovieId"] = new SelectList(_movieService.GetAll(), "Id", "MovieName", ticket.MovieId);
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = _ticketService.GetDetailsForTicket(id ?? 0);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            _ticketService.DeleteTicket(id);

            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _ticketService.GetDetailsForTicket(id) != null;
        }

        [HttpPost]
        public FileContentResult ExportAllTickets(string category)
        {
            string fileName = category + "Tickets_" + DateTime.Now.ToString() + ".xlsx";
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Tickets");

                worksheet.Cell(1, 1).Value = "Movie";
                worksheet.Cell(1, 2).Value = "Category";
                worksheet.Cell(1, 3).Value = "Date and Time";
                worksheet.Cell(1, 4).Value = "Ticket Price";

                var tickets = _ticketService.GetAllTickets();
                var ticketsByCategory = tickets.Where(t => t.Movie.Category.Equals(category)).ToList();

                for (int i = 0; i < tickets.Count(); i++)
                {
                    var item = tickets[i];

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
