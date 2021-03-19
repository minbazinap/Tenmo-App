using System;
using System.Collections.Generic;
using System.Text;
using TenmoServer.DAO;
using TenmoServer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TenmoServerTests
{
    [TestClass]
    public class TransferDAOTests : StarterDAOTests
    {
        [TestMethod]

        public void GetAllTransfer_Returns_Correct_Number_Rows()
        {
            TransferSqlDAO dao = new TransferSqlDAO(ConnectionString);
            List<Transfer> transfer = dao.GetAllTransfers(1);
            Assert.AreEqual(7, transfer.Count);
        }

        [TestMethod]
        public void SendTransfer_Returns_SameAccount_Error_Message()
        {
            TransferSqlDAO dao = new TransferSqlDAO(ConnectionString);
            string message = dao.SendTransfer(1, 1, 100);
            Assert.AreEqual("Cannot transfer money to account because destination matches source.", message);
        }

        [TestMethod]
        public void SendTransfer_Returns_TransferComplete()
        {
            TransferSqlDAO dao = new TransferSqlDAO(ConnectionString);
            string message = dao.SendTransfer(1, 2, 100);
            Assert.AreEqual("Transfer complete", message);
        }

        [TestMethod]
        public void GetTransferById_Returns_Correct_TransferId()
        {
            TransferSqlDAO dao = new TransferSqlDAO(ConnectionString);
            Transfer transfer = dao.GetTransferById(2);
            Assert.AreEqual(2, transfer.TransferId);
        }

        [TestMethod]
        public void GetTransferById_Returns_No_Data()
        {
            TransferSqlDAO dao = new TransferSqlDAO(ConnectionString);
            Transfer transfer = dao.GetTransferById(10);
            Assert.AreEqual(0, transfer.Amount);
        }


    }
}
