
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eBoxOfficeAdminApp.Models
{
    public class CinemaUser
    {
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public string Username { get; set; }
        public string NormalizedUsername{ get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Address { get; set; }
       
    }
}
