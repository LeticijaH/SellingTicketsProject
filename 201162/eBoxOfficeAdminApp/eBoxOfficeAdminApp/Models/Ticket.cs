using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOfficeAdminApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int TicketPrice { get; set; }
        public DateTime DateTime { get; set; }
        public Movie Movie { get; set; }
    }
}
