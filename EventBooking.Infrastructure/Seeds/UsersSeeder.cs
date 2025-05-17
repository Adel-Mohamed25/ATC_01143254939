using EventBooking.Domain.Entities.IdentityEntities;
using EventBooking.Domain.Enums;
using EventBooking.Domain.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace EventBooking.Infrastructure.Seeds
{
    public static class UsersSeeder
    {
        public static async Task SeedAdminUserAsync(IUnitOfWork unitOfWork)
        {
            var defaultAdminUser = new User
            {
                FirstName = "Adel",
                LastName = "Mohamed",
                UserName = "adel2852003adel@gmail.com",
                Email = "adel2852003adel@gmail.com",
                Address = "Menoufia",
                Gender = GenderType.Male,
                PhoneNumber = "01143254939",
                EmailConfirmed = true,
            };

            if (!await unitOfWork.Users.IsExistAsync(bu => bu.Email == defaultAdminUser.Email))
            {
                await unitOfWork.Users.UserManager.CreateAsync(defaultAdminUser, "1Q2w3e4@");
                await unitOfWork.Users.UserManager.AddToRoleAsync(defaultAdminUser, UserRole.User.ToString());
                await unitOfWork.Users.UserManager.AddToRoleAsync(defaultAdminUser, UserRole.Admin.ToString());
            }
        }

        public static async Task SeedSuperAdminUserAsync(IUnitOfWork unitOfWork)
        {
            var defaultSuperAdminUser = new User
            {
                FirstName = "Giving Hands",
                LastName = "Charity",
                UserName = "givinghands.contact@gmail.com",
                Email = "givinghands.contact@gmail.com",
                Address = "Menoufia",
                Gender = GenderType.Male,
                PhoneNumber = "01101426595",
                EmailConfirmed = true,
            };

            if (!await unitOfWork.Users.IsExistAsync(bu => bu.Email == defaultSuperAdminUser.Email))
            {
                await unitOfWork.Users.UserManager.CreateAsync(defaultSuperAdminUser, "1Q2w3e4@");
                await unitOfWork.Users.UserManager.AddToRoleAsync(defaultSuperAdminUser, UserRole.User.ToString());
                await unitOfWork.Users.UserManager.AddToRoleAsync(defaultSuperAdminUser, UserRole.Admin.ToString());
                await unitOfWork.Users.UserManager.AddToRoleAsync(defaultSuperAdminUser, UserRole.SuperAdmin.ToString());
            }
            await unitOfWork.Roles.RoleManager.SeedClaimsForSuperAdminAsync();
        }

        private static async Task SeedClaimsForSuperAdminAsync(this RoleManager<Role> roleManager)
        {
            var superAdminRole = await roleManager.FindByNameAsync(UserRole.SuperAdmin.ToString());
            await roleManager.AddPermissionClaimsAsync(superAdminRole!, "Beneficiary");
        }

        public static async Task AddPermissionClaimsAsync(this RoleManager<Role> roleManager, Role role, string module)
        {
            var allClaims = await roleManager.GetClaimsAsync(role);
            var allPermissions = Permissions.GeneratePermissionsForModule(module);
            foreach (var permission in allPermissions)
            {
                if (!allClaims.Any(a => a.Type == "Permission" && a.Value == permission))
                {
                    await roleManager.AddClaimAsync(role, new Claim("Permission", permission));
                }
            }
        }
    }
}
