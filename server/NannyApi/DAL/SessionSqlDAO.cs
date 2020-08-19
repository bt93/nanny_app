using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public class SessionSqlDAO : ISessionDAO
    {
        private string connectionString { get; set; }
        /// <summary>
        /// Creates a sql based Session DAO
        /// </summary>
        /// <param name="dbconnectionString"></param>
        public SessionSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public Session GetSessionById(int sessionId, int careTakerId)
        {
            Session session = new Session();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM session
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE session.session_id = @session_id
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@session_id", sessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    session = ParseRow(rdr);
                }
            }

            return session;
        }

        public List<Session> GetAllSessionsByCareTakerId(int careTakerId)
        {
            List<Session> sessions = new List<Session>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM session
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE session_caretaker.caretaker_id = @caretaker_id
                                        ORDER BY session.drop_off DESC";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    sessions.Add(ParseRow(rdr));
                }
            }

            return sessions;
        }


        public List<int> GetAllSessionsByChildId(int childId, int careTakerId)
        {
            List<int> sessions = new List<int>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM session
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE session_caretaker.caretaker_id = @caretaker_id
                                        AND session.child_id = @child_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);
                cmd.Parameters.AddWithValue("@child_id", childId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    sessions.Add(Convert.ToInt32(rdr["session_id"]));
                }
            }

            return sessions;
        }

        public List<Session> GetCurrentSessionsByCareTakerId(int caretakerId)
        {
            List<Session> sessions = new List<Session>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"SELECT *
                                        FROM session
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE session.pick_up IS NULL
                                        AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@caretaker_id", caretakerId);

                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    sessions.Add(ParseRow(rdr));
                }
            }

            return sessions;
        }

        public Session CreateNewSession(Session session, int caretakerId, int childId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("dbo.createNewSession", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@child_id", childId);
                cmd.Parameters.AddWithValue("@drop_off", session.DropOff);
                cmd.Parameters.AddWithValue("@notes", session.Notes);
                cmd.Parameters.AddWithValue("@caretaker_id", caretakerId);
                // Adds to session_caretaker
                session.SessionId = Convert.ToInt32(cmd.ExecuteScalar());

                return session;
            }
        }

        public Session UpdateOpenSession(Session session, int careTakerId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"UPDATE session
                                    SET session.drop_off = @drop_off,
                                    session.notes = @notes
                                    OUTPUT INSERTED.session_id
                                    FROM session
                                    JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                    WHERE session.session_id = @session_id
                                    AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@drop_off", session.DropOff);
                cmd.Parameters.AddWithValue("@notes", session.Notes);
                cmd.Parameters.AddWithValue("@session_id", session.SessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                session.SessionId = Convert.ToInt32(cmd.ExecuteScalar());

                return session;
            }
        }

        public Session EndSession(Session session, int careTakerId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"UPDATE session
                                    SET session.drop_off = @drop_off,
                                    session.pick_up = @pick_up,
                                    session.notes = @notes
                                    OUTPUT INSERTED.session_id
                                    FROM session
                                    JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                    WHERE session.session_id = @session_id
                                    AND session_caretaker.caretaker_id = @caretaker_id";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@drop_off", session.DropOff);
                cmd.Parameters.AddWithValue("@pick_up", session.PickUp);
                cmd.Parameters.AddWithValue("@notes", session.Notes);
                cmd.Parameters.AddWithValue("@session_id", session.SessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                session.SessionId = Convert.ToInt32(cmd.ExecuteScalar());

                return session;
            }
        }

        public Session UpdateClosedSession(Session session, int careTakerId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                const string sql = @"UPDATE session
                                        SET session.drop_off = @drop_off,
                                        session.pick_up = @pick_up,
                                        session.notes = @notes
                                        OUTPUT INSERTED.session_id
                                        FROM session
                                        JOIN session_caretaker ON session.session_id = session_caretaker.session_id
                                        WHERE session.session_id = @session_id
                                        AND session_caretaker.caretaker_id = @caretaker_id
                                        AND session.pick_up IS NOT NULL";

                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@drop_off", session.DropOff);
                cmd.Parameters.AddWithValue("@pick_up", session.PickUp);
                cmd.Parameters.AddWithValue("@notes", session.Notes);
                cmd.Parameters.AddWithValue("@session_id", session.SessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);

                session.SessionId = Convert.ToInt32(cmd.ExecuteScalar());

                return session;
            }
        }

        public bool DeleteSession(int sessionId, int careTakerId)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(this.connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("dbo.deleteSession", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@session_id", sessionId);
                    cmd.Parameters.AddWithValue("@caretaker_id", careTakerId);
                    
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private Session ParseRow(SqlDataReader rdr)
        {
            Session session = new Session();

            // Gets session details
            session.SessionId = Convert.ToInt32(rdr["session_id"]);
            session.ChildId = Convert.ToInt32(rdr["child_id"]);
            session.DropOff = Convert.ToDateTime(rdr["drop_off"]);
            if (rdr["pick_up"] != DBNull.Value)
            {
                session.PickUp = Convert.ToDateTime(rdr["pick_up"]);
            } 

            if (rdr["notes"] != DBNull.Value)
            {
                session.Notes = Convert.ToString(rdr["notes"]);
            }

            return session;
        }
    }
}
