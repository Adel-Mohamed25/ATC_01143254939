using EventBooking.Domain.Entities.IdentityEntities;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Domain.IRepositories.IdentityRepositories
{
    public interface IRoleRepository : IGenericRepository<Role>
    {
        RoleManager<Role> RoleManager { get; }
    }
}
