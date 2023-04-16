using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SHCApiGateway.Data.Entity;

namespace SHCApiGateway.Repository.DbContext
{
    public static class SeedData
    {
        private static User[] Users =
        {
            new User()
            {
                Id = "c62b9487-35d8-4eef-88c2-70cad782bd78",
                UserName = "admin",
                Email = "admin@gmail.com",
                PasswordHash = "AQAAAAIAAYagAAAAEE9LPh5DjDPDNs8ccnYgnvbE30OlLT8Spv0ku5edvP/ejhk4nx83r6UeO5840bMJXA=="
            },
        };
        private static Role[] roles =
        {
            new Role()
            {
                Id = "1",
                Name = "AdminRole",
                NormalizedName = "admin",
            }
        };
        private static IdentityUserRole<string>[] userRols =
        {
            new IdentityUserRole<string>()
            {
                UserId = "c62b9487-35d8-4eef-88c2-70cad782bd78",
                RoleId = "1"
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


        public static void ConfigSeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(roles);
            modelBuilder.Entity<User>().HasData(userRols);
            modelBuilder.Entity<User>().HasData(userClims);
        }
    }
}
