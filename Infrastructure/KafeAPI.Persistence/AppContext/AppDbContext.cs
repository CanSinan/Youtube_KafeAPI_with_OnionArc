using KafeAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KafeAPI.Persistence.AppContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Table> Tables { get; set; }

        //public DbSet<User> Users { get; set; }
    }
}
