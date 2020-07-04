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

        // TODO: Fix query so it'll insert into child_parent as well
        public Parent AddParent(Parent parent)
        {
           using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();
                const string addressSql = @"INSERT INTO address(street, city, state, zip, county, country)
                                           VALUES(@street, @city, @state, @zip, @county, @country)
                                            SELECT @@Identity";
                // Insert address
                SqlCommand cmd = new SqlCommand(addressSql, conn);
                cmd.Parameters.AddWithValue("@street", parent.Address.Street);
                cmd.Parameters.AddWithValue("@city", parent.Address.City);
                cmd.Parameters.AddWithValue("@state", parent.Address.State);
                cmd.Parameters.AddWithValue("@zip", parent.Address.Zip);
                cmd.Parameters.AddWithValue("@county", parent.Address.County);
                cmd.Parameters.AddWithValue("@country", parent.Address.Country);

                parent.AddressId = Convert.ToInt32(cmd.ExecuteScalar());

                const string careTakerSql = @"INSERT INTO parent (address_id, first_name, last_name, email_address, phone_number)
	                                        VALUES (@@Identity, @first_name, @last_name, @email_address, @phone_number);
                                            SELECT @@Identity";

                // Starts parent insert with address id added from above
                cmd = new SqlCommand(careTakerSql, conn);
                cmd.Parameters.AddWithValue("@first_name", parent.FirstName);
                cmd.Parameters.AddWithValue("@last_name", parent.LastName);
                cmd.Parameters.AddWithValue("@email_address", parent.EmailAddress);
                cmd.Parameters.AddWithValue("@phone_number", parent.PhoneNumber);

                // Finally, executes the parent insert
                parent.ParentId = Convert.ToInt32(cmd.ExecuteScalar());

                return parent;
            }
        }

        // TODO: Fix query so it'll update child_parent as well
        public Parent UpdateParent(Parent parent)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string addressSql = @"UPDATE address
	                                            SET street = @street,
	                                            city = @city,
	                                            state = @state,
	                                            zip = @zip,
	                                            county = @county,
                                                country = @country
                                                OUTPUT INSERTED.address_id
	                                            WHERE address_id = (SELECT address_id FROM parent WHERE parent_id = @parent_id)";

                const string parentSql = @"UPDATE parent
	                                            SET first_name = @first_name,
	                                            last_name = @last_name,
	                                            email_address = @email_address,
                                                phone_number = @phone_number
                                                OUTPUT INSERTED.parent_id
	                                            WHERE parent_id = @parent_id;";

                // Address insert done first 
                SqlCommand cmd = new SqlCommand(addressSql, conn);
                cmd.Parameters.AddWithValue("@parent_id", parent.ParentId);
                cmd.Parameters.AddWithValue("@street", parent.Address.Street);
                cmd.Parameters.AddWithValue("@city", parent.Address.City);
                cmd.Parameters.AddWithValue("@state", parent.Address.State);
                cmd.Parameters.AddWithValue("@zip", parent.Address.Zip);
                cmd.Parameters.AddWithValue("@county", parent.Address.County);
                cmd.Parameters.AddWithValue("@country", parent.Address.Country);
                parent.AddressId = Convert.ToInt32(cmd.ExecuteScalar());

                // Starts caretaker insert with address id added from above
                cmd = new SqlCommand(parentSql, conn);
                cmd.Parameters.AddWithValue("@parent_id", parent.ParentId);
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
