using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommandDotNet;
using CommandDotNet.IoC.MicrosoftDependencyInjection;
using InclusCommunication.Cli;
using InclusCommunication.Cli.Helpers;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace InclusCommunication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                HandleWeb(args);
            }
            else
            {
                HandleCli(args);
            }
        }

        public static void HandleCli(string [] args)
        {
            AppRunner<CliInterface> appRunner = new AppRunner<CliInterface>()
                .UseMicrosoftDependencyInjection(ServiceBuilder.BuildServiceProvider());
           appRunner.Run(args);
        }

        public static void HandleWeb(string[] args)
        {
             CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
