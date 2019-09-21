using InclusCommunication.Services.Implementations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using InclusCommunication.Services.Interfaces;
using InclusCommunication.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Isopoh.Cryptography.Argon2;
using Moq;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace Tests.Helpers 
{
    public class FakeWebFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {

        public static string TEST_EMAIL = "test@mail.ru";

        public static string TEST_LOGIN = "testLogin";

        public static string TEST_PASSWORD = "123456";

        public static string TEST_NAME = "Test_NAME";


        public void DestroyDatabase()
        {
            using (var scope = Server.Host.Services.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<Postgres>().Database.EnsureDeleted();
            }
        }

        public T GetService<T>()
        {
            var scope = Server.Host.Services.CreateScope();
            return scope.ServiceProvider.GetRequiredService<T>();
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                string postgresConnection=string.Empty;
                using (var sp = services.BuildServiceProvider())
                {
                    using (var scope = sp.CreateScope())
                    {
                         postgresConnection= Regex.Replace(scope.ServiceProvider.GetRequiredService<IConfiguration>().GetConnectionString("Postgres"), @"Database=\w+;","Database="+ Guid.NewGuid().ToString() + ";");
                    }
                }
                services.AddDbContext<Postgres>(options =>
                {
                    options.UseNpgsql(postgresConnection);
                });
                services.AddMemoryCache();
                services.AddScoped<IRepository<User>, UsersRepository>();
                services.AddScoped<IRepository<UserStatus>, UserStatusesRepository>();
                services.AddScoped<IRepository<Credentials>, CredentialsRepository>();
                services.AddScoped<IRepository<UserRole>, UserRolesRepository>();
                services.AddScoped<SecurityProvider>();
                // Build the service provider.
                var sp1 = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database
                // context (ApplicationDbContext).
                using (var scope = sp1.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<Postgres>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<FakeWebFactory<TStartup>>>();
                    db.Database.Migrate();
                    InclusCommunication.Cli.Models.RegistrateAdministrator request = new InclusCommunication.Cli.Models.RegistrateAdministrator
                    {
                        Email = TEST_EMAIL,
                        Password = TEST_PASSWORD,
                        Login = TEST_LOGIN,
                        Name = TEST_NAME
                    };
                    Credentials credentials = Credentials.CreateFromCliModel(request);
                    User user = User.CreateAdministratorFromCli(request);
                    credentials.User = user;
                    db.Credentials.Add(credentials);
                    db.SaveChanges();
                }
            });
        }
    }
}
