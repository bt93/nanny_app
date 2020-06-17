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

        public IList<CareTaker> GetAllCareTakers()
        {
            List<CareTaker> careTakers = new List<CareTaker>();

            try
            {
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
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return careTakers;
        }

        public CareTaker GetCareTakerByName(string firstName, string lastName)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();

                    // Queries for a single person by first and last name w/ their address
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
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        public void AddCareTaker(CareTaker careTaker)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();

                    // Two inserts, first is done on Address then selects that id
                    const string addressSql = @"INSERT INTO address (street_number, street_name, city, state, zip, county, country)
	                                            VALUES (@street_number, @street_name, @city, @state, @zip, @county, @country)
                                                SELECT @@Identity";
                    // Second insert adds to the caretaker table
                    const string personSql = @"INSERT INTO caretaker (address_id, first_name, last_name, email_address, password, phone_number)
	                                            VALUES (@address_id, @first_name, @last_name, @email_address, @password, @phone_number);";
                    // Address insert done first 
                    SqlCommand cmd = new SqlCommand(addressSql, conn);
                    cmd.Parameters.AddWithValue("@street_number", careTaker.StreetNumber);
                    cmd.Parameters.AddWithValue("@street_name", careTaker.StreetName);
                    cmd.Parameters.AddWithValue("@city", careTaker.City);
                    cmd.Parameters.AddWithValue("@state", careTaker.State);
                    cmd.Parameters.AddWithValue("@zip", careTaker.Zip);
                    cmd.Parameters.AddWithValue("@county", careTaker.County);
                    cmd.Parameters.AddWithValue("@country", careTaker.Country);

                    // Takes the @@identity converts it to an int
                    int addressId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Starts caretaker insert with address id added from above
                    cmd = new SqlCommand(personSql, conn);
                    cmd.Parameters.AddWithValue("@address_id", addressId);
                    cmd.Parameters.AddWithValue("@first_name", careTaker.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", careTaker.LastName);
                    cmd.Parameters.AddWithValue("@email_address", careTaker.EmailAddress);
                    cmd.Parameters.AddWithValue("@password", careTaker.Password);
                    cmd.Parameters.AddWithValue("@phone_number", careTaker.PhoneNumber);

                    // Finally, executes the caretaker insert
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException)
            {

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
            careTaker.StreetNumber = Convert.ToInt32(rdr["street_number"]);
            careTaker.StreetName = Convert.ToString(rdr["street_name"]);
            careTaker.City = Convert.ToString(rdr["city"]);
            careTaker.State = Convert.ToString(rdr["state"]);
            careTaker.Zip = Convert.ToInt32(rdr["zip"]);
            careTaker.County = Convert.ToString(rdr["county"]);
            careTaker.Country = Convert.ToString(rdr["country"]);

            return careTaker;
        }

        
    }
}
