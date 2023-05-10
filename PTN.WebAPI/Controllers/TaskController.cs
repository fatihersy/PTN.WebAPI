using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTN.WebAPI.Auth;
using PTN.WebAPI.DataAccess;
using PTN.WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PTN.WebAPI.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Controller]
    [Authorize]
    [Route("/")]
    public class TaskController : Controller
    {
        private readonly MongoDbService _mongoDbService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mongoDbService"></param>
        public TaskController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        /// <summary>
        /// Get a token
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public string GetToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var handler = new JwtSecurityTokenHandler().WriteToken(new JwtAuth().GetToken(claims));

            return handler;
        }

        /// <summary>
        /// Get the tasks
        /// </summary>
        /// <returns></returns>
        [HttpGet("/get")]
        public List<Tasks> GetTasks()
        {
            return _mongoDbService.getTasks();
        }

        /// <summary>
        /// Development specific method for testing tokens
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("ValidateToken")]
        [AllowAnonymous]
        public bool Validate(string token)
        {
            var isValid = new JwtAuth().ValidateToken(token);

            if (isValid != null) return true;

            return false;
        }

        /// <summary>
        /// For creating tasks
        /// </summary>
        /// <param name="tasks"></param>
        [HttpPost]
        public void Create([FromBody] Tasks tasks)
        {
            _mongoDbService.AddTask(tasks);
        }

        /// <summary>
        /// For updating tasks
        /// </summary>
        /// <param name="tasks"></param>
        [HttpPut]
        public void Update([FromBody] Tasks tasks)
        {
            _mongoDbService.UpdateTask(tasks);
        }

        /// <summary>
        /// For deleting tasks
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public void Delete([FromBody] string id)
        {
            _mongoDbService.DeleteTask(new Tasks { Id = id });
        }
    }
}
