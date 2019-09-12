using FileSystem.Entities;
using FileSystem.Services.Implementations;
using FileSystem.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Helpers
{
    class ServiceBuilder
    {
        public static IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                 .AddSingleton<IConfiguration>(Configuration.GetConfiguration())
                 .AddDbContext<Postgres>()
                 .AddTransient<IRepository<User>, UsersRepository>()
                 .AddTransient<IRepository<UserStatus>, UserStatusesRepository>()
                 .AddTransient<IRepository<Credentials>, CredentialsRepository>()
                 .BuildServiceProvider();
        }
    }
}
