using EventBooking.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Domain.IRepositories.IdentityRepositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        UserManager<User> UserManager { get; }
    }
}
