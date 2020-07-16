using Microsoft.VisualStudio.TestTools.UnitTesting;
using NannyApi.DAL;
using NannyApi.Models;
using System;
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
    }
}
