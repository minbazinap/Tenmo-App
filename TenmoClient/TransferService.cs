using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using RestSharp;
using TenmoClient.Data;

namespace TenmoClient
{
    public class TransferService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();

        public List<API_Transfer> GetAllUserTransfer(int userId)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "transfer/account/transfers/" + userId);
            IRestResponse<List<API_Transfer>> response = client.Get<List<API_Transfer>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Transfers");
                Console.WriteLine("ID\t\tFrom/To\t\tAmount");
                Console.WriteLine("-------------------------------------------");
                List<API_Transfer> transfers = new List<API_Transfer>(response.Data);
                foreach(API_Transfer transfer in transfers)
                {
                    string nameDisplayed = "";
                    if (transfer.accountFrom == UserService.GetUserId())
                    {
                        nameDisplayed = transfer.userTo;
                    }
                    else
                    {
                        nameDisplayed = transfer.userFrom;
                    }
                    Console.WriteLine($"{transfer.transferId}\t\t{GetFromOrTo(transfer.accountFrom)} {nameDisplayed}\t\t{transfer.amount:C2}");
                }
                Console.WriteLine("-------------------------------------------");
                
                List<int> validIds = new List<int>();
                foreach(API_Transfer transfer in transfers)
                {
                    validIds.Add(transfer.transferId);
                }
                int selectedId = -1;
                bool isValidId = false;
                while(isValidId == false)
                {
                    if (selectedId == 0)
                    {
                        return new List<API_Transfer>();
                    }
                    else if (!validIds.Contains(selectedId))
                    {
                        Console.WriteLine("Please enter transfer ID to view details (0 to cancel):");
                        try
                        {
                            selectedId = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Must be an ID listed.");
                        }
                        
                    }
                    
                    else
                    {
                        isValidId = true;
                    }
                }
                RestRequest request2 = new RestRequest(API_BASE_URL + "transfer/" + selectedId);
                IRestResponse<API_Transfer> response2 = client.Get<API_Transfer>(request2);
                if (response2.ResponseStatus != ResponseStatus.Completed)
                {
                    ProcessErrorResponse(response2);
                }
                else
                {
                    API_Transfer transfer = new API_Transfer();
                    transfer = response2.Data;
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine("Transfer Details");
                    Console.WriteLine("--------------------------------------------");
                    Console.WriteLine($"Id: {transfer.transferId}");
                    Console.WriteLine($"From: {transfer.userFrom}");
                    Console.WriteLine($"To: {transfer.userTo}");
                    Console.WriteLine($"Type: {transfer.transferType}");
                    Console.WriteLine($"Status: {transfer.transferStatus}");
                    Console.WriteLine($"Amount: {transfer.amount:C2}");
                    Console.WriteLine("--------------------------------------------");
                }
                return response.Data;
            }
            return null;
        }

        private string GetFromOrTo(int id)
        {
            if (id != UserService.GetUserId())
            {
                return "From:";
            }
            return "To:";
        }

        public string TransferFunds(int userId)
        {
            int accountFrom = 0;
            decimal accountFromBalance = 0;
            int accountTo = 0;
            decimal amount = 0;
            //Get the account you want to tranfer from
            RestRequest request = new RestRequest(API_BASE_URL + "account/"+ userId + "/list");
            IRestResponse<List<API_Account>> response = client.Get<List<API_Account>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Users\t\tAccounts");
                Console.WriteLine("   ID\t\t      ID\t\tName");
                Console.WriteLine("-------------------------------------------");
                List<API_Account> accountList = new List<API_Account>(response.Data);
                foreach (API_Account account in accountList)
                {
                    Console.WriteLine($"{account.UserId}\t\t\t{account.AccountId}\t\t{account.Username}");

                }

                Console.WriteLine("-------------------------------------------");
                List<int> validIDs = new List<int>();
                foreach(API_Account account in accountList)
                {
                    validIDs.Add(account.AccountId);
                }

                bool isValidId = false;
                while(isValidId == false)
                {
                    if (!validIDs.Contains(accountFrom))
                    {
                        Console.WriteLine("Enter the Account ID you want to transfer from: ");
                        try
                        {
                            accountFrom = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        { 
                        }
                    }
                    else
                    {
                        isValidId = true;
                    }
                }
                
                Console.WriteLine();
            }

            //Get the balance of account
            RestRequest requestBalance = new RestRequest(API_BASE_URL + "account/balance" + userId);
            IRestResponse<List<API_Account>> responseBalance = client.Get<List<API_Account>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                List<API_Account> account = new List<API_Account>(responseBalance.Data);
                accountFromBalance = account[0].Balance;
            }

            //Get account you want to tranfer to
            RestRequest request2 = new RestRequest(API_BASE_URL + "account/" + "listaccounts");
            IRestResponse<List<API_Account>> response2 = client.Get<List<API_Account>>(request2);
            if (response2.ResponseStatus != ResponseStatus.Completed)
            {
                ProcessErrorResponse(response2);
            }
            else
            {
                Console.WriteLine("-------------------------------------------");
                Console.WriteLine("Users\t\tAccounts");
                Console.WriteLine("   ID\t\t      ID\t\tName");
                Console.WriteLine("-------------------------------------------");

                List<int> accounts = new List<int>();
                foreach (API_Account account in response2.Data)
                {
                    Console.WriteLine($"{account.UserId}\t\t{account.AccountId}\t\t{account.Username}");
                    accounts.Add(account.AccountId);
                }
                Console.WriteLine("-------------------------------------------");

                bool isValidId = false;
                while (isValidId == false)
                {
                    if (!accounts.Contains(accountTo))
                    {
                        Console.WriteLine("Enter the Account ID you want to transfer to: ");
                        try
                        {
                            accountTo = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception)
                        {
                        }
                    }
                    else
                    {
                        isValidId = true;
                    }
                }
                Console.WriteLine();
            }

            //Get amount you want to transfer

            if (accountTo != 0)
            {
                bool validAmount = false;
                while (validAmount == false)
                {
                    Console.WriteLine("Enter valid transfer amount: ");
                    try
                    {
                        amount = Convert.ToDecimal(Console.ReadLine());
                        if (!(amount <= 0) && !(amount > accountFromBalance))
                        {
                            validAmount = true;
                        }
                    }
                    catch (Exception)
                    {

                    }
                }

                //Add funds and update DB 
                RestRequest request3 = new RestRequest(API_BASE_URL + "account/" + accountTo + "/addfunds/" + amount);
                IRestResponse response3 = client.Put(request3);
                if (response3.ResponseStatus != ResponseStatus.Completed)
                {
                    ProcessErrorResponse(response3);
                }
                else
                {
                    //return response3.Content;
                }

                //Subtract funds and update DB
                RestRequest request4 = new RestRequest(API_BASE_URL + "account/" + accountFrom + "/subtractfunds/" + amount);
                IRestResponse response4 = client.Put(request4);
                if (response3.ResponseStatus != ResponseStatus.Completed)
                {
                    ProcessErrorResponse(response4);
                }
                else
                {
                    //return response4.Content;
                }

                //Write transfer to DB
                RestRequest request5 = new RestRequest(API_BASE_URL + "transfer/" + accountFrom +"/"+amount+"/"+accountTo);
                IRestResponse response5 = client.Post(request5);
                if (response5.ResponseStatus != ResponseStatus.Completed)
                {
                    ProcessErrorResponse(response5);
                }
                else
                {
                    return response5.Content;
                }
            }
            return null;
        }

        private void ProcessErrorResponse(IRestResponse response)
        {
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                throw new HttpRequestException("Error occurred - unable to reach server.");
            }
            else if (!response.IsSuccessful)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    throw new HttpRequestException("Authorization is required for this endpoint.Please log in.");
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    throw new HttpRequestException("You do not have permission to perform the requested action");
                }
                else
                {
                    throw new HttpRequestException("Error occurred - received non-success response: " + (int)response.StatusCode);
                }
            }
        }

    }
}
