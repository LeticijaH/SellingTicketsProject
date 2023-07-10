using eBoxOffice.Domain.identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOffice.Domain.Domain_models
{
    public class Order : BaseEntity
    {
        public string UserId { get; set; }
        public CinemaUser OrderedBy { get; set; }
        public virtual ICollection<TicketsInOrder> TicketsInOrder { get; set; }
    }
}
