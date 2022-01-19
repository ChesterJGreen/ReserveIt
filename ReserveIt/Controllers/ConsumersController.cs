using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserveIt.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Utilities.Extensions;
using ReserveIt.Models.Response;
using ReserveIt.Models.Request;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReserveIt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ConsumersController : ControllerBase
    {
        private ConsumerService _consumerService;
        
        public ConsumersController(Config.BaseControllerDependencies dependencies, 
            ConsumerService consumerService) :base(dependencies)
        {
           
            _consumerService = consumerService;
        }
        // GET: api/<ConsumersController>
        [HttpGet("me")]
        public async Task<IActionResult> Get()
        {
            int? userId = HttpContext.User.GetUserIdFromClaims();

            if (userId == null)
                return new NoContentResult();

            return new JsonResult(await _consumerService.GetByUserId((int)userId));
            var data = Mapper.Map<ConsumerDTO>(await _consumerService.GetByUserId((int)userId);
        }

        // GET api/<ConsumersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //get User Id from claims
            int? userId = HttpContext.User.GetUserIdFromClaims();
            if (userId == null)
                return new NoContentResult();
            
            return new JsonResult(await _consumerService.GetByUserId((int)userId));
        }

        // POST api/<ConsumersController>
        [HttpPost]
        [Authorize(Roles = "consuymer-editor")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ConsumersController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "consumer-editor")]

        public void Put(int id, [FromBody] string value)
        {
        }
        [HttpPut("me")]
        public async Task<IActionResult> Put([FromBody] ConsumerSelfUpdateRequest request)
        {
            int? userId = HttpContext.User.GetUserIdFromClaims();

            if (userId == null)
                return new NoContentResult();

            var errors = await HandleConsumerUpdate((int)userId, request);

            return new JsonResult(errors)
            {
                StatusCode = (errors.Length == 0) ? 200 : 400
            };
        }
        private async Task<object[]> HandleConsumerUpdate(int userId, ConsumerSelfUpdateRequest request)
        {
            List<string> errors = new List<string>();

            bool usernameHasData = false == string.IsNullOrWhiteSpace(request.Username);
            if (usernameHasData)
            {
                if (false == await _consumerService.UpdateUserName(userId, request.Username))
                    errors.Add("username could not be updateed");
            }
            bool nameHasData = (false == string.IsNullOrWhiteSpace(request.FirstName)) &&
                (false == String.IsNullOrWhiteSpace(request.LastName));

            if (nameHasData)
            {
                if (false == await _consumerService.UpdateFirstLastName(userId, request.FirstName, request.LastName))
                    errors.Add("first and last name could not be updated");
            }
            return errors.Select(error => new { errorMessage = error }).ToArray();
        }

        // DELETE api/<ConsumersController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "consumer-editor")]
        public void Delete(int id)
        {
        }
    }
}
