using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOfficeAdminApp.Models
{
    public class Movie
    {
        public string MovieName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public string MovieImage { get; set; }
    }
}
