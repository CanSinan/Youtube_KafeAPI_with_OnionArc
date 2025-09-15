using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KafeAPI.Persistence.AppContext.Identity
{
    public class AppIdentityDbContext : IdentityDbContext<AppIdentityUser,AppIdentityRole,string>
    {
        public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<AppIdentityUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });
            builder.Entity<AppIdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
           
        }
    }
}
