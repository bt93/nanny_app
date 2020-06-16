﻿using System;
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
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("NannyDB");

            ICareTakerDAO careTakerDAO = new CareTakerSqlDAO(connectionString);

            NannyAppCLI cli = new NannyAppCLI(careTakerDAO);
            cli.RunCLI();
        }
    }
}
