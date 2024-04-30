using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Villajour.Persistence;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<VilleajourDbContextInitialiser>();

        await initialiser.InitialiseAsync();
    }
}

public class VilleajourDbContextInitialiser
{
    private readonly VilleajourDbContext _context;

    public VilleajourDbContextInitialiser(VilleajourDbContext context)
    {
        this._context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            //_logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }
}
