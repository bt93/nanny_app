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

        public List<Parent> GetParentsByChild(int childId)
        {
            List<Parent> parents = new List<Parent>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                const string sql = @"SELECT *
	                                    FROM parent
                                        JOIN address ON parent.address_id = address.address_id
	                                    JOIN child_parent ON parent.parent_id = child_parent.parent_id
	                                    WHERE child_id = @child_id;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@child_id", childId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    parents.Add(ParseRow(rdr));
                }
            }

            return parents;
        }

        public Parent GetParentById(int id)
        {
            Parent parent = new Parent();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                const string sql = @"SELECT * FROM parent
                                        JOIN address ON parent.address_id = address.address_id
                                        WHERE parent_id = @parent_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@parent_id", id);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    parent = ParseRow(rdr);
                }
            }

            return parent;
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
            parent.Address.Street = Convert.ToString(rdr["street"]);
            parent.Address.City = Convert.ToString(rdr["city"]);
            parent.Address.State = Convert.ToString(rdr["state"]);
            parent.Address.Zip = Convert.ToInt32(rdr["zip"]);
            parent.Address.County = Convert.ToString(rdr["county"]);
            parent.Address.Country = Convert.ToString(rdr["country"]);

            return parent;
        }
    }
}
