﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class ChildTests
    {
        private string connectionString = @"Server=.\sqlexpress;database=NannyDB; trusted_connection=true;";
        private TransactionScope transaction;

        // A place to hold the id's that were added to the database
        private int ellie;
        private int johnny;
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
                    ellie = Convert.ToInt32(rdr["ellie"]);
                    johnny = Convert.ToInt32(rdr["johnny"]);
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
        public void TestGetAllChildren()
        {
            // Arrange
            ChildSqlDAO dao = new ChildSqlDAO(this.connectionString);

            // Act
            List<Child> children = dao.GetChildren(ruth);

            //Assert
            Assert.AreEqual(2, children.Count);
        }

        [TestMethod]
        public void TestGetDeactivatedChildren()
        {
            // Arrange
            ChildSqlDAO dao = new ChildSqlDAO(this.connectionString);

            // Act
            List<Child> children = dao.GetDeactivedChildren(ruth);

            //Assert
            Assert.AreEqual(2, children.Count);
        }

        [TestMethod]
        public void TestGetChildById()
        {
            // Arrange
            ChildSqlDAO dao = new ChildSqlDAO(this.connectionString);

            // Act
            Child child = dao.GetChildById(ellie, ruth);

            //Assert
            Assert.AreEqual("Ellie", child.FirstName);
        }

        [TestMethod]
        public void TestGetDeactivatedChildById()
        {
            // Arrange
            ChildSqlDAO dao = new ChildSqlDAO(this.connectionString);

            // Act
            Child child = dao.GetDeactivatedChildById(johnny, ruth);

            //Assert
            Assert.AreEqual("Johnny", child.FirstName);
        }

        [TestMethod]
        public void TestAddChild()
        {
            // Arrange
            ChildSqlDAO dao = new ChildSqlDAO(this.connectionString);
            Child newChild = new Child()
            {
                FirstName = "Ellie",
                LastName = "Kweicen",
                Gender = 'F',
                DateOfBirth = DateTime.Now,
                RatePerHour = 6.30M,
                NeedsDiapers = true,
                ImageUrl = ""
            };

            // Act
            Child checkChild = dao.AddChild(newChild, ruth);

            // Assert
            Assert.AreEqual("Ellie", checkChild.FirstName);
        }

        [TestMethod]
        public void TestUpdateChild()
        {
            // Arrange
            ChildSqlDAO dao = new ChildSqlDAO(this.connectionString);
            Child newChild = new Child()
            {
                FirstName = "El",
                LastName = "Kweicen",
                Gender = 'F',
                DateOfBirth = DateTime.Now,
                RatePerHour = 6.30M,
                NeedsDiapers = true,
                ImageUrl = ""
            };

            // Act
            Child updateChild = dao.UpdateChild(newChild, ellie, ruth);

            // Assert
            Assert.AreEqual("El", updateChild.FirstName);
        }

        [TestMethod]
        public void TestDeleteChild()
        {
            // Arrange
            ChildSqlDAO dao = new ChildSqlDAO(this.connectionString);

            // Act
            int childId = dao.DeleteChild(ellie, ruth);

            // Assert
            Assert.AreEqual(ellie, childId);
        }

        [TestMethod]
        public void TestReinstateChild()
        {
            // Arrange
            ChildSqlDAO dao = new ChildSqlDAO(this.connectionString);

            // Act
            int childId = dao.ReinstateChild(ellie, ruth);

            // Assert
            Assert.AreEqual(ellie, childId);
        }
    }
}
