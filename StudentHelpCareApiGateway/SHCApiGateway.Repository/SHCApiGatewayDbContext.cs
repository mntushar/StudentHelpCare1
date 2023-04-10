using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SHCApiGateway.Data.Entity;

namespace SHCApiGateway.Repository
{
    public class SHCApiGatewayDbContext : IdentityDbContext<User, UserRole, string>
    {
        public DbSet<User> User { get; set; }
        public DbSet<UserRole> UserRole { get; set; }

        public SHCApiGatewayDbContext(DbContextOptions<SHCApiGatewayDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("User");
            builder.Entity<UserRole>().ToTable("Role");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("UserRoleClaim");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClim");
        }
    }
}
