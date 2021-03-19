using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface IAccountDAO
    {
        List<decimal> GetUserBalances(int userId);
        decimal AddToBalance(int userAccountId, decimal amount);
        decimal SubtractFromBalance(int userAccountId, decimal amount);
        Account FindAccountById(int accountId);
        decimal GetAccountBalance(int accountId);
        List<Account> ListAllAccounts();
        List<Account> ListUserAccounts(int userId);
    }
}
