using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Transactions;


namespace TenmoServerTests
{
    [TestClass]
    public class StarterDAOTests
    {
        //Model after 28_Integration_Testing exercise-final "ProjectOrganizerDAOTests

        protected string ConnectionString { get; } = @"Data Source=.\SQLEXPRESS;Initial Catalog=tenmo;Integrated Security=True";

        //protected variables go here
        

        private TransactionScope transaction;

        [TestInitialize]
        public void Setup()
        {
            transaction = new TransactionScope();

            // Get the sql
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string sql = File.ReadAllText(projectDirectory + "\\tenmo-test.sql");
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    //DepartmentId = Convert.ToInt32(reader["departmentId"]);
                    //AssignedEmployeeId = Convert.ToInt32(reader["assignedEmployeeId"]);
                    //UnassignedEmployeeId = Convert.ToInt32(reader["unassignedEmployeeId"]);
                    //ProjectId = Convert.ToInt32(reader["projectId"]);
                }
            }
        }

        /// <summary>
        /// Cleans up the database after each test.
        /// </summary>
        [TestCleanup]
        public void Cleanup()
        {
            transaction.Dispose();
        }

        /// <summary>
        /// Gets the row count for a given table.
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        protected int GetRowCount(string table)
        {
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT COUNT(*) FROM {table};", conn);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count;
            }
        }

    }
}
