using Microsoft.VisualStudio.TestTools.UnitTesting;
using NannyApi.DAL;
using NannyApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;

namespace NannyApiTests
{
    [TestClass]
    public class ParentTests
    {
        private string connectionString = @"Server=.\sqlexpress;database=NannyDB; trusted_connection=true;";
        private TransactionScope transaction;

        // A place to hold the id's that were added to the database
        private int megan;
        private int mayfield;
        private int ellie;
        private int ruth;

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
                    megan = Convert.ToInt32(rdr["megan"]);
                    mayfield = Convert.ToInt32(rdr["mayfield"]);
                    ellie = Convert.ToInt32(rdr["ellie"]);
                    ruth = Convert.ToInt32(rdr["ruth"]);
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
        public void TestGetParentsCount()
        {
            // Arrange
            ParentSqlDAO dao = new ParentSqlDAO(this.connectionString);

            // Act
            List<Parent> parents = dao.GetParents(ruth);

            // Assert
            Assert.AreEqual(3, parents.Count);
        }

        [TestMethod]
        public void TestGetParentById()
        {
            // Arrange
            ParentSqlDAO dao = new ParentSqlDAO(this.connectionString);
            Parent testParent = new Parent()
            {
                AddressId = mayfield,
                FirstName = "Megan",
                LastName = "Kweicen",
                EmailAddress = "askdfja",
                PhoneNumber = "342342432",
            };
            testParent.Address.Street = "34243243";
            testParent.Address.City = "sdlktgj";
            testParent.Address.State = "akedgihj";
            testParent.Address.Zip = 324234;
            testParent.Address.County = "asf";
            testParent.Address.Country = "ertwseg";

            // Act
            Parent parent = dao.GetParentById(megan, ruth);

            // Assert
            Assert.AreEqual("Megan", parent.FirstName);
        }

        [TestMethod]
        public void TestGetParentsByChildCount()
        {
            // Arrange
            ParentSqlDAO dao = new ParentSqlDAO(this.connectionString);

            // Act
            List<Parent> parents = dao.GetParentsByChild(ellie, ruth);

            // Assert
            Assert.AreEqual(2, parents.Count);
        }

        [TestMethod]
        public void TestAddParent()
        {
            // Arrange
            ParentSqlDAO dao = new ParentSqlDAO(this.connectionString);
            Parent testParent = new Parent()
            {
                AddressId = mayfield,
                FirstName = "Megan",
                LastName = "Kweicen",
                EmailAddress = "askdfja",
                PhoneNumber = "342342432",
            };
            testParent.Address.Street = "34243243";
            testParent.Address.City = "sdlktgj";
            testParent.Address.State = "akedgihj";
            testParent.Address.Zip = 324234;
            testParent.Address.County = "asf";
            testParent.Address.Country = "ertwseg";

            // Act
            dao.AddParent(testParent);
            Parent parent = dao.GetParentById(megan, ruth);

            // Assert
            Assert.AreEqual("Megan", parent.FirstName);
        }

        [TestMethod]
        public void TestUpdateParent()
        {
            // Arrange
            ParentSqlDAO dao = new ParentSqlDAO(this.connectionString);
            Parent testParent = new Parent()
            {
                ParentId = megan,
                AddressId = mayfield,
                FirstName = "Meg",
                LastName = "Kweicen",
                EmailAddress = "askdfja",
                PhoneNumber = "342342432",
            };
            testParent.Address.Street = "34243243";
            testParent.Address.City = "sdlktgj";
            testParent.Address.State = "akedgihj";
            testParent.Address.Zip = 324234;
            testParent.Address.County = "asf";
            testParent.Address.Country = "ertwseg";

            // Act
            dao.UpdateParent(testParent);
            Parent parent = dao.GetParentById(megan, ruth);

            // Assert
            Assert.AreEqual("Meg", parent.FirstName);
        }
    }
}
