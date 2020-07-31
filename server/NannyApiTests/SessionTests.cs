using Microsoft.VisualStudio.TestTools.UnitTesting;
using NannyApi.DAL;
using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace NannyApiTests
{
    [TestClass]
    public class SessionTests
    {
        private string connectionString = @"Server=.\sqlexpress;database=NannyDB; trusted_connection=true;";
        private TransactionScope transaction;

        // A place to hold the id's that were added to the database
        private int ruth;
        private int ellie;
        private int session1;

        [TestInitialize]
        public void SetupDB()
        {
            // This is "Begin Tran"
            transaction = new TransactionScope();

            string sqlScript = File.ReadAllText("Setup.sql");

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlScript, conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                if (rdr.Read())
                {
                    ruth = Convert.ToInt32(rdr["ruth"]);
                    ellie = Convert.ToInt32(rdr["ellie"]);
                    session1 = Convert.ToInt32(rdr["session1"]);
                }
            }
        }

        [TestCleanup]
        public void CleanupDB()
        {
            // Roll back the transaction
            transaction.Dispose();
        }

        [TestMethod]
        public void TestGetSessionById()
        {
            // Act
            SessionSqlDAO dao = new SessionSqlDAO(this.connectionString);

            // Arange
            Session session = dao.GetSessionById(session1, ruth);

            // Assert
            Assert.AreEqual(ellie, session.ChildId);
        }

        [TestMethod]
        public void TestGetAllSessionsByCareTakerId()
        {
            // Act
            SessionSqlDAO dao = new SessionSqlDAO(this.connectionString);

            // Arange
            List<Session> sessions = dao.GetAllSessionsByCareTakerId(ruth);

            // Assert
            Assert.AreEqual(4, sessions.Count);
        }

        [TestMethod]
        public void TestGetCurrentSessionsByCareTakerId()
        {
            // Act
            SessionSqlDAO dao = new SessionSqlDAO(this.connectionString);

            // Arange
            List<Session> sessions = dao.GetCurrentSessionsByCareTakerId(ruth);

            // Assert
            Assert.AreEqual(2, sessions.Count);
        }

        [TestMethod]
        public void TestCreateNewSession()
        {
            // Arrange
            SessionSqlDAO dao = new SessionSqlDAO(this.connectionString);
            Session session = new Session()
            {
                DropOff = DateTime.Now,
                Notes = "Ellie was big mad today."
            };

            // Act
            Session newSession = dao.CreateNewSession(session, ruth, ellie);

            // Assert
            Assert.AreEqual("Ellie was big mad today.", newSession.Notes);
        }

        [TestMethod]
        public void TestUpdateOpenSession()
        {
            // Arrange
            SessionSqlDAO dao = new SessionSqlDAO(this.connectionString);
            Session session = new Session()
            {
                SessionId = session1,
                DropOff = DateTime.Now,
                Notes = "Ellie was big mad today."
            };

            // Act
            Session newSession = dao.UpdateOpenSession(session, ruth);

            // Assert
            Assert.AreEqual(session1, newSession.SessionId);
        }

        [TestMethod]
        public void TestEndSession()
        {
            // Arrange
            SessionSqlDAO dao = new SessionSqlDAO(this.connectionString);
            Session session = new Session()
            {
                SessionId = session1,
                DropOff = DateTime.Now,
                PickUp = DateTime.Now.AddHours(7),
                Notes = "Ellie was big mad today."
            };

            // Act
            Session newSession = dao.UpdateOpenSession(session, ruth);

            // Assert
            Assert.AreEqual(DateTime.Now.AddHours(7).ToString(), newSession.PickUp.ToString());
        }
    }
}
