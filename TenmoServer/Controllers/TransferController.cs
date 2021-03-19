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
    public class TransferController : ControllerBase
    {
        private readonly IAccountDAO accountDAO;
        private readonly ITransferDAO transferDAO;

        public TransferController(IAccountDAO _accountDAO, ITransferDAO _transferDAO)
        {
            accountDAO = _accountDAO;
            transferDAO = _transferDAO;
        }

        [HttpGet("account/transfers/{id}")]
        public List<Transfer> GetAllUserTransfers(int id)
        {
            return transferDAO.GetAllTransfers(id);
        }

        [HttpGet("{id}")]
        public Transfer GetSpecificTransferDetails(int id)
        {
            return transferDAO.GetTransferById(id);
        }

        [HttpPost("{id}/{amount}/{idTo}")]
        public string TransferFunds(int id, int idTo, decimal amount)
        {
            return transferDAO.SendTransfer(id, idTo, amount);
        }
    }
}
