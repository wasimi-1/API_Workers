using API_Workers.Model;
using Microsoft.EntityFrameworkCore;

namespace API_Workers.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<WorkerEntity> Workers { get; set; }

        public DbSet<LocalUser> LocalUsers { get; set; }
    }
}
