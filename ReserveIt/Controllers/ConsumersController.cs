using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserveIt.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ReserveIt.Utilities.Extensions;
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ConsumersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ConsumersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
