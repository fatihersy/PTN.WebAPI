using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using PTN.WebAPI.DataAccess;
using PTN.WebAPI.Models;

namespace PTN.WebAPI.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class TaskController : Controller
    {
        private readonly MongoDbService _mongoDbService;

        public TaskController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        // GET: TaskController
        [HttpGet]
        public List<Tasks> Get()
        {
            return _mongoDbService.getTasks();
        }

        // POST: TaskController/Create
        [HttpPost]
        public void Create([FromBody]Tasks tasks)
        {
            _mongoDbService.AddTask(tasks);
        }

        [HttpPut]
        public void Update([FromBody] Tasks tasks)
        {
            _mongoDbService.UpdateTask(tasks);
        }

        [HttpDelete]
        public void Delete([FromBody] string id)
        {
            _mongoDbService.DeleteTask(new Tasks { Id = id });
        }
    }
}
