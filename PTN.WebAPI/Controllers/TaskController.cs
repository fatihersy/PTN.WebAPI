using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTN.WebAPI.Auth;
using PTN.WebAPI.DataAccess;
using PTN.WebAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace PTN.WebAPI.Controllers
{
    [Controller]
    [Authorize]
    [Route("/")]
    public class TaskController : Controller
    {
        private readonly MongoDbService _mongoDbService;

        public TaskController(MongoDbService mongoDbService)
        {
            _mongoDbService = mongoDbService;
        }

        [HttpGet]
        [AllowAnonymous]
        public string Get(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var handler = new JwtSecurityTokenHandler().WriteToken(new JwtAuth().GetToken(claims));

            return handler;

            //return _mongoDbService.getTasks();
        }

        [HttpGet("ValidateToken")]
        [AllowAnonymous]
        public bool Validate(string token)
        {
            var isValid = new JwtAuth().ValidateToken(token);

            if (isValid != null) return true;

            return false;
        }

        [HttpPost]
        public void Create([FromBody] Tasks tasks)
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
