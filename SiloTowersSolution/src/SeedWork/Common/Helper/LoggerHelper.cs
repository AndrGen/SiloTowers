using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILogger = Serilog.ILogger;

namespace Common.Helper
{
    [Obsolete("Только для хелперов")]
    internal static class LoggerHelper
    {
        private static readonly LoggerConfiguration LoggerConfiguration;

        private const string ConfigName = "appsettings.json";

        static LoggerHelper()
        {
            LoggerConfiguration = new LoggerConfiguration();
        }

        internal static ILogger Logger { get; private set; }


        public static void ConfigureLogging()
        {
            var builder = new ConfigurationBuilder()
           .AddJsonFile(ConfigName, optional: true, reloadOnChange: true)
           .AddEnvironmentVariables();


            Logger = LoggerConfiguration
                    .Enrich.WithProperty("Environment", Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"))
                    .Enrich.FromLogContext()
                    .ReadFrom.Configuration(builder.Build())
                    .CreateLogger();
        }
    }
}