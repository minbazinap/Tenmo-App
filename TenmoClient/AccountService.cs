using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using RestSharp;
using TenmoClient.Data;

namespace TenmoClient
{
    public class AccountService
    {
        private readonly static string API_BASE_URL = "https://localhost:44315/";
        private readonly IRestClient client = new RestClient();
        

        public List<decimal> GetBalances(int userId)
        {
            RestRequest request = new RestRequest(API_BASE_URL + "account/balance/" + userId);
            IRestResponse<List<decimal>> response = client.Get<List<decimal>>(request);
            if(response.ResponseStatus!= ResponseStatus.Completed)
            {
                ProcessErrorResponse(response);
            }
            else 
            {
                return response.Data;
            }
            return null;
        }

        public List<API_Account> ListAccounts()
        {
            RestRequest request = new RestRequest(API_BASE_URL + "listaccounts");
            IRestResponse<List<API_Account>> response = client.Get<List<API_Account>>(request);
            if (response.ResponseStatus != ResponseStatus.Completed)
            {
                ProcessErrorResponse(response);
            }
            else
            {
                return response.Data;
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
