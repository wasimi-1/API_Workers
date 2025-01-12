using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using WorkersMVC.Models;
using WorkersMVC.Services;
using Microsoft.Extensions.Configuration;

namespace WorkersMVC.Controllers
{
    public class WorkersController : Controller
    {
        private readonly ApiService _apiService;
        private readonly IConfiguration _config;
        //private string BaseApiUrl = "http://localhost:5179/";

        public WorkersController(IConfiguration config)
        {
            _apiService = new ApiService();
            _config = config;
        }
        [Authorize(Roles ="Admin,Worker")]
        public async Task <IActionResult> Index(int pageNumber=1)
        {
            //After ApiService created
            int pageSize = _config.GetValue<int>("PageSettings:PageSize");
            PageResult<WorkerEntity> workersList = new PageResult<WorkerEntity>();
            workersList = await _apiService.GetAllWorkers(HttpContext.Session.GetString("ApiToken"), pageSize,pageNumber);
            return View(workersList);

            //Before ApiService created
            //using (var _httpClient = new HttpClient())
            //{
            //    _httpClient.BaseAddress = new Uri(BaseApiUrl + "api/Workers");
            //    _httpClient.DefaultRequestHeaders.Accept.Clear();
            //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage getData = await _httpClient.GetAsync("");
            //    if (getData.IsSuccessStatusCode)
            //    {
            //        string result = getData.Content.ReadAsStringAsync().Result;
            //        workersList = JsonConvert.DeserializeObject<List<WorkerEntity>>(result);
            //    }
            //    else
            //    {
            //        return View("ErrorPage");
            //    }
            //}
            //return View(workersList);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateWorker(WorkerEntity workerEntity)
        {
            //After ApiService created
            await _apiService.AddWorker(workerEntity, HttpContext.Session.GetString("ApiToken"));
            return RedirectToAction("Index");

            //Before ApiService created
            //using (var _httpClient = new HttpClient())
            //{
            //    _httpClient.BaseAddress = new Uri(BaseApiUrl + "api/Workers/");
            //    _httpClient.DefaultRequestHeaders.Accept.Clear();
            //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage getData = await _httpClient.PostAsJsonAsync("AddWorker", workerEntity);
            //    if (getData.IsSuccessStatusCode)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return View("ErrorPage");
            //    }
            //}
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Details(int Id)
        {
            //After ApiService created
            WorkerEntity workerDetails = new WorkerEntity();
            workerDetails = await _apiService.GetWorkerById(Id, HttpContext.Session.GetString("ApiToken"));
            return View(workerDetails);

            //Before ApiService created
            //using (var _httpClient = new HttpClient())
            //{
            //    _httpClient.BaseAddress = new Uri(BaseApiUrl + "api/Workers/");
            //    _httpClient.DefaultRequestHeaders.Accept.Clear();
            //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage getData = await _httpClient.GetAsync($"GetWorkerById?Id={Id}");
            //    if (getData.IsSuccessStatusCode)
            //    {
            //        string result = getData.Content.ReadAsStringAsync().Result;
            //        workerDetails = JsonConvert.DeserializeObject<WorkerEntity>(result);
            //    }
            //    else
            //    {
            //        return View("ErrorPage");
            //    }
            //}
            //return View(workerDetails);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateWorkerDetails(int Id, WorkerEntity workerEntity)
        {
            //After ApiService created
            await _apiService.UpdateWorkerById(Id, workerEntity, HttpContext.Session.GetString("ApiToken"));
            return RedirectToAction("Index");

            //Before ApiService created
            //using (var _httpClient = new HttpClient())
            //{
            //    _httpClient.BaseAddress = new Uri(BaseApiUrl + "api/Workers/");
            //    _httpClient.DefaultRequestHeaders.Accept.Clear();
            //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage getData = await _httpClient.PostAsJsonAsync($"UpdateWorkerById?Id={Id}", workerEntity);
            //    if (getData.IsSuccessStatusCode)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return View("ErrorPage");
            //    }
            //}
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int Id)
        {
            //After ApiService created
            WorkerEntity workerDetails = new WorkerEntity();
            workerDetails = await _apiService.GetWorkerById(Id, HttpContext.Session.GetString("ApiToken"));
            return View(workerDetails);

            //Before ApiService created
            //using (var _httpClient = new HttpClient())
            //{
            //    _httpClient.BaseAddress = new Uri(BaseApiUrl + "api/Workers/");
            //    _httpClient.DefaultRequestHeaders.Accept.Clear();
            //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage getData = await _httpClient.GetAsync($"GetWorkerById?Id={Id}");
            //    if (getData.IsSuccessStatusCode)
            //    {
            //        string result = getData.Content.ReadAsStringAsync().Result;
            //        workerDetails = JsonConvert.DeserializeObject<WorkerEntity>(result);
            //    }
            //    else
            //    {
            //        return View("ErrorPage");
            //    }
            //}
            //return View(workerDetails);
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteWorker(int Id, WorkerEntity workerEntity)
        {
            //After ApiService created
            await _apiService.DeleteWorkerById(Id, HttpContext.Session.GetString("ApiToken"));
            return RedirectToAction("Index");

            //Before ApiService created
            //using (var _httpClient = new HttpClient())
            //{
            //    _httpClient.BaseAddress = new Uri(BaseApiUrl + "api/Workers/");
            //    _httpClient.DefaultRequestHeaders.Accept.Clear();
            //    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            //    HttpResponseMessage getData = await _httpClient.PutAsync($"DeleteWorkerById?Id={Id}", null);
            //    if (getData.IsSuccessStatusCode)
            //    {
            //        return RedirectToAction("Index");
            //    }
            //    else
            //    {
            //        return View("ErrorPage");
            //    }
            //}
        }

        public IActionResult ErrorPage()
        {
            return View();
        }

    }
}
