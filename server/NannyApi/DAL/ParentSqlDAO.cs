using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        public List<Parent> GetParents(int caretakerId)
        {
            List<Parent> parents = new List<Parent>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                const string sql = @"SELECT * FROM parent
                                        JOIN address ON parent.address_id = address.address_id
                                        JOIN child_parent ON parent.parent_id = child_parent.parent_id
                                        JOIN child ON child_parent.child_id = child.child_id
                                        JOIN child_caretaker ON child.child_id = child_caretaker.child_id
                                        JOIN caretaker ON child_caretaker.caretaker_id = caretaker.caretaker_id
                                        WHERE caretaker.caretaker_id = @caretaker_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@caretaker_id", caretakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    parents.Add(ParseRow(rdr));
                }
            }

            return parents;
        }

        public List<Parent> GetParentsByChild(int childId, int caretakerId)
        {
            List<Parent> parents = new List<Parent>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                const string sql = @"SELECT *
	                                    FROM parent
                                        JOIN address ON parent.address_id = address.address_id
                                        JOIN child_parent ON parent.parent_id = child_parent.parent_id
                                        JOIN child ON child_parent.child_id = child.child_id
                                        JOIN child_caretaker ON child.child_id = child_caretaker.child_id
                                        JOIN caretaker ON child_caretaker.caretaker_id = caretaker.caretaker_id
	                                    WHERE child.child_id = @child_id
                                        AND caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@child_id", childId);
                cmd.Parameters.AddWithValue("@caretaker_id", caretakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    parents.Add(ParseRow(rdr));
                }
            }

            return parents;
        }

        public Parent GetParentById(int parentId, int caretakerId)
        {
            Parent parent = new Parent();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                const string sql = @"SELECT * FROM parent
                                        JOIN address ON parent.address_id = address.address_id
                                        JOIN child_parent ON parent.parent_id = child_parent.parent_id
                                        JOIN child ON child_parent.child_id = child.child_id
                                        JOIN child_caretaker ON child.child_id = child_caretaker.child_id
                                        JOIN caretaker ON child_caretaker.caretaker_id = caretaker.caretaker_id
                                        WHERE parent.parent_id = @parent_id
                                        AND caretaker.caretaker_id = @caretaker_id";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@parent_id", parentId);
                cmd.Parameters.AddWithValue("@caretaker_id", caretakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    parent = ParseRow(rdr);
                }
            }

            return parent;
        }

        public Parent AddParent(Parent parent, int childId)
        {
           using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                // Insert address
                SqlCommand cmd = new SqlCommand("dbo.addParent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@street", parent.Address.Street);
                cmd.Parameters.AddWithValue("@city", parent.Address.City);
                cmd.Parameters.AddWithValue("@state", parent.Address.State);
                cmd.Parameters.AddWithValue("@zip", parent.Address.Zip);
                cmd.Parameters.AddWithValue("@county", parent.Address.County);
                cmd.Parameters.AddWithValue("@country", parent.Address.Country);
                cmd.Parameters.AddWithValue("@first_name", parent.FirstName);
                cmd.Parameters.AddWithValue("@last_name", parent.LastName);
                cmd.Parameters.AddWithValue("@email_address", parent.EmailAddress);
                cmd.Parameters.AddWithValue("@phone_number", parent.PhoneNumber);
                cmd.Parameters.AddWithValue("@child_id", childId);

                cmd.ExecuteNonQuery();

                return parent;
            }
        }

        public bool AddExsistingParent(int childId, int parentId)
        {
            int rowsAffected = 0;
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"INSERT INTO child_parent (child_id, parent_id)
                                                VALUES (@child_id, @parent_id)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@child_id", childId);
                cmd.Parameters.AddWithValue("@parent_id", parentId);

                rowsAffected += cmd.ExecuteNonQuery();

                return (rowsAffected == 1);
            }
        }

        public Parent UpdateParent(Parent parent)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                // Address insert done first 
                SqlCommand cmd = new SqlCommand("dbo.updateParent", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@parent_id", parent.ParentId);
                cmd.Parameters.AddWithValue("@street", parent.Address.Street);
                cmd.Parameters.AddWithValue("@city", parent.Address.City);
                cmd.Parameters.AddWithValue("@state", parent.Address.State);
                cmd.Parameters.AddWithValue("@zip", parent.Address.Zip);
                cmd.Parameters.AddWithValue("@county", parent.Address.County);
                cmd.Parameters.AddWithValue("@country", parent.Address.Country);
                cmd.Parameters.AddWithValue("@first_name", parent.FirstName);
                cmd.Parameters.AddWithValue("@last_name", parent.LastName);
                cmd.Parameters.AddWithValue("@email_address", parent.EmailAddress);
                cmd.Parameters.AddWithValue("@phone_number", parent.PhoneNumber);

                // Finally, executes the caretaker insert
                parent.ParentId = Convert.ToInt32(cmd.ExecuteScalar());

                return parent;
            }
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
