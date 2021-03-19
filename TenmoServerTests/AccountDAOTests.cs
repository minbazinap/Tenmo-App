using System;
using System.Collections.Generic;
using System.Text;
using TenmoServer.DAO;
using TenmoServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TenmoServerTests
{
    [TestClass]
    public class AccountDAOTests : StarterDAOTests
    {
        [TestMethod]
        public void GetUserBalances_Returns_Correct_Results()
        {
            AccountSqlDAO dao = new AccountSqlDAO(ConnectionString);
            List<decimal> balance = dao.GetUserBalances(1);
            Assert.AreEqual(Convert.ToDecimal(1500.54), balance[0]);
        }
        [TestMethod]
        public void AddToBalance_Returns_Correct_Balance()
        {
            AccountSqlDAO dao = new AccountSqlDAO(ConnectionString);
            decimal balance = dao.AddToBalance(1, Convert.ToDecimal(100));
            Assert.AreEqual(Convert.ToDecimal(1600.54), balance);
        }
        [TestMethod]
        public void GetAccountBalance_Is_True()
        {
            AccountSqlDAO dao = new AccountSqlDAO(ConnectionString);
            decimal balance = dao.GetAccountBalance(1);
            Assert.AreEqual(Convert.ToDecimal(1500.54), balance);
        }
        [TestMethod]
        public void ListAllAccounts_Should_Return_The_Correct_Count()
        {
            AccountSqlDAO dao = new AccountSqlDAO(ConnectionString);
            List<Account> accounts = dao.ListAllAccounts();
            Assert.AreEqual(3, accounts.Count);
        }
        [TestMethod]
        public void SubtractFromBalance_Return_Correct_Balance()
        {
            AccountSqlDAO dao = new AccountSqlDAO(ConnectionString);
            decimal balance = dao.SubtractFromBalance(1, Convert.ToDecimal(100));
            Assert.AreEqual(Convert.ToDecimal(1400.54), balance);
        }
        [TestMethod]
        public void SubtractFromBalance_Do_Not_Return_Correct_Balance()
        {
            AccountSqlDAO dao = new AccountSqlDAO(ConnectionString);
            decimal balance = dao.SubtractFromBalance(1, Convert.ToDecimal(2000));
            Assert.AreEqual(Convert.ToDecimal(1500.54), balance);
        }
        [TestMethod]
        public void FindAccountById_Return_Correct_Account()
        {
            AccountSqlDAO dao = new AccountSqlDAO(ConnectionString);
            Account account = dao.FindAccountById(1);
            Assert.AreEqual(1, account.AccountId);
        }
        [TestMethod]
        public void ListUserAccounts_Return_Correct_Number_Of_Account()
        {
            AccountSqlDAO dao = new AccountSqlDAO(ConnectionString);
            List<Account> accounts = dao.ListUserAccounts(3);
            Assert.AreEqual(1, accounts.Count);
        }
    }
    
}
