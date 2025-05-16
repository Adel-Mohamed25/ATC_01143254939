using EventBooking.Domain.Entities;
using EventBooking.Domain.Entities.IdentityEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace EventBooking.Persistence.Context
{
    public interface IEventBookingDbContext
    {
        DbSet<User> Users { get; }
        DbSet<Role> Roles { get; }
        DbSet<JwtToken> JwtTokens { get; }
        DbSet<Event> Events { get; }
        DbSet<Booking> Bookings { get; }

        DatabaseFacade Database { get; }
        ChangeTracker ChangeTracker { get; }
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        ValueTask DisposeAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
