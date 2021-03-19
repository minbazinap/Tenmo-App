using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TenmoServer.Models;

namespace TenmoServer.DAO
{
    public interface ITransferDAO
    {
        List<Transfer> GetAllTransfers(int userId);
        string SendTransfer(int userAccountFrom, int userAccountTo, decimal amount);
        Transfer GetTransferById(int transferId);
    }
}
