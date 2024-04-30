using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Villajour.Application.Commands.Interface;
using Villajour.Domain.Common;

namespace Villajour.Persistence;

public class VilleajourDbContext : DbContext, IVilleajourDbContext
{
    public DbSet<UserEntity> Users => Set<UserEntity>();
    
    public VilleajourDbContext(DbContextOptions<VilleajourDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);

    }
}
