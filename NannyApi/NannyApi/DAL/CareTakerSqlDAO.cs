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

        public CareTakerSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public IList<CareTaker> GetCareTakers()
        {
            List<CareTaker> careTakers = new List<CareTaker>();

            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();

                    const string sql = "SELECT * FROM caretaker";
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

        private CareTaker ParseRow(SqlDataReader rdr)
        {
            CareTaker careTaker = new CareTaker();

            careTaker.CareTakerId = Convert.ToInt32(rdr["caretaker_id"]);
            careTaker.AddressId = Convert.ToInt32(rdr["address_id"]);
            careTaker.FirstName = Convert.ToString(rdr["first_name"]);
            careTaker.LastName = Convert.ToString(rdr["last_name"]);
            careTaker.EmailAddress = Convert.ToString(rdr["email_address"]);
            careTaker.Password = Convert.ToString(rdr["password"]);
            careTaker.PhoneNumber = Convert.ToString(rdr["phone_number"]);

            return careTaker;
        }
    }
}
