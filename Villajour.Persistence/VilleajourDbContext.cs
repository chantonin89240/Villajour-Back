using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Villajour.Persistence
{
    public class VilleajourDbContext : DbContext
    {
        public VilleajourDbContext(DbContextOptions<VilleajourDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
