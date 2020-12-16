using System.IO;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Common.Helper
{
    public static class AppFileConfiguration
    {
        public static IConfigurationRoot GetConfiguration(string configFileName) =>
            new ConfigurationBuilder()
                .AddJsonFile(configFileName)
                //.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .SetBasePath(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location))
                .AddEnvironmentVariables()
                .Build();

    }
}
