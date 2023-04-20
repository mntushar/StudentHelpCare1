using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SHCApiGateway.Data.Entity;

namespace SHCApiGateway.Repository.DbContext
{
    public static class SeedData
    {
        private static User userSeed = new User()
        {
            Id = "72f14134-1611-499f-a58f-a5c4299b8b15",
            UserName = "Admin",
            Email = "admin@gmail.com",
            NormalizedUserName = "Admin@gmail.com".ToUpper(),
            NormalizedEmail = "Admin".ToUpper(),
            //passwotd 123
            PasswordHash = "AQAAAAIAAYagAAAAEFbMg/4ElLBGlexTVfyy/DlBIOFib3JoT+ZQIzn/LHwgsZAkGJwqP+6c6uXu5jr1ZQ==",
            EmailConfirmed = false,
            LockoutEnabled = false,
            PhoneNumberConfirmed = false,
            SecurityStamp = "TCWKAWXAIBHYIIH2RDDGUH3HWJSATRLC"
        };

        private static Role role = new Role()
        {
            Id = "4b65e0d2-5c89-41e1-8505-fe956483e735",
            Name = "AdminRole",
            NormalizedName = "admin",
            ConcurrencyStamp = "233e96aa-a060-43d5-b306-5a6309f88395"
        };

        private static IdentityUserRole<string> userRols = new IdentityUserRole<string>()
        {
            UserId = "72f14134-1611-499f-a58f-a5c4299b8b15",
            RoleId = "4b65e0d2-5c89-41e1-8505-fe956483e735"
        };


        private static IdentityUserClaim<string>[] userClims =
        {
            new IdentityUserClaim<string>()
            {
                Id = 1,
                UserId = "72f14134-1611-499f-a58f-a5c4299b8b15",
                ClaimType = "Rred",
                ClaimValue = "true"

            },
            new IdentityUserClaim<string>()
            {
                Id = 2,
                UserId = "72f14134-1611-499f-a58f-a5c4299b8b15",
                ClaimType = "Write",
                ClaimValue = "true"
            },
            new IdentityUserClaim<string>()
            {
                Id = 3,
                UserId = "72f14134-1611-499f-a58f-a5c4299b8b15",
                ClaimType = "Edit",
                ClaimValue = "true"

            },
            new IdentityUserClaim<string>()
            {
                Id = 4,
                UserId = "72f14134-1611-499f-a58f-a5c4299b8b15",
                ClaimType = "Delete",
                ClaimValue = "true"

            }
        };


        public static void ConfigSeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(userSeed);
            modelBuilder.Entity<Role>().HasData(role);
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(userRols);
            modelBuilder.Entity<IdentityUserClaim<string>>().HasData(userClims);

        }
    }
}
