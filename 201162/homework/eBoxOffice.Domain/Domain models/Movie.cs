using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOffice.Domain.Domain_models
{
    public class Movie : BaseEntity
    {
        [Required]
        [Display(Name = "Movie")]
        public string MovieName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        [Display(Name = "Image")]
        public string MovieImage { get; set; }
        

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
