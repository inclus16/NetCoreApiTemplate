using InclusCommunication.Entities;
using InclusCommunication.Services.Implementations;
using InclusCommunication.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InclusCommunication.Cli.Helpers
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
                 .AddTransient<SecurityProvider>()
                 .BuildServiceProvider();
        }
    }
}
