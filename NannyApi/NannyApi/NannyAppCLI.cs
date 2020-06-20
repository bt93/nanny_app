using NannyApi.DAL;
using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NannyApi
{
    public class NannyAppCLI
    {
        const string Command_GetAllCareTakers = "1";
        const string Command_CountriesInNorthAmerica = "2";
        const string Command_CitiesByCountryCode = "3";
        const string Command_LanguagesByCountryCode = "4";
        const string Command_AddNewLanguage = "5";
        const string Command_RemoveLanguage = "6";
        const string Command_AddCareTaker = "7";
        const string Command_Quit = "q";

        private ICareTakerDAO careTakerDAO;


        public NannyAppCLI(ICareTakerDAO careTakerDAO)
        {
            this.careTakerDAO = careTakerDAO;
        }

        public void RunCLI()
        {
            PrintHeader();
            PrintMenu();

            while (true)
            {
                string command = Console.ReadLine();

                Console.Clear();

                switch (command.ToLower())
                {
                    case Command_GetAllCareTakers:
                        GetAllCareTakers();
                        break;

                    //case Command_CountriesInNorthAmerica:
                    //    GetCountriesInNorthAmerica();
                    //    break;

                    //case Command_CitiesByCountryCode:
                    //    GetCitiesByCountryCode();
                    //    break;

                    //case Command_LanguagesByCountryCode:
                    //    GetLanguagesForCountry();
                    //    break;

                    //case Command_AddNewLanguage:
                    //    AddNewLanguage();
                    //    break;

                    //case Command_RemoveLanguage:
                    //    RemoveLanguage();
                    //    break;

                    case Command_AddCareTaker:
                        AddCareTaker();
                        break;

                    case Command_Quit:
                        Console.WriteLine("Thank you for using the world geography cli app");
                        return;

                    default:
                        Console.WriteLine("The command provided was not a valid command, please try again.");
                        break;
                }

                PrintMenu();
            }
        }



        private void PrintHeader()
        {
            Console.WriteLine("Nanny App Testing CLI");
        }



        private void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Main-Menu Type in a command");
            Console.WriteLine(" 1 - Get all of the caretakers");
            Console.WriteLine(" 2 - Get a list of the countries in North America");
            Console.WriteLine(" 3 - Get a list of the cities by country code");
            Console.WriteLine(" 4 - Get a list of the languages by country code");
            Console.WriteLine(" 5 - Add a new language");
            Console.WriteLine(" 6 - Remove language");
            Console.WriteLine(" 7 - Add a caretaker");
            Console.WriteLine(" Q - Quit");
        }

        private void AddCareTaker()
        {
            
            string firstName = CLIHelper.GetString("First Name");
            string lastName = CLIHelper.GetString("Last Name");
            string email = CLIHelper.GetString("Email");
            string password = CLIHelper.GetString("Password");
            string phoneNumber = CLIHelper.GetString("Phone Number");
            string street = CLIHelper.GetString("Street");
            string city = CLIHelper.GetString("City");
            string state = CLIHelper.GetString("State");
            int zip = CLIHelper.GetInteger("Zip Code");
            string county = CLIHelper.GetString("County");
            string country = CLIHelper.GetString("Country");

            CareTaker careTaker = new CareTaker()
            {
                
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = email,
                Password = password,
                PhoneNumber = phoneNumber,
                Street = street,
                City = city,
                State = state,
                Zip = zip,
                County = county,
                Country = country
            };

            this.careTakerDAO.AddCareTaker(careTaker);

            Console.WriteLine("Caretaker added.");
        }


        private void GetAllCareTakers()
        {
            IList<CareTaker> careTakers = careTakerDAO.GetAllCareTakers();

            Console.WriteLine();
            Console.WriteLine("Printing all of the caretakers");

            for (int index = 0; index < careTakers.Count; index++)
            {
                Console.WriteLine($"{index} - {careTakers[index]} - {careTakers[index].GetAddress()}");
            }
        }



        //private void GetCountriesInNorthAmerica()
        //{
        //    string continent = CLIHelper.GetString("Continent to filter by:");

        //    IList<Country> northAmericanCountries = countryDAO.GetCountries(continent);

        //    Console.WriteLine();
        //    Console.WriteLine("All North American Countries");

        //    foreach (var country in northAmericanCountries)
        //    {
        //        Console.WriteLine(country);
        //    }
        //}



        //private void GetCitiesByCountryCode()
        //{
        //    string countryCode = CLIHelper.GetString("Enter the country code that you want to retrieve:");

        //    IList<City> cities = cityDAO.GetCitiesByCountryCode(countryCode);

        //    Console.WriteLine();
        //    Console.WriteLine($"Printing {cities.Count} cities for {countryCode}");

        //    foreach (var city in cities)
        //    {
        //        Console.WriteLine(city);
        //    }
        //}

        //private void AddNewLanguage()
        //{
        //    string countryCode = CLIHelper.GetString("Enter the country code the language is for:");
        //    bool officialOnly = CLIHelper.GetBool("Is it official only? True/False ");
        //    int percentage = CLIHelper.GetInteger("What percentage is it spoken by?");
        //    string name = CLIHelper.GetString("What is the name of the lanaguage?");

        //    Language lang = new Language
        //    {
        //        CountryCode = countryCode,
        //        IsOfficial = officialOnly,
        //        Percentage = percentage,
        //        Name = name
        //    };

        //    bool result = languageDAO.AddNewLanguage(lang);

        //    if (result)
        //    {
        //        Console.WriteLine("Success!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("The new language was not inserted");
        //    }
        //}

        //private void RemoveLanguage()
        //{
        //    string language = CLIHelper.GetString("Which language should be removed:");
        //    string countryCode = CLIHelper.GetString("For which country code:");

        //    Language lang = new Language
        //    {
        //        CountryCode = countryCode,
        //        Name = language
        //    };

        //    bool result = languageDAO.RemoveLanguage(lang);

        //    if (result)
        //    {
        //        Console.WriteLine("Success!");
        //    }
        //    else
        //    {
        //        Console.WriteLine("The language was not found or removed.");
        //    }
        //}

        //private void GetLanguagesForCountry()
        //{
        //    string countryCode = CLIHelper.GetString("Enter the country code you want to retrieve:");

        //    IList<Language> languages = languageDAO.GetLanguages(countryCode);

        //    Console.WriteLine();
        //    Console.WriteLine($"Printing {languages.Count} languages for {countryCode}");

        //    foreach (var language in languages)
        //    {
        //        Console.WriteLine(language);
        //    }
        //}
    }
}
