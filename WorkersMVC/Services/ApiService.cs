using System.Net.Http.Headers;
using Newtonsoft.Json;
using WorkersMVC.Models;
namespace WorkersMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        private string _ApiURLPath = "http://localhost:5179/";

        public ApiService()
        {
            _httpClient = new HttpClient(); 
            _httpClient.BaseAddress = new Uri(_ApiURLPath);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PageResult<WorkerEntity>> GetAllWorkers(string Token, int pageSize, int pageNumber)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Workers?pageSize={pageSize}&pageNumber={pageNumber}");
            response.EnsureSuccessStatusCode();
            var contents = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<PageResult<WorkerEntity>>(contents);
            return apiResponse;
        }

        public async Task AddWorker(WorkerEntity worker, string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<WorkerEntity>($"api/Workers/AddWorker", worker);
            response.EnsureSuccessStatusCode();
        }

        public async Task<WorkerEntity> GetWorkerById(int Id, string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = await _httpClient.GetAsync($"api/Workers/GetWorkerById?Id={Id}");
            response.EnsureSuccessStatusCode();
            var contents = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonConvert.DeserializeObject<WorkerEntity>(contents);
            return apiResponse;
        }

        public async Task UpdateWorkerById(int Id, WorkerEntity worker, string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync<WorkerEntity>($"api/Workers/UpdateWorkerById?Id={Id}", worker);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteWorkerById(int Id, string Token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            HttpResponseMessage response = await _httpClient.PutAsync($"api/Workers/DeleteWorkerById?Id={Id}",null);
            response.EnsureSuccessStatusCode();
        }
    }
}
