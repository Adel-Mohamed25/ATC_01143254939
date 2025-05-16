using EventBooking.Domain.IRepositories.IdentityRepositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EventBooking.Domain.IRepositories
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        public IUserRepository Users { get; }
        public IJwtTokenRepository JwtTokens { get; }
        public IRoleRepository Roles { get; }
        public IEventRepository Events { get; }
        public IBookingRepository Bookings { get; }


        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default);
        Task CommitTransactionAsync(CancellationToken cancellationToken = default);
        Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
    }
}
