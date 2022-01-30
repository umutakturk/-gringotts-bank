using System;
using GringottsBank.Infrastructure.Persistence.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace GringottsBank.API
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.FromLogContext()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .CreateLogger();

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                using (var dbContext = scope.ServiceProvider.GetRequiredService<IDatabaseContext>())
                {
                    Log.Information("Migrating the database...");
                    dbContext.Migrate();
                    Log.Information("Migrating the database... Done.");
                }

                host.Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Unhandled exception");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog();
                    webBuilder.UseStartup<Startup>();
                });
    }
}
