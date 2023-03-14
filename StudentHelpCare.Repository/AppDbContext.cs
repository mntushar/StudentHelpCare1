using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentHelpCare.StudentHelpCare.Data.Entity;

namespace StudentHelpCare.StudentHelpCare.Repository
{
    public class AppDbContext : IdentityDbContext<IdentityUser, IdentityRole, string>
    {
        public DbSet<UserEntity> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<StudentEntity> Student { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUser>().ToTable("User");
            builder.Entity<IdentityRole>().ToTable("Role");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserToken");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("UserRoleClaim");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClim");
        }
    }
}
