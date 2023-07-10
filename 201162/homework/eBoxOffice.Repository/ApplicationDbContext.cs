
using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Domain.identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace eBoxOffice.Repository
{
    public class ApplicationDbContext : IdentityDbContext<CinemaUser>
    {
        public virtual DbSet<CinemaUser> CinemaUsers { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public virtual DbSet<TicketsInShoppingCart> TicketsInShoppingCart { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<TicketsInOrder> TicketsInOrders{ get; set; }
        public virtual DbSet<EmailMessage> EmailMessages{ get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            //creating a composite key
            builder.Entity<TicketsInShoppingCart>().HasKey(c => new { c.CartId, c.TicketId });
            builder.Entity<TicketsInOrder>().HasKey(c => new { c.OrderId, c.TicketId });
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
