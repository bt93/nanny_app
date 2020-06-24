using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace NannyApi.DAL
{
    public class ParentSqlDAO : IParentDAO
    {
        private string connectionString { get; set; }
        /// <summary>
        /// Creates a sql based parent DAO
        /// </summary>
        /// <param name="dbconnectionString"></param>
        public ParentSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public List<Parent> GetParents()
        {
            List<Parent> parents = new List<Parent>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                const string sql = @"SELECT * FROM parent
                                        JOIN address ON parent.address_id = address.address_id";
                SqlCommand cmd = new SqlCommand(sql, conn);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    parents.Add(ParseRow(rdr));
                }
            }

            return parents;
        }

        private Parent ParseRow(SqlDataReader rdr)
        {
            Parent parent = new Parent();

            // Basic Parent Info
            parent.ParentId = Convert.ToInt32(rdr["parent_id"]);
            parent.AddressId = Convert.ToInt32(rdr["address_id"]);
            parent.FirstName = Convert.ToString(rdr["first_name"]);
            parent.LastName = Convert.ToString(rdr["last_name"]);
            parent.EmailAddress = Convert.ToString(rdr["email_address"]);
            parent.PhoneNumber = Convert.ToString(rdr["phone_number"]);

            // Parent Address Info
            parent.Street = Convert.ToString(rdr["street"]);
            parent.City = Convert.ToString(rdr["city"]);
            parent.State = Convert.ToString(rdr["state"]);
            parent.Zip = Convert.ToInt32(rdr["zip"]);
            parent.County = Convert.ToString(rdr["county"]);
            parent.Country = Convert.ToString(rdr["country"]);

            return parent;
        }
    }
}
