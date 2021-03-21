using AKSoftware.WebApi.Client;
using Newtonsoft.Json;
using SharedLibrary.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SharedLibrary.Services
{
    public class AuthenticationService
    {
        private readonly string _baseUrl;
        //public ILocalStorageService _localStorageService { get; }
        HttpClient _httpClient = new HttpClient();

        ServiceClient client = new ServiceClient();

        public AuthenticationService(string url)
        {
            _baseUrl = url;
            
        }

        public async Task<SignupResponse> SignupUserAsync(SignUpModel request)
        {
            var response = await client.PostAsync<SignupResponse>($"{_baseUrl}/api/users/signup", request);

            if (response.IsSucceded)
                return new SignupResponse { Succeeded = true, Message = "The user is registerd (signupUserAsync)"};

            return new SignupResponse { Succeeded = false, Message = "Registration is failed (signupUserAsync)" };

        }



        public async Task<SignInResponse> SigninUserAsync(SignInModel request)
        {
            
            var response = await client.PostAsync<SignInResponse>($"{_baseUrl}/api/users/signin", request);

            return response.Result;     
        }



        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync(string requestUri, string token)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

     //       var token = await _localStorageService.GetItemAsync<string>("token");

            requestMessage.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (true)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<UserResponse>>(responseBody));
            }

            //return null;
        }



        public async Task<IEnumerable<CustomerResponseModel>> GetAllCustomersAsync(string requestUri)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (response != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<CustomerResponseModel>>(responseBody));
            }

            return null;
        }



        public async Task<SignupResponse> SignupCustomerAsync(CreateCustomerModel request)
        {
            var response = await client.PostAsync<SignupResponse>($"{_baseUrl}/api/users/customers", request);

            if (response.IsSucceded)
                return new SignupResponse { Succeeded = true, Message = "The customer is registerd (signupCustomerAsync)" };

            return new SignupResponse { Succeeded = false, Message = "Registration is failed (signupCustomerAsync)" };

        }



        public async Task<CreateIssueResponseModel> CreateIssueRazorAsync(CreateIssueModel request)
        {
            var response = await client.PostAsync<CreateIssueResponseModel>($"{_baseUrl}/api/issues/create", request);

            if (response.IsSucceded)
                return new CreateIssueResponseModel { Succeeded = true, Message = "The issue is created", CustomerId = request.CustomerId };

            return new CreateIssueResponseModel { Succeeded = false, Message = "The issue is NOT created" };

        }


        public async Task<CreateIssueResponseModel> EditIssueRazorAsync(CreateIssueModel request, int id)
        {
            var response = await client.PostAsync<CreateIssueResponseModel>($"{_baseUrl}/api/issues/edit/{id}", request);

            if (response.IsSucceded)
                return new CreateIssueResponseModel { Succeeded = true, Message = "The issue is updated"};

            return new CreateIssueResponseModel { Succeeded = false, Message = "The issue is NOT updated" };

        }




        public async Task<CreateCommentModel> CreateCommentRazorAsync(CreateCommentModel request)
        {
            var response = await client.PostAsync<CreateCommentModel>($"{_baseUrl}/api/comments/create", request);

            if (response.IsSucceded)
                return new CreateCommentModel { Succeeded = true};

            return new CreateCommentModel { Succeeded = false};

        }



        public async Task<IEnumerable<CreateIssueModel>> GetAllIssuesAsync(string uri, int customerId)
        {
            string requestUri = $"{uri}/{customerId}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (response != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<CreateIssueModel>>(responseBody));
            }

            return null;
        }



        public async Task<IEnumerable<CreateCommentModel>> GetAllCommentsAsync(string uri, int issueId)
        {
            string requestUri = $"{uri}/{issueId}";

            var requestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await _httpClient.SendAsync(requestMessage);

            var responseStatusCode = response.StatusCode;

            if (response != null)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return await Task.FromResult(JsonConvert.DeserializeObject<IEnumerable<CreateCommentModel>>(responseBody));
            }

            return null;
        }





        //-------------------------------------------
    }
}
