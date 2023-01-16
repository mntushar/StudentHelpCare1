using Microsoft.EntityFrameworkCore;
using StudentHelpCare.Data.Entity;

namespace StudentHelpCare.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        public DbSet<StudentEntity> Student { get; set; }
    }
}
