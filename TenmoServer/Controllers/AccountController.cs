using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountDAO accountDAO;

        public AccountController(IAccountDAO _accountDAO)
        {
            accountDAO = _accountDAO;
        }

        [HttpGet("balance/{id}")]
        public List<decimal> GetUserBalances(int id)
        {
            return accountDAO.GetUserBalances(id);
        }

        [HttpGet("{id}/list")]
        public List<Account> GetCurrentAccounts(int id)
        {
            return accountDAO.ListUserAccounts(id);
        }

        [HttpGet("listaccounts")]
        public List<Account> ListAccounts()
        {
            return accountDAO.ListAllAccounts();
        }

        [HttpPut("{id}/addfunds/{amount}")]
        public decimal AddFundsToAccount(int id, decimal amount)
        {
            return accountDAO.AddToBalance(id, amount);
        }
        [HttpPut("{id}/subtractfunds/{amount}")]
        public decimal FundsToAccount(int id, decimal amount)
        {
            return accountDAO.SubtractFromBalance(id, amount);
        }
    }

    
}
