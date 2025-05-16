using EventBooking.Domain.Commons;
using EventBooking.Domain.IRepositories;
using EventBooking.Domain.IRepositories.IdentityRepositories;
using EventBooking.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace EventBooking.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IEventBookingDbContext _context;
        private readonly ILogger<UnitOfWork> _logger;

        public IUserRepository Users { get; private set; }
        public IJwtTokenRepository JwtTokens { get; private set; }
        public IRoleRepository Roles { get; private set; }
        public IEventRepository Events { get; private set; }
        public IBookingRepository Bookings { get; private set; }

        public UnitOfWork(IEventBookingDbContext context,
            ILogger<UnitOfWork> logger,
            IUserRepository users,
            IJwtTokenRepository jwtTokens,
            IRoleRepository roles,
            IEventRepository events,
            IBookingRepository bookings)
        {
            _context = context;
            _logger = logger;
            Users = users;
            JwtTokens = jwtTokens;
            Roles = roles;
            Events = events;
            Bookings = bookings;
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                foreach (var entry in _context.ChangeTracker.Entries<BaseEntity<string>>())
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedDate = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            entry.Entity.ModifiedDate = DateTime.Now;
                            break;
                    }
                }

                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while saving changes in UnitOfWork.");
                throw;
            }
        }

        public async ValueTask DisposeAsync()
            => await _context.DisposeAsync();

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
            => await _context.Database.BeginTransactionAsync(cancellationToken);


        public Task CommitTransactionAsync(CancellationToken cancellationToken = default)
            => _context.Database.CommitTransactionAsync(cancellationToken);


        public Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
            => _context.Database.RollbackTransactionAsync(cancellationToken);
    }
}
