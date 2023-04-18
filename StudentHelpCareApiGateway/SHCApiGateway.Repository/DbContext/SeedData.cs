using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SHCApiGateway.Data.Entity;
using System.Runtime.CompilerServices;

namespace SHCApiGateway.Repository.DbContext
{
    public static class SeedData
    {
        private static readonly IServiceProvider _serviceProvider;
        private static User userseed = new User()
        {
            UserName = "admin",
            Email = "admin@gmail.com",
            //password 123
            PhoneNumber = "123456",
            LockoutEnabled = false,
            AccessFailedCount = 0,
        };
        private static Role roles = new Role()
        {
            Name = "AdminRole",
        };
        private static IdentityUserRole<string>[] userRols =
        {
            new IdentityUserRole<string>()
            {
                UserId = "3d33b832-8ffa-4ca1-9188-32d77fbf2685",
                RoleId = "3d33b832-8ffa-4ca1-9188-32d77fbf2685"
            }
        };
        private static IdentityUserClaim<string>[] userClims =
        {
            new IdentityUserClaim<string>()
            {
                Id = 1,
                UserId = "c62b9487-35d8-4eef-88c2-70cad782bd78",
                ClaimType = "Rred",
                ClaimValue = "true"

            },
            new IdentityUserClaim<string>()
            {
                Id = 1,
                UserId = "c62b9487-35d8-4eef-88c2-70cad782bd78",
                ClaimType = "Write",
                ClaimValue = "true"
            },
            new IdentityUserClaim<string>()
            {
                Id = 1,
                UserId = "c62b9487-35d8-4eef-88c2-70cad782bd78",
                ClaimType = "Edit",
                ClaimValue = "true"

            },
            new IdentityUserClaim<string>()
            {
                Id = 1,
                UserId = "c62b9487-35d8-4eef-88c2-70cad782bd78",
                ClaimType = "Delete",
                ClaimValue = "true"

            }
        };


        public static void Initialize(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider.GetService<SHCApiGatewayDbContext>();
        }

        public static void ConfigSeedData(this ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<User>().HasData(Users);
            //modelBuilder.Entity<User>().HasData(roles);
            //modelBuilder.Entity<User>().HasData(userRols);
            //modelBuilder.Entity<User>().HasData(userClims);
        }
    }
}
