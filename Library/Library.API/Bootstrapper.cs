using Microsoft.EntityFrameworkCore;
using RMLibrary.Database.Context;

public static class Bootstrapper
{
    public static async Task MigrateAsync(this WebApplication app)
    {
        var provider = app.Services.CreateScope();
        var context = provider.ServiceProvider.GetRequiredService<RMLibraryDbContext>();

        await context.Database.MigrateAsync();
    }
}