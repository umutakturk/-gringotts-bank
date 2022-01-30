using System;
using System.Reflection;
using GringottsBank.Infrastructure.Identity;
using GringottsBank.Infrastructure.Identity.Abstractions;
using GringottsBank.Infrastructure.Persistence;
using GringottsBank.Infrastructure.Persistence.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace GringottsBank.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IJwtHelper, JwtHelper>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserContext, UserContext>();

            services.AddEntityFrameworkNpgsql().AddDbContext<DatabaseContext>(o =>
            {
                var databaseUri = new Uri(configuration["DATABASE_URL"]);
                var userInfo = databaseUri.UserInfo.Split(':');
                var connectionStringBuilder = new NpgsqlConnectionStringBuilder
                {
                    Host = databaseUri.Host,
                    Port = databaseUri.Port,
                    Username = userInfo[0],
                    Password = userInfo[1],
                    Database = databaseUri.LocalPath.TrimStart('/')
                };

                if (Convert.ToBoolean(configuration["DATABASE_SSL"]))
                {
                    connectionStringBuilder.SslMode = SslMode.Require;
                    connectionStringBuilder.TrustServerCertificate = true;
                }

                o.UseNpgsql(connectionStringBuilder.ToString(), o =>
                 {
                     var assemblyName = typeof(DatabaseContext).GetTypeInfo().Assembly.GetName().Name;
                     o.MigrationsAssembly(assemblyName);
                     o.MinBatchSize(1).MaxBatchSize(100);
                 });
            });
            services.AddScoped<IDatabaseContext>(sp => sp.GetRequiredService<DatabaseContext>());

            return services;
        }
    }
}
