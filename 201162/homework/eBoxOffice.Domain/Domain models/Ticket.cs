using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOffice.Domain.Domain_models
{
    public class Ticket : BaseEntity
    {
        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }




        [Required]
        [Display(Name = "Price")]
        public int TicketPrice { get; set; }

        public DateTime DateTime { get; set; }

        public ICollection<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }

    }
}
