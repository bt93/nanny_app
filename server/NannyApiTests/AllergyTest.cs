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
    public class AllergyTest
    {
        private string connectionString = @"Server=.\sqlexpress;database=NannyDB; trusted_connection=true;";
        private TransactionScope transaction;

        // A place to hold the id's that were added to the database
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

        [TestMethod]
        public void TestGetAllergiesByChildId()
        {
            AllergySqlDAO dao = new AllergySqlDAO(this.connectionString);
            List<Allergy> allergies = dao.GetAllergiesByChildId(ellie);

            Assert.AreEqual(1, allergies.Count);
            Assert.AreEqual("Banana", allergies[0].Name);
        }

        [TestMethod]
        public void TestAddAllergyToChild()
        {
            AllergySqlDAO dao = new AllergySqlDAO(this.connectionString);
            bool isRowAffected = dao.AddAllergyToChild(ellie, 33);
            List<Allergy> allergies = dao.GetAllergiesByChildId(ellie);

            Assert.IsTrue(isRowAffected);
            Assert.AreEqual(2, allergies.Count);
            Assert.AreEqual("Mustard Seed", allergies[1].Name);
        }

        [TestMethod]
        public void TestRemoveAllergryFromChild()
        {
            AllergySqlDAO dao = new AllergySqlDAO(this.connectionString);
            bool isRowAffected = dao.RemoveAllergyFromChild(ellie, 27);
            List<Allergy> allergies = dao.GetAllergiesByChildId(ellie);

            Assert.IsTrue(isRowAffected);
            Assert.AreEqual(0, allergies.Count);
        }

        [TestMethod]
        public void TestGetAllergyTypes()
        {
            AllergySqlDAO dao = new AllergySqlDAO(this.connectionString);
            List<AllergyType> types = dao.getAllergyTypes();

            Assert.IsNotNull(types);
            Assert.AreEqual(8, types.Count);
        }
    }
}
