using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

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

        public CareTakerSqlDAO()
        {
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

        public CareTaker GetCareTakerByName(string firstName, string lastName)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                // Queries for a single person by id w/ their address
                const string sql = @"SELECT *
                                        FROM caretaker
                                        JOIN address ON caretaker.address_id = address.address_id
                                        WHERE first_name = @first_name
                                        AND last_name = @last_name";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@first_name", firstName);
                cmd.Parameters.AddWithValue("@last_name", lastName);

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
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                // Two inserts, first is done on Address then selects that id
                const string sql = @"INSERT INTO address (street, city, state, zip, county, country)
	                                        VALUES (@street, @city, @state, @zip, @county, @country)
                                            SELECT @@Identity
                                            INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number)
	                                        VALUES (@@Identity, @first_name, @last_name, @email_address, @password, @phone_number);
                                            SELECT @@Identity";
                // Address insert done first 
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@street", careTaker.Street);
                cmd.Parameters.AddWithValue("@city", careTaker.City);
                cmd.Parameters.AddWithValue("@state", careTaker.State);
                cmd.Parameters.AddWithValue("@zip", careTaker.Zip);
                cmd.Parameters.AddWithValue("@county", careTaker.County);
                cmd.Parameters.AddWithValue("@country", careTaker.Country);

                // Starts caretaker insert with address id added from above
                cmd.Parameters.AddWithValue("@address_id", careTaker.AddressId);
                cmd.Parameters.AddWithValue("@first_name", careTaker.FirstName);
                cmd.Parameters.AddWithValue("@last_name", careTaker.LastName);
                cmd.Parameters.AddWithValue("@email_address", careTaker.EmailAddress);
                cmd.Parameters.AddWithValue("@password", careTaker.Password);
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

                // Two updates, first is done on Address then selects that id
                const string sql = @"UPDATE address
	                                            SET street = @street,
	                                            city = @city,
	                                            state = @state,
	                                            zip = @zip,
	                                            county = @county,
                                                country = @country
	                                            WHERE address_id = (SELECT address_id FROM caretaker WHERE caretaker_id = @caretaker_id)
                                                UPDATE caretaker
	                                            SET first_name = @first_name,
	                                            last_name = @last_name,
	                                            email_address = @email_address,
	                                            password = @password
	                                            WHERE caretaker_id = @caretaker_id;";
                
                // Address insert done first 
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@caretaker_id", careTaker.CareTakerId);
                cmd.Parameters.AddWithValue("@street", careTaker.Street);
                cmd.Parameters.AddWithValue("@city", careTaker.City);
                cmd.Parameters.AddWithValue("@state", careTaker.State);
                cmd.Parameters.AddWithValue("@zip", careTaker.Zip);
                cmd.Parameters.AddWithValue("@county", careTaker.County);
                cmd.Parameters.AddWithValue("@country", careTaker.Country);


                // Starts caretaker insert with address id added from above
                cmd.Parameters.AddWithValue("@address_id", careTaker.AddressId);
                cmd.Parameters.AddWithValue("@first_name", careTaker.FirstName);
                cmd.Parameters.AddWithValue("@last_name", careTaker.LastName);
                cmd.Parameters.AddWithValue("@email_address", careTaker.EmailAddress);
                cmd.Parameters.AddWithValue("@password", careTaker.Password);
                cmd.Parameters.AddWithValue("@phone_number", careTaker.PhoneNumber);

                // Finally, executes the caretaker insert
                cmd.ExecuteNonQuery();

                return careTaker;
            }
        }

        public bool DeleteCareTaker(int caretakerId)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                int rowsAffected = 0;
                const string caretakerSql = @"DELETE FROM caretaker
                                        WHERE caretaker_id = @caretaker_id";
                SqlCommand cmd = new SqlCommand(caretakerSql, conn);
                cmd.Parameters.AddWithValue("@caretaker_id", caretakerId);
                rowsAffected += cmd.ExecuteNonQuery();
                // TODO: Figure out way to delete both caretaker and address in same query

                return (rowsAffected == 1);
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

            // Caretaker Address info
            careTaker.Street = Convert.ToString(rdr["street"]);
            careTaker.City = Convert.ToString(rdr["city"]);
            careTaker.State = Convert.ToString(rdr["state"]);
            careTaker.Zip = Convert.ToInt32(rdr["zip"]);
            careTaker.County = Convert.ToString(rdr["county"]);
            careTaker.Country = Convert.ToString(rdr["country"]);

            return careTaker;
        }

        
    }
}
