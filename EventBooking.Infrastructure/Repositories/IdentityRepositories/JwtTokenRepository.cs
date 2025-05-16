using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Domain.IRepositories.IdentityRepositories;
using EventBooking.Persistence.Context;

namespace EventBooking.Infrastructure.Repositories.IdentityRepositories
{
    public class JwtTokenRepository : GenericRepository<JwtToken>, IJwtTokenRepository
    {
        public JwtTokenRepository(IEventBookingDbContext context)
            : base(context) { }
    }
}
