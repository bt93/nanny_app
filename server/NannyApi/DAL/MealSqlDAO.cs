using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public class MealSqlDAO : IMealDAO
    {
        private string connectionString { get; set; }
        /// <summary>
        /// Creates a sql based Meal DAO
        /// </summary>
        /// <param name="dbconnectionString"></param>
        public MealSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public List<Meal> GetAllMealsBySession(int sessionId, int careTakerId)
        {
            List<Meal> meals = new List<Meal>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM meal
                                        JOIN session ON meal.session_id = session.session_id
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE meal.session_id = @session_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", sessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    meals.Add(ParseRow(rdr));
                }

                return meals;
            }
        }

        public Meal GetAMealBySession(int sessionId, int careTakerId, int mealId)
        {
            Meal meal = new Meal();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM meal
                                        JOIN session ON meal.session_id = session.session_id
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE meal.session_id = @session_id
                                        AND meal.meal_id = @meal_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", sessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);
                cmd.Parameters.AddWithValue("@meal_id", mealId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    meal = ParseRow(rdr);
                }

                return meal;
            }
        }

        public Meal AddMeal(Meal meal)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"INSERT INTO meal (session_id, time, type, notes)
	                                    VALUES (@session_id, @time, @type, @notes)
                                        SELECT @@IDENTITY";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", meal.SessionId);
                cmd.Parameters.AddWithValue("@time", meal.Time);
                cmd.Parameters.AddWithValue("@type", meal.Type);
                cmd.Parameters.AddWithValue("@notes", meal.Notes);

                meal.MealId = Convert.ToInt32(cmd.ExecuteScalar());

                return meal;
            }
        }

        public Meal UpdateMeal(Meal meal, int careTakerId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"UPDATE meal
                                        SET meal.time = @time,
                                        meal.type = @type,
                                        meal.notes = @notes
                                        OUTPUT INSERTED.meal_id
                                        FROM meal
                                        JOIN session ON meal.session_id = session.session_id
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE meal.meal_id = @meal_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@time", meal.Time);
                cmd.Parameters.AddWithValue("@type", meal.Type);
                cmd.Parameters.AddWithValue("@notes", meal.Notes);
                cmd.Parameters.AddWithValue("@meal_id", meal.MealId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                meal.MealId = Convert.ToInt32(cmd.ExecuteScalar());

                return meal;
            }
        }

        public int DeleteMeal(int mealId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"DELETE FROM meal
                                        WHERE meal_id = @meal_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@meal_id", mealId);

                return cmd.ExecuteNonQuery();
            }
        }

        private Meal ParseRow(SqlDataReader rdr)
        {
            Meal meal = new Meal();

            meal.MealId = Convert.ToInt32(rdr["meal_id"]);
            meal.SessionId = Convert.ToInt32(rdr["session_id"]);

            if (rdr["time"] != DBNull.Value)
            {
                meal.Time = Convert.ToDateTime(rdr["time"]);
            }

            if (rdr["type"] != DBNull.Value)
            {
                meal.Type = Convert.ToString(rdr["type"]);
            }

            if (rdr["notes"] != DBNull.Value)
            {
                meal.Notes = Convert.ToString(rdr["notes"]);
            }

            return meal;
        }
    }
}
