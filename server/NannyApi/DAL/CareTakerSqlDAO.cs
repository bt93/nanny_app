using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using NannyApi.Security.Models;
using NannyApi.Security;
using System.Data;

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

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                // Returns all the caretakers with address
                const string sql = @"SELECT * FROM caretaker
                                       JOIN address ON caretaker.address_id = address.address_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    careTakers.Add(ParseRow(rdr));
                }
            }
            

            return careTakers;
        }

        public CareTaker GetCareTakerByEmail(string email)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                // Queries for a single person by id w/ their address
                const string sql = @"SELECT *
                                        FROM caretaker
                                        JOIN address ON caretaker.address_id = address.address_id
                                        WHERE email_address = @email_address";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@email_address", email);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    return ParseRow(rdr);
                }
            }

            return null;
        }

        public CareTaker GetCareTakerById(int id)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                // Queries for a single person by first and last name w/ their address
                const string sql = @"SELECT *
                                        FROM caretaker
                                        JOIN address ON caretaker.address_id = address.address_id
                                        WHERE caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@caretaker_id", id);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    return ParseRow(rdr);
                }
            }
            
            return null;
        }

        public CareTaker AddCareTaker(CareTaker careTaker)
        {
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(careTaker);

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                // Stored Procedure created in db/init_scrip.sql
                SqlCommand cmd = new SqlCommand("dbo.addCareTaker", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@street", careTaker.Address.Street);
                cmd.Parameters.AddWithValue("@city", careTaker.Address.City);
                cmd.Parameters.AddWithValue("@state", careTaker.Address.State);
                cmd.Parameters.AddWithValue("@zip", careTaker.Address.Zip);
                cmd.Parameters.AddWithValue("@county", careTaker.Address.County);
                cmd.Parameters.AddWithValue("@country", careTaker.Address.Country);
                cmd.Parameters.AddWithValue("@first_name", careTaker.FirstName);
                cmd.Parameters.AddWithValue("@last_name", careTaker.LastName);
                cmd.Parameters.AddWithValue("@email_address", careTaker.EmailAddress);
                cmd.Parameters.AddWithValue("@password", hash.Password);
                cmd.Parameters.AddWithValue("@salt", hash.Salt);
                cmd.Parameters.AddWithValue("@phone_number", careTaker.PhoneNumber);
                
                // Finally, executes the caretaker insert
                careTaker.CareTakerId = Convert.ToInt32(cmd.ExecuteScalar());

                return careTaker;
            }
        }

        public CareTaker UpdateCareTaker(CareTaker careTaker)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                
                // Address insert done first 
                SqlCommand cmd = new SqlCommand("dbo.updateCareTaker", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@caretaker_id", careTaker.CareTakerId);
                cmd.Parameters.AddWithValue("@street", careTaker.Address.Street);
                cmd.Parameters.AddWithValue("@city", careTaker.Address.City);
                cmd.Parameters.AddWithValue("@state", careTaker.Address.State);
                cmd.Parameters.AddWithValue("@zip", careTaker.Address.Zip);
                cmd.Parameters.AddWithValue("@county", careTaker.Address.County);
                cmd.Parameters.AddWithValue("@country", careTaker.Address.Country);
                cmd.Parameters.AddWithValue("@first_name", careTaker.FirstName);
                cmd.Parameters.AddWithValue("@last_name", careTaker.LastName);
                cmd.Parameters.AddWithValue("@email_address", careTaker.EmailAddress);
                cmd.Parameters.AddWithValue("@phone_number", careTaker.PhoneNumber);

                // Finally, executes the caretaker insert
                careTaker.CareTakerId = Convert.ToInt32(cmd.ExecuteScalar());

                return careTaker;
            }
        }

        public bool UpdatePassword(string password, int careTakerId)
        {
            CareTaker caretaker = new CareTaker();
            caretaker.Password = password;
            IPasswordHasher passwordHasher = new PasswordHasher();
            PasswordHash hash = passwordHasher.ComputeHash(caretaker);

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"UPDATE caretaker
                                        SET password = @password,
                                        SALT = @salt
                                        WHERE caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@password", hash.Password);
                cmd.Parameters.AddWithValue("@salt", hash.Salt);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);
                
                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public void DeleteCareTaker(CareTaker careTaker)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.deleteCareTaker", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@caretaker_id", careTaker.CareTakerId);
                cmd.Parameters.AddWithValue("@address_id", careTaker.AddressId);
                cmd.ExecuteNonQuery();
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
