using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public class ChildSqlDAO : IChildDAO
    {
        private string connectionString { get; set; }
        /// <summary>
        /// Creates a sql based caretaker DAO
        /// </summary>
        /// <param name="dbconnectionString"></param>
        public ChildSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public List<Child> GetChildren(int careTakerId)
        {
            List<Child> children = new List<Child>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT child.*
	                                    FROM child
	                                    JOIN child_caretaker ON child.child_id = child_caretaker.child_id
                                        JOIN caretaker ON child_caretaker.caretaker_id = caretaker.caretaker_id
	                                    WHERE caretaker.caretaker_id = @caretaker_id
	                                    GROUP BY child.child_id, child.first_name, child.last_name, 
	                                    child.gender, child.date_of_birth, child.needs_diapers,
	                                    child.rate_per_hour, child.image_url;";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    children.Add(ParseRow(rdr));
                }
            }

            return children;
        }

        public Child GetChildById(int childId, int careTakerId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT child.*
	                                    FROM child
	                                    JOIN child_caretaker ON child.child_id = child_caretaker.child_id
                                        JOIN caretaker ON child_caretaker.caretaker_id = caretaker.caretaker_id
	                                    WHERE caretaker.caretaker_id = @caretaker_id
                                        AND child.child_id = @child_id
	                                    GROUP BY child.child_id, child.first_name, child.last_name, 
	                                    child.gender, child.date_of_birth, child.needs_diapers,
	                                    child.rate_per_hour, child.image_url;";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);
                cmd.Parameters.AddWithValue("@child_id", childId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    return ParseRow(rdr);
                }
            }

            return null;
        }

        public Child AddChild(Child child, int cartakerId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"INSERT INTO child (first_name, last_name, gender, date_of_birth, rate_per_hour, needs_diapers, image_url)
                                        VALUES (@first_name, @last_name, @gender, @date_of_birth, @rate_per_hour, @needs_diapers, @image_url)
                                        SELECT @@Identity
                                        INSERT INTO child_caretaker (child_id, caretaker_id)
                                        VALUES (@@Identity, @caretaker_id)";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@first_name", child.FirstName);
                cmd.Parameters.AddWithValue("@last_name", child.LastName);
                cmd.Parameters.AddWithValue("@gender", child.Gender);
                cmd.Parameters.AddWithValue("@date_of_birth", child.DateOfBirth);
                cmd.Parameters.AddWithValue("@rate_per_hour", child.RatePerHour);
                cmd.Parameters.AddWithValue("@needs_diapers", child.NeedsDiapers);
                cmd.Parameters.AddWithValue("@image_url", child.ImageUrl);
                cmd.Parameters.AddWithValue("@caretaker_id", cartakerId);

                child.ChildId = Convert.ToInt32(cmd.ExecuteScalar());
            }

            return child;
        }

        private Child ParseRow(SqlDataReader rdr)
        {
            Child child = new Child();

            child.ChildId = Convert.ToInt32(rdr["child_id"]);
            child.FirstName = Convert.ToString(rdr["first_name"]);
            child.LastName = Convert.ToString(rdr["last_name"]);
            child.Gender = Convert.ToChar(rdr["gender"]);
            child.DateOfBirth = Convert.ToDateTime(rdr["date_of_birth"]);
            child.RatePerHour = Convert.ToDecimal(rdr["rate_per_hour"]);
            child.NeedsDiapers = Convert.ToBoolean(rdr["needs_diapers"]);
            child.ImageUrl = Convert.ToString(rdr["image_url"]);

            return child;
        }
    }
}
