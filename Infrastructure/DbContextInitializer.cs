using Domain.Entity;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;

namespace Infrastructure
{
    public sealed class DbContextInitializer
    {
        public async Task SeedDataAsync(ChatDbContext _context)
        {

        }
    }
    public static class InitializerExtensions
    {
        public static async Task InitializeDatabaseAsync(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ChatDbContext>();

            var seeder = new DbContextInitializer();
            await seeder.SeedDataAsync(context);
        }
    }
}