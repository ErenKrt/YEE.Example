using Microsoft.EntityFrameworkCore;
using System.Security;
using YEE.Inventory.API.Models.Entities;

namespace YEE.Inventory.API.Database
{
    public class DatabaseContext : DbContext
    {
        protected IConfiguration Configuration { get; }

        public virtual DbSet<Item> Items { get; set; }

        public DatabaseContext(
            DbContextOptions<DatabaseContext> options,
            IConfiguration configuration
        )
            : base(options)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
