using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NannyApi.DAL;

namespace NannyApi
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("NannyDB");

            ICareTakerDAO careTakerDAO = new CareTakerSqlDAO(connectionString);
        }
    }
}
