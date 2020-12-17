using System;
using Common.Helper;
using Microsoft.Extensions.Configuration;

namespace SiloTower.Infrastructure.DB
{
    public class DbConnectionString
    {
        public DbConnectionString()
        {
        }

        public string GetDbConnectionString()
        {
            var config = AppFileConfiguration.GetConfiguration(@"DB\DbConnection.json");
            string conStr = config?.GetConnectionString("DevConnection");
            string pass = config?["Pass"];
            return string.Format(conStr, Base64Decode(pass));
        }

        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
