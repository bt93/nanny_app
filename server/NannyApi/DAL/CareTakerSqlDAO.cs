using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NannyApi.Security.Models;
using NannyApi.Security;
using System.Data;
using NannyApi.DAL.DBHelpers;

namespace NannyApi.DAL
{
    public class CareTakerSqlDAO : ICareTakerDAO
    {

        private string connectionString { get; set; }

        /// <summary>
        /// Creates a sql based caretaker DAO
        /// </summary>
        /// <param name="dbconnectionString"></param>
        public CareTakerSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public IList<CareTaker> GetAllCareTakers()
        {
            List<CareTaker> careTakers = new List<CareTaker>();

            using (var connection = Connection.CreateConnection(connectionString))
            {
                //GetAllCaretakers
                var command = connection.CreateNewCommand("GetAllCaretakers");
                var rdr = command.ToList();

                while (rdr.Read())
                {
                    careTakers.Add(ParseRow(rdr));
                }
            }
            

            return careTakers;
        }

        public CareTaker GetCareTakerByEmail(string email)
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                var command = connection.CreateNewCommand("GetCaretakerByEmail");
                command.AddWithValue("@EmailAddress", email, SqlDbType.NVarChar);
                
                var rdr = command.Single();

                if (rdr.Read())
                {
                    return ParseRow(rdr);
                }
            }

            return null;
        }

        public CareTaker GetCareTakerById(int id)
        {
            using (var connection = Connection.CreateConnection(connectionString))
            {
                var command = connection.CreateNewCommand("GetCaretakerByID");
                command.AddWithValue("@CaretakerID", id, SqlDbType.Int);

                var rdr = command.Single();

                if (rdr.Read())
                {
                    return ParseRow(rdr);
                }
            }

            return null;
        }

        public CareTaker AddCareTaker(CareTaker careTaker, IPasswordHasher hasher)
        {
            IPasswordHasher passwordHasher = hasher;
            PasswordHash hash = passwordHasher.ComputeHash(careTaker);

            using (var connection = Connection.CreateConnection(this.connectionString))
            {
                var command = connection.CreateNewCommand("AddCareTaker");
                // Stored Procedure created in db/init_scrip.sql
                command.AddWithValue("@street", careTaker.Address.Street, SqlDbType.NVarChar);
                command.AddWithValue("@city", careTaker.Address.City, SqlDbType.NVarChar);
                command.AddWithValue("@state", careTaker.Address.State, SqlDbType.NVarChar);
                command.AddWithValue("@zip", careTaker.Address.Zip, SqlDbType.Int);
                command.AddWithValue("@county", careTaker.Address.County, SqlDbType.NVarChar);
                command.AddWithValue("@country", careTaker.Address.Country, SqlDbType.NVarChar);
                command.AddWithValue("@first_name", careTaker.FirstName, SqlDbType.NVarChar);
                command.AddWithValue("@last_name", careTaker.LastName, SqlDbType.NVarChar);
                command.AddWithValue("@email_address", careTaker.EmailAddress, SqlDbType.NVarChar);
                command.AddWithValue("@password", hash.Password, SqlDbType.NVarChar);
                command.AddWithValue("@salt", hash.Salt, SqlDbType.NVarChar);
                command.AddWithValue("@phone_number", careTaker.PhoneNumber, SqlDbType.NVarChar);
                
                // Finally, executes the caretaker insert
                careTaker.CareTakerId = Convert.ToInt32(command.ModifyAndReturn());

                return careTaker;
            }
        }

        public CareTakerSettings UpdateCareTaker(CareTakerSettings careTaker)
        {
            using (var connection = Connection.CreateConnection(this.connectionString))
            {                
                // Address insert done first 
                var command = connection.CreateNewCommand("UpdateCareTaker");
                command.AddWithValue("@caretaker_id", careTaker.CareTakerId, SqlDbType.Int);
                command.AddWithValue("@street", careTaker.Address.Street, SqlDbType.NVarChar);
                command.AddWithValue("@city", careTaker.Address.City, SqlDbType.NVarChar);
                command.AddWithValue("@state", careTaker.Address.State, SqlDbType.NVarChar);
                command.AddWithValue("@zip", careTaker.Address.Zip, SqlDbType.Int);
                command.AddWithValue("@county", careTaker.Address.County, SqlDbType.NVarChar);
                command.AddWithValue("@country", careTaker.Address.Country, SqlDbType.NVarChar);
                command.AddWithValue("@first_name", careTaker.FirstName, SqlDbType.NVarChar);
                command.AddWithValue("@last_name", careTaker.LastName, SqlDbType.NVarChar);
                command.AddWithValue("@email_address", careTaker.EmailAddress, SqlDbType.NVarChar);
                command.AddWithValue("@phone_number", careTaker.PhoneNumber, SqlDbType.NVarChar);

                // Finally, executes the caretaker insert
                careTaker.CareTakerId = Convert.ToInt32(command.ModifyAndReturn());

                return careTaker;
            }
        }

        public bool UpdatePassword(string password, int careTakerId, IPasswordHasher hasher)
        {
            CareTaker caretaker = new CareTaker();
            caretaker.Password = password;
            IPasswordHasher passwordHasher = hasher;
            PasswordHash hash = passwordHasher.ComputeHash(caretaker);

            using (var connection = new SqlConnection(this.connectionString))
            {
                var command = connection.CreateNewCommand("UpdatePassword");
                command.AddWithValue("@Password", hash.Password, SqlDbType.NVarChar);
                command.AddWithValue("@Salt", hash.Salt, SqlDbType.NVarChar);
                command.AddWithValue("@CaretakerID", careTakerId, SqlDbType.Int);
                
                return command.Modify() == 1;
            }
        }

        public void DeleteCareTaker(CareTaker careTaker)
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var command = connection.CreateNewCommand("DeleteCareTaker");
                command.AddWithValue("@caretaker_id", careTaker.CareTakerId, SqlDbType.Int);
                command.AddWithValue("@address_id", careTaker.AddressId, SqlDbType.Int);
                command.Modify();
            }
        }

        private CareTaker ParseRow(SqlDataReader rdr)
        {
            CareTaker careTaker = new CareTaker();

            // Caretaker main Data
            careTaker.CareTakerId = Convert.ToInt32(rdr["caretaker_id"]);
            careTaker.AddressId = Convert.ToInt32(rdr["address_id"]);
            careTaker.FirstName = Convert.ToString(rdr["first_name"]);
            careTaker.LastName = Convert.ToString(rdr["last_name"]);
            careTaker.EmailAddress = Convert.ToString(rdr["email_address"]);
            careTaker.Password = Convert.ToString(rdr["password"]);
            careTaker.PhoneNumber = Convert.ToString(rdr["phone_number"]);
            careTaker.Salt = Convert.ToString(rdr["salt"]);

            // Caretaker Address info
            careTaker.Address.Street = Convert.ToString(rdr["street"]);
            careTaker.Address.City = Convert.ToString(rdr["city"]);
            careTaker.Address.State = Convert.ToString(rdr["state"]);
            careTaker.Address.Zip = Convert.ToInt32(rdr["zip"]);
            careTaker.Address.County = Convert.ToString(rdr["county"]);
            careTaker.Address.Country = Convert.ToString(rdr["country"]);

            return careTaker;
        }

        
    }
}
