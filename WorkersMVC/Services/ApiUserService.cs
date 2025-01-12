using Newtonsoft.Json;
using System.Net.Http.Headers;
using WorkersMVC.Model;

namespace WorkersMVC.Services
{
    public class ApiUserService
    {
        private readonly HttpClient _httpClient;
        private string _ApiURLPath = "http://localhost:5179/";

        public ApiUserService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_ApiURLPath);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<UserLoginResponse> AuthenticateUser(UserLoginRequest userDetails)
        {
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<UserLoginRequest>($"api/Users/UserLogin", userDetails);
            response.EnsureSuccessStatusCode();

            var contents = await response.Content.ReadAsStringAsync();
            var ApiResponse = JsonConvert.DeserializeObject<UserLoginResponse>(contents);
            return ApiResponse;
        }
    }
}
