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
        private int meal1;

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
                    meal1 = Convert.ToInt32(rdr["meal1"]);
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
        public void TestGetAllSessionsByChildId()
        {
            // Act
            SessionSqlDAO dao = new SessionSqlDAO(this.connectionString);

            // Arange
            List<int> sessions = dao.GetAllSessionsByChildId(ellie, ruth);

            // Assert
            Assert.AreEqual(3, sessions.Count);
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

        [TestMethod]
        public void TestUpdateClosedSession()
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
            Session newSession = dao.UpdateClosedSession(session, ruth);

            // Assert
            Assert.AreEqual(session1, newSession.SessionId);
        }

        [TestMethod]
        public void TestDeleteSession()
        {
            // Arrange
            SessionSqlDAO dao = new SessionSqlDAO(this.connectionString);

            // Act
            bool isDeleted = dao.DeleteSession(session1, ruth);

            // Assert
            Assert.AreEqual(true, isDeleted);
        }

        [TestMethod]
        public void TestGetAllMealsBySession()
        {
            // Arrange
            MealSqlDAO dao = new MealSqlDAO(this.connectionString);

            // Act
            List<Meal> meals = dao.GetAllMealsBySession(session1, ruth);

            // Assert
            Assert.AreEqual(3, meals.Count);
        }

        [TestMethod]
        public void TestGetAMealBySession()
        {
            // Arrange
            MealSqlDAO dao = new MealSqlDAO(this.connectionString);

            // Act
            Meal meal = dao.GetAMealBySession(session1, ruth, meal1);

            // Assert
            Assert.AreEqual("Breakfast", meal.Type);
        }

        [TestMethod]
        public void TestAddMeal()
        {
            // Arrange
            MealSqlDAO dao = new MealSqlDAO(this.connectionString);
            Meal newMeal = new Meal()
            {
                SessionId = session1,
                Time = DateTime.Now,
                Type = "Snack",
                Notes = "She was hungry!"
            };

            // Act
            Meal meal = dao.AddMeal(newMeal);

            // Assert
            Assert.AreEqual("Snack", meal.Type);
        }

        [TestMethod]
        public void TestUpdateMeal()
        {
            // Arrange
            MealSqlDAO dao = new MealSqlDAO(this.connectionString);
            Meal newMeal = new Meal()
            {
                MealId = meal1,
                SessionId = session1,
                Time = DateTime.Now,
                Type = "Snack",
                Notes = "She was not hungry!"
            };

            // Act
            Meal meal = dao.UpdateMeal(newMeal, ruth);

            // Assert
            Assert.AreEqual("Snack", meal.Type);
        }
    }
}
