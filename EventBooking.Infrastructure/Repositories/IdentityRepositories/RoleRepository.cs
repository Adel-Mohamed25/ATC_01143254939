using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Domain.IRepositories.IdentityRepositories;
using EventBooking.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Infrastructure.Repositories.IdentityRepositories
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(IEventBookingDbContext context, RoleManager<Role> roleManager)
            : base(context)
        {
            RoleManager = roleManager;
        }
        public RoleManager<Role> RoleManager { get; }
    }
}
