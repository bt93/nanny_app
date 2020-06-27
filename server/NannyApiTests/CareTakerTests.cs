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
    public class CareTakerTests
    {
        private string connectionString = @"Server=.\sqlexpress;database=NannyDB; trusted_connection=true;";
        private TransactionScope transaction;

        // A place to hold the id's that were added to the database
        private int berkshire;
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
                    berkshire = Convert.ToInt32(rdr["berkshire"]);
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
        public void TestCareTakerCount()
        {
            // Arrange
            CareTakerSqlDAO dao = new CareTakerSqlDAO(this.connectionString);

            // Act
            IList<CareTaker> careTakers = dao.GetAllCareTakers();

            // Assert
            Assert.AreEqual(3, careTakers.Count);
        }

        [TestMethod]
        public void TestGetCareTakerByName()
        {
            // Arrange
            CareTakerSqlDAO dao = new CareTakerSqlDAO(this.connectionString);

            // Act
            CareTaker careTaker = dao.GetCareTakerByName("Ruth", "Howie");
            

            // Assert
            Assert.AreEqual("Ruth", careTaker.FirstName);
        }

        [TestMethod]
        public void TestAddCareTaker()
        {
            // Arrange
            CareTakerSqlDAO dao = new CareTakerSqlDAO(this.connectionString);
            CareTaker testCareTaker = new CareTaker()
            {
                AddressId = berkshire,
                FirstName = "Jason",
                LastName = "Howie",
                EmailAddress = "askdfja",
                Password = "pass",
                PhoneNumber = "342342432",
                Street = "34243243",
                City = "sdlktgj",
                State = "akedgihj",
                Zip = 324234,
                County = "asf",
                Country = "ertwseg"
            };

            // Act
            dao.AddCareTaker(testCareTaker);
            CareTaker careTaker = dao.GetCareTakerByName("Jason", "Howie");


            // Assert
            Assert.AreEqual("Jason", careTaker.FirstName);
        }

        [TestMethod]
        public void TestGetCareTakerById()
        {
            // Arrange
            CareTakerSqlDAO dao = new CareTakerSqlDAO(this.connectionString);
            CareTaker testCareTaker = new CareTaker()
            {
                AddressId = berkshire,
                FirstName = "Ruth",
                LastName = "Howie",
                EmailAddress = "askdfja",
                Password = "pass",
                PhoneNumber = "342342432",
                Street = "34243243",
                City = "sdlktgj",
                State = "akedgihj",
                Zip = 324234,
                County = "asf",
                Country = "ertwseg"
            };

            // Act
            dao.AddCareTaker(testCareTaker);
            CareTaker careTaker = dao.GetCareTakerById(ruth);


            // Assert
            Assert.AreEqual("Ruth", careTaker.FirstName);
        }

        [TestMethod]
        public void TestUpdateCareTaker()
        {
            // Arrange
            CareTakerSqlDAO dao = new CareTakerSqlDAO(this.connectionString);
            CareTaker testCareTaker = new CareTaker()
            {
                CareTakerId = ruth,
                AddressId = berkshire,
                FirstName = "Ru",
                LastName = "Howie",
                EmailAddress = "askdfja",
                Password = "pass",
                PhoneNumber = "342342432",
                Street = "34243243",
                City = "sdlktgj",
                State = "akedgihj",
                Zip = 324234,
                County = "asf",
                Country = "ertwseg"
            };

            // Act
            dao.UpdateCareTaker(testCareTaker);
            CareTaker careTaker = dao.GetCareTakerById(ruth);


            // Assert
            Assert.AreEqual("Ru", careTaker.FirstName);
        }

        [TestMethod]
        public void TestDeleteCareTaker()
        {
            // Arrange
            CareTakerSqlDAO dao = new CareTakerSqlDAO(this.connectionString);

            // Act
            bool isDeleted = dao.DeleteCareTaker(ruth);            

            // Assert
            Assert.AreEqual(true, isDeleted);
        }
    }
}
