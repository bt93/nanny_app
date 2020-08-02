using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public class NapSqlDAO : INapDAO
    {
        private string connectionString { get; set; }
        /// <summary>
        /// Creates a sql based Nap DAO
        /// </summary>
        /// <param name="dbconnectionString"></param>
        public NapSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public List<Nap> GetAllNapsBySession(int sessionId, int careTakerId)
        {
            List<Nap> naps = new List<Nap>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM nap
                                        JOIN session ON nap.session_id = session.session_id
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE nap.session_id = @session_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", sessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    naps.Add(ParseRow(rdr));
                }

                return naps;
            }
        }

        public Nap GetANapBySession(int sessionId, int careTakerId, int napId)
        {
            Nap naps = new Nap();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM nap
                                        JOIN session ON nap.session_id = session.session_id
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE nap.session_id = @session_id
                                        AND nap.nap_id = @nap_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", sessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);
                cmd.Parameters.AddWithValue("@nap_id", napId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    naps = ParseRow(rdr);
                }

                return naps;
            }
        }

        public Nap AddNap(Nap nap)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"INSERT INTO nap (session_id, start_time, notes)
	                                    VALUES (@session_id, @start_time, @notes)
                                        SELECT @@IDENTITY";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", nap.SessionId);
                cmd.Parameters.AddWithValue("@start_time", nap.StartTime);
                cmd.Parameters.AddWithValue("@notes", nap.Notes);

                nap.NapId = Convert.ToInt32(cmd.ExecuteScalar());

                return nap;
            }
        }

        public Nap UpdateNap(Nap nap, int careTakerId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"UPDATE nap
                                        SET nap.start_time = @start_time,
                                        nap.end_time = @end_time,
                                        nap.notes = @notes
                                        OUTPUT INSERTED.nap_id
                                        FROM nap
                                        JOIN session ON nap.session_id = session.session_id
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE nap.nap_id = @nap_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@start_time", nap.StartTime);
                cmd.Parameters.AddWithValue("@end_time", nap.EndTime);
                cmd.Parameters.AddWithValue("@notes", nap.Notes);
                cmd.Parameters.AddWithValue("@nap_id", nap.NapId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                nap.NapId = Convert.ToInt32(cmd.ExecuteScalar());

                return nap;
            }
        }

        public int DeleteNap(int napId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"DELETE FROM nap
                                        WHERE nap_id = @nap_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@nap_id", napId);

                return cmd.ExecuteNonQuery();
            }
        }

        private Nap ParseRow(SqlDataReader rdr)
        {
            Nap nap = new Nap();

            nap.NapId = Convert.ToInt32(rdr["nap_id"]);
            nap.SessionId = Convert.ToInt32(rdr["session_id"]);

            if (rdr["start_time"] != DBNull.Value)
            {
                nap.StartTime = Convert.ToDateTime(rdr["start_time"]);
            }

            if (rdr["end_time"] != DBNull.Value)
            {
                nap.EndTime = Convert.ToDateTime(rdr["end_time"]);
            }

            if (rdr["notes"] != DBNull.Value)
            {
                nap.Notes = Convert.ToString(rdr["notes"]);
            }

            return nap;
        }
    }
}
