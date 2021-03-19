using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public class AccountSqlDAO : IAccountDAO
    {
        
        private readonly string connectionString;
        public AccountSqlDAO(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }
        
        public List<decimal> GetUserBalances(int userId)
        {
            List<decimal> balance = new List<decimal>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT balance FROM accounts WHERE user_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        balance.Add(Convert.ToDecimal(reader["balance"]));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return balance;
        }

 

        public decimal GetAccountBalance(int accountId)
        {
            decimal balance = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT balance FROM accounts WHERE account_id = @account_id", conn);
                    cmd.Parameters.AddWithValue("@account_id", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        balance = (Convert.ToDecimal(reader["balance"]));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return balance;
        }
        public decimal AddToBalance(int userAccountId,decimal amount)
        {
          
            Account account = FindAccountById(userAccountId);
            decimal newBalance = account.Balance + amount;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE accounts SET balance = @newBalance WHERE user_id = @userAccountId", conn);
                    cmd.Parameters.AddWithValue("@userAccountId", userAccountId);
                    cmd.Parameters.AddWithValue("@newBalance", newBalance);
                    cmd.ExecuteNonQuery();

                    
                }
            }
            catch (SqlException)
            {
                throw;
            }
            
            return newBalance;
        }
        public decimal SubtractFromBalance(int userAccountId, decimal amount)
        {
            Account account = FindAccountById(userAccountId);
            decimal newBalance = account.Balance - amount;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("UPDATE accounts SET balance = @newBalance WHERE user_id = @userAccountId", conn);
                    cmd.Parameters.AddWithValue("@userAccountId", userAccountId);
                    cmd.Parameters.AddWithValue("@newBalance", newBalance);
                    cmd.ExecuteNonQuery();


                }
            }
            catch (SqlException)
            {
                throw;
            }


            return newBalance;
        }
        public Account FindAccountById(int accountId)
        {
            
            Account account = new Account();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM accounts WHERE account_id = @accountId", conn);
                    cmd.Parameters.AddWithValue("@accountId", accountId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows && reader.Read())
                    {
                        account.AccountId = Convert.ToInt32(reader["account_id"]);
                        account.UserId = Convert.ToInt32(reader["user_id"]);
                        account.Balance = Convert.ToDecimal(reader["balance"]);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return account;
        }

        public List<Account> ListAllAccounts()
        {
            List<Account> accounts = new List<Account>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM accounts JOIN users ON users.user_id=accounts.account_id", conn);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Account account = new Account();
                        account.AccountId = Convert.ToInt32(reader["account_id"]);
                        account.UserId = Convert.ToInt32(reader["user_id"]);
                        account.Balance = Convert.ToDecimal(reader["balance"]);
                        account.Username = Convert.ToString(reader["username"]);
                        accounts.Add(account);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return accounts;
        }

        public List<Account> ListUserAccounts(int userId)
        {
            List<Account> accounts = new List<Account>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT * FROM accounts JOIN users ON users.user_id=accounts.account_id WHERE users.user_id = @userId", conn);
                    cmd.Parameters.AddWithValue("@userid", userId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Account account = new Account();
                        account.AccountId = Convert.ToInt32(reader["account_id"]);
                        account.UserId = Convert.ToInt32(reader["user_id"]);
                        account.Balance = Convert.ToDecimal(reader["balance"]);
                        account.Username = Convert.ToString(reader["username"]);
                        accounts.Add(account);
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return accounts;
        }
    }
       
}
