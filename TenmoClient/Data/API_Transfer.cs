using System;
using System.Collections.Generic;
using System.Text;

namespace TenmoClient.Data
{
    public class API_Transfer
    {
		public int transferId { get; set; }
		public int transferTypeId { get; set; }
		public int transferStatusId { get; set; }
		public int accountFrom { get; set; }
		public int accountTo { get; set; }
		public decimal amount { get; set; }
		public string transferType { get; set; }
		public string transferStatus { get; set; }
		public string userFrom { get; set; }
		public string userTo { get; set; }
	}
}
