using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using NannyApi.DAL;
using NannyApi.Models;

namespace NannyApi
{
    class Program
    {
        static void Main(string[] args)
        {
            // Configures the connection string from the JSON file
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("NannyDB");

            // Creates all the objects needed
            ICareTakerDAO careTakerDAO = new CareTakerSqlDAO(connectionString);

            // Runs test CLI with objects passed
            NannyAppCLI cli = new NannyAppCLI(careTakerDAO);
            cli.RunCLI();
        }
    }
}
