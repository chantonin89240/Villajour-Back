﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Villajour.Persistence;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<VillajourDbContextInitialiser>();

        await initialiser.InitialiseAsync();
    }
}

public class VillajourDbContextInitialiser
{
    private readonly VillajourDbContext _context;

    public VillajourDbContextInitialiser(VillajourDbContext context)
    {
        this._context = context;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }
}
