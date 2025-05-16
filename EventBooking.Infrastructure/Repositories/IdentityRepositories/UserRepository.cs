using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Domain.IRepositories.IdentityRepositories;
using EventBooking.Persistence.Context;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Infrastructure.Repositories.IdentityRepositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(IEventBookingDbContext context, UserManager<User> userManager)
            : base(context)
        {
            UserManager = userManager;
        }
        public UserManager<User> UserManager { get; }
    }
}
