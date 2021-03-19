using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class TransferSqlDAO : ITransferDAO
    {
        private readonly string connectionString;
        public TransferSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Transfer> GetAllTransfers(int userId)
        {
            List<Transfer> list = new List<Transfer>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "SELECT transfers.*, usersA.username userFrom, usersB.username userTo, ts.transfer_status_desc, tt.transfer_type_desc FROM transfers " +
                                    "JOIN accounts accountA ON accountA.account_id = transfers.account_from " +
                                    "JOIN accounts accountB ON accountB.account_id = transfers.account_to " +
                                    "JOIN users usersA ON usersA.user_id = accountA.user_id " +
                                    "JOIN users usersB ON usersB.user_id = accountB.user_id " +
                                    "JOIN transfer_statuses ts ON ts.transfer_status_id = transfers.transfer_status_id " +
                                    "JOIN transfer_types tt ON tt.transfer_type_id = transfers.transfer_type_id " +
                                    "WHERE usersA.user_id = @userId OR usersB.user_id = @userId;";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@userId", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        list.Add(GetTransferFromReader(reader));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return list;
        }
        
        public string SendTransfer(int userAccountFrom, int userAccountTo, decimal amount)
        {
            if(userAccountFrom == userAccountTo)
            {
                return "Cannot transfer money to account because destination matches source.";
            }
            //decimal currentBalance = accountDAO.GetAccountBalance(userAccountFrom);
            //if (amount < currentBalance)
            //{
                //accountDAO.AddToBalance(userAccountTo, amount);
                //accountDAO.SubtractFromBalance(userAccountFrom, amount);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "INSERT INTO transfers (transfer_type_id, transfer_status_id, account_from, account_to, amount) " +
                                    "VALUES(2,2,@userAccountFrom,@userAccountTo,@amount);";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@userAccountFrom", userAccountFrom);
                    cmd.Parameters.AddWithValue("@userAccountTo", userAccountTo);
                    cmd.Parameters.AddWithValue("@amount", amount);
                    cmd.ExecuteNonQuery();
                    return "Transfer complete";
                }
            //}
            //else
            //{
            //    return "Transfer failed: insufficient funds in account";
            //}
        }

        public Transfer GetTransferById(int transferId)
        {
            Transfer transfer = new Transfer();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string sqlText = "SELECT transfers.*, usersA.username AS userFrom, usersB.username AS userTo,ts.transfer_status_desc, tt.transfer_type_desc FROM transfers " +
                                    "JOIN accounts AS accountA ON accountA.account_id = transfers.account_from " +
                                    "JOIN accounts AS accountB ON accountB.account_id = transfers.account_to " +
                                    "JOIN users AS usersA ON usersA.user_id = accountA.user_id " +
                                    "JOIN users AS usersB ON usersB.user_id = accountB.user_id " +
                                    "JOIN transfer_statuses AS ts ON ts.transfer_status_id = transfers.transfer_status_id " +
                                    "JOIN transfer_types AS tt ON tt.transfer_type_id = transfers.transfer_type_id " +
                                    "WHERE transfers.transfer_id = @transferId;";
                    SqlCommand cmd = new SqlCommand(sqlText, conn);
                    cmd.Parameters.AddWithValue("@transferId", transferId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        transfer = GetTransferFromReader(reader);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return transfer;
        }

        private Transfer GetTransferFromReader(SqlDataReader reader)
        {
            Transfer transfer = new Transfer()
            {
                TransferId = Convert.ToInt32(reader["transfer_id"]),
                TransferTypeId = Convert.ToInt32(reader["transfer_type_id"]),
                TransferStatusId = Convert.ToInt32(reader["transfer_status_id"]),
                AccountFrom = Convert.ToInt32(reader["account_from"]),
                AccountTo = Convert.ToInt32(reader["account_to"]),
                Amount = Convert.ToDecimal(reader["amount"]),
                TransferType = Convert.ToString(reader["transfer_type_desc"]),
                TransferStatus = Convert.ToString(reader["transfer_status_desc"]),
                UserFrom = Convert.ToString(reader["userFrom"]),
                UserTo = Convert.ToString(reader["userTo"])
            };
            return transfer;
        }
    }
}
