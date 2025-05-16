using EventBooking.Domain.Entities.IdentityEntities;

namespace EventBooking.Domain.IRepositories.IdentityRepositories
{
    public interface IJwtTokenRepository : IGenericRepository<JwtToken>
    {
    }
}
