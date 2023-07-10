using eBoxOffice.Domain;
using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Domain.identity;
using eBoxOffice.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace homework.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly UserManager<CinemaUser> _userManager;


        public AdminController(ITicketService ticketService, UserManager<CinemaUser> userManager)
        {
            _ticketService = ticketService;
            _userManager = userManager;
        }

        [HttpGet("[action]")]
        public List<Ticket> GetAllTickets()
        {
            return _ticketService.GetAllTickets();
        }


        [HttpPost("[action]")]
        public bool ImportAllUsers(List<UserRegistrationDto> users)
        {
            bool status = true; 

            foreach(var user in users)
            {
                var userCheck = _userManager.FindByEmailAsync(user.Email).Result;
                if (userCheck == null)
                {
                    var newUser = new CinemaUser
                    {
                        UserName = user.Email,
                        NormalizedEmail = user.Email,
                        Email = user.Email,
                        EmailConfirmed = true,
                        PhoneNumberConfirmed = true,
                        UserShoppingCart = new ShoppingCart()
                    };

                    var result = _userManager.CreateAsync(newUser, user.Password).Result;
                    status = status && result.Succeeded;
                }
                else
                {
                    continue;
                }
            }

            return status;
        }

    }
}
