using FileSystem.Entities;
using FileSystem.Services.Implementations;
using FileSystem.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace FileSystem.Cli.Helpers
{
    public class ServiceBuilder
    {
        public static IServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                 .AddSingleton<IConfiguration>(Configuration.GetConfiguration())
                 .AddSingleton<Postgres>()
                 .AddMemoryCache()
                 .AddTransient<IRepository<User>, UsersRepository>()
                 .AddTransient<IRepository<UserStatus>, UserStatusesRepository>()
                 .AddTransient<IRepository<Credentials>, CredentialsRepository>()
                 .BuildServiceProvider();
        }
    }
}
