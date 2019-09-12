using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Helpers
{
    class Configuration
    {
        public static IConfiguration GetConfiguration()
        {
            return new ConfigurationBuilder()
                          .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                         .Build();
        }

    }
}
