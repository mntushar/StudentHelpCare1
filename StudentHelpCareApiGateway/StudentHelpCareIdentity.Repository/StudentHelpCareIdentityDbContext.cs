using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentHelpCareIdentity.Data.Entity;

namespace StudentHelpCare.Identity.Repository
{
    public class StudentHelpCareIdentityDbContext : IdentityDbContext<UserEntity, Role, string>
    {
        public DbSet<UserEntity> User { get; set; }
        public DbSet<Role> Role { get; set; }

        public StudentHelpCareIdentityDbContext(DbContextOptions<StudentHelpCareIdentityDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UserEntity>().ToTable("User");
            builder.Entity<Role>().ToTable("Role");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("UserRoleClaim");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClim");
        }
    }
}
