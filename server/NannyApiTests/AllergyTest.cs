using Microsoft.VisualStudio.TestTools.UnitTesting;
using NannyApi.DAL;
using NannyApi.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Transactions;

namespace NannyApiTests
{
    [TestClass]
    public class AllergyTest
    {
        private string connectionString = @"Server=.\sqlexpress;database=NannyDB; trusted_connection=true;";
        private TransactionScope transaction;

        // A place to hold the id's that were added to the database

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
        public void TestGetAllergies()
        {
            AllergySqlDAO dao = new AllergySqlDAO(this.connectionString);
            List<Allergy> allergies = dao.GetAllergies();

            Assert.AreEqual(74, allergies.Count);
        }

        [TestMethod]
        public void TestGetAllergiesByType()
        {
            AllergySqlDAO dao = new AllergySqlDAO(this.connectionString);
            List<Allergy> allergies = dao.GetAllergiesByType(2); // Food

            Assert.AreEqual(20, allergies.Count);
        }
    }
}
