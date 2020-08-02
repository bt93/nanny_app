using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public class DiaperSqlDAO : IDiaperDAO
    {
        private string connectionString { get; set; }
        /// <summary>
        /// Creates a sql based Nap DAO
        /// </summary>
        /// <param name="dbconnectionString"></param>
        public DiaperSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public List<Diaper> GetAllDiapersBySession(int sessionId, int careTakerId)
        {
            List<Diaper> diapers = new List<Diaper>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM diaper
                                        JOIN session ON diaper.session_id = session.session_id
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE diaper.session_id = @session_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", sessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    diapers.Add(ParseRow(rdr));
                }

                return diapers;
            }
        }

        public Diaper GetADiaperBySession(int sessionId, int careTakerId, int diaperId)
        {
            Diaper diaper = new Diaper();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM diaper
                                        JOIN session ON diaper.session_id = session.session_id
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE diaper.session_id = @session_id
                                        AND diaper.diaper_id = @diaper_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", sessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);
                cmd.Parameters.AddWithValue("@diaper_id", diaperId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    diaper = ParseRow(rdr);
                }

                return diaper;
            }
        }

        public Diaper AddDiaper(Diaper diaper)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"INSERT INTO diaper (session_id, time, notes)
	                                    VALUES (@session_id, @time, @notes)
                                        SELECT @@IDENTITY";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", diaper.SessionId);
                cmd.Parameters.AddWithValue("@time", diaper.Time);
                cmd.Parameters.AddWithValue("@notes", diaper.Notes);

                diaper.DiaperId = Convert.ToInt32(cmd.ExecuteScalar());

                return diaper;
            }
        }

        public Diaper UpdateDiaper(Diaper diaper, int careTakerId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"UPDATE diaper
                                        SET diaper.time = @time,
                                        diaper.notes = @notes
                                        OUTPUT INSERTED.diaper_id
                                        FROM diaper
                                        JOIN session ON diaper.session_id = session.session_id
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE diaper.nap_id = @nap_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@time", diaper.Time);
                cmd.Parameters.AddWithValue("@notes", diaper.Notes);
                cmd.Parameters.AddWithValue("@nap_id", diaper.DiaperId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                diaper.DiaperId = Convert.ToInt32(cmd.ExecuteScalar());

                return diaper;
            }
        }

        public int DeleteDiaper(int diaperId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"DELETE FROM diaper
                                        WHERE diaper_id = @diaper_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@diaper_id", diaperId);

                return cmd.ExecuteNonQuery();
            }
        }

        private Diaper ParseRow(SqlDataReader rdr)
        {
            Diaper diaper = new Diaper();

            diaper.DiaperId = Convert.ToInt32(rdr["diaper_id"]);
            diaper.SessionId = Convert.ToInt32(rdr["session_id"]);

            if (rdr["time"] != DBNull.Value)
            {
                diaper.Time = Convert.ToDateTime(rdr["time"]);
            }

            if (rdr["notes"] != DBNull.Value)
            {
                diaper.Notes = Convert.ToString(rdr["notes"]);
            }

            return diaper;
        }
    }
}
