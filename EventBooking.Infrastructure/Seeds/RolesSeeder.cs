using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Domain.Enums;
using EventBooking.Domain.IRepositories;

namespace EventBooking.Infrastructure.Seeds
{
    public class RolesSeeder
    {
        public static async Task SeedAsync(IUnitOfWork unitOfWork)
        {
            if (!await unitOfWork.Roles.IsExistAsync())
            {
                await unitOfWork.Roles.RoleManager.CreateAsync(new Role { Name = UserRole.SuperAdmin.ToString() });
                await unitOfWork.Roles.RoleManager.CreateAsync(new Role { Name = UserRole.Admin.ToString() });
                await unitOfWork.Roles.RoleManager.CreateAsync(new Role { Name = UserRole.User.ToString() });
            }
        }
    }
}
