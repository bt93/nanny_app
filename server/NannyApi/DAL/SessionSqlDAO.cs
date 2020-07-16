using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NannyApi.DAL
{
    public class SessionSqlDAO : ISessionDAO
    {
        private string connectionString { get; set; }
        /// <summary>
        /// Creates a sql based caretaker DAO
        /// </summary>
        /// <param name="dbconnectionString"></param>
        public SessionSqlDAO(string dbconnectionString)
        {
            this.connectionString = dbconnectionString;
        }

        public Session CreateNewSession(Session session, int caretakerId, int childId)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                conn.Open();

                // Creates a new session row
                const string sessionSql = @"INSERT INTO session (child_id, drop_off, notes)
                                                VALUES (@child_id, @drop_off, @notes)
                                                SELECT @@Identity";

                SqlCommand cmd = new SqlCommand(sessionSql, conn);
                cmd.Parameters.AddWithValue("@child_id", childId);
                cmd.Parameters.AddWithValue("@drop_off", session.DropOff);
                cmd.Parameters.AddWithValue("@notes", session.Notes);

                // Takes that Identity so it can be added to session_caretaker
                session.SessionId = Convert.ToInt32(cmd.ExecuteScalar());
                session.ChildId = childId;

                const string caretakerSql = @"INSERT INTO session_caretaker (session_id, caretaker_id)
                                                VALUES (@session_id, @caretaker_id)";
                cmd = new SqlCommand(caretakerSql, conn);
                cmd.Parameters.AddWithValue("@session_id", session.SessionId);
                cmd.Parameters.AddWithValue("@caretaker_id", caretakerId);
                // Adds to session_caretaker
                cmd.ExecuteNonQuery();

                return session;
            }
        }
    }
}
