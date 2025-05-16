using EventBooking.Domain.Entities;
using EventBooking.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EventBooking.Persistence.Context
{
    public class EventBookingDbContext : IdentityDbContext<User, Role, string>, IEventBookingDbContext
    {
        public EventBookingDbContext(DbContextOptions<EventBookingDbContext> options)
            : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<JwtToken> JwtTokens { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
