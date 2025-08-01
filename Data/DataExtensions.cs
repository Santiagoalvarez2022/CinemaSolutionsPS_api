using Microsoft.EntityFrameworkCore;

namespace CinemaSolutionApi.Data;

public static class DataExtensions
{
    public static void MigrateDb(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var dbContex = scope.ServiceProvider.GetRequiredService<CinemaSolutionContext>();
        dbContex.Database.Migrate();
    }
}