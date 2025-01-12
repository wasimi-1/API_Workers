using API_Workers.Data;
using API_Workers.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace API_Workers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorkersController : ControllerBase
    {
        private ApplicationDbContext _db;
        private readonly ILogger<WorkersController> _logger;

        public WorkersController(ApplicationDbContext db, ILogger<WorkersController> logger = null)
        {
            _db = db;
            _logger = logger;
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Worker")]
        public IActionResult GetAllWorkers(int pageSize, int pageNumber)
        {
            _logger.LogInformation("Getting all workers list");
            int totalCount = _db.Workers.Count();
            var workerList = _db.Workers.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
            var result = new PageResult<WorkerEntity>
            {
                items = workerList,
                CurrentPage = pageNumber,
                TotalCount = totalCount,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

            return Ok(result);
        }

        [HttpGet("GetWorkerById")]
        [Authorize(Roles = "Admin,Worker")]
        public ActionResult<WorkerEntity> GetWorkerById(int id)
        {
            _logger.LogInformation("Getting worker by ID : " + id);
            if(id <= 0)
            {
                _logger.LogError("Provided ID is invalid (it should be more than 0");
                return BadRequest("Provided ID is invalid (it should be more than 0");
            }

            var worker = _db.Workers.FirstOrDefault(
                x=> x.Id == id);

            if(worker == null)
            {
                _logger.LogError("Could NOT find worker by provided ID");
                return NotFound("Could NOT find worker by provided ID");
            }
            _logger.LogInformation("Getting worker by ID success");
            return Ok(worker);
        }

        [HttpPost("AddWorker")]
        [Authorize(Roles = "Admin")]
        public ActionResult<WorkerEntity> AddWorker([FromBody]WorkerEntity worker)
        {
            _logger.LogInformation("Adding worker started");
            if(!ModelState.IsValid)
            {
                _logger.LogError("Bad Request. Model State is invalid");
                return BadRequest(ModelState);
            }

            _db.Workers.Add(worker);
            _db.SaveChanges();
            _logger.LogInformation("Adding new worker success");
            return Ok(worker);
        }

        [HttpPost("UpdateWorkerById")]
        [Authorize(Roles = "Admin")]
        public ActionResult<WorkerEntity> UpdateWorker(int id, [FromBody] WorkerEntity worker)
        {
            _logger.LogInformation("Update worker by ID started");
            if (worker==null)
            {
                _logger.LogError("Bad Request. Worker is Null");
                return BadRequest(worker);
            }

            var Worker=_db.Workers.FirstOrDefault(
                x=> x.Id == id);

            if (Worker == null)
            {
                _logger.LogError("Could NOT find worker by provided ID");
                return NotFound("Could NOT find worker by provided ID");
            }
                
            Worker.FirstName = worker.FirstName;
            Worker.LastName = worker.LastName;
            Worker.Title = worker.Title;
            Worker.Salary = worker.Salary;
            Worker.EmailAddress = worker.EmailAddress;

            _db.SaveChanges();
            _logger.LogInformation("Updating worker details success");
            return Ok(worker);
        }

        [HttpPut("DeleteWorkerById")]
        [Authorize(Roles = "Admin")]
        public ActionResult<WorkerEntity> DeleteWorker(int id)
        {
            _logger.LogInformation("Deleting worker by ID started");
            var Worker = _db.Workers.FirstOrDefault(
                x => x.Id == id);

            if (Worker == null)
            {
                _logger.LogError("Could NOT find worker with provided ID");
                return NotFound("Could NOT find worker with provided ID");
            }

            _db.Workers.Remove(Worker);
            _db.SaveChanges();

            _logger.LogInformation("Deleting worker success");
            return Ok("Deleted worker by ID: " + id);
        }

    }
}
