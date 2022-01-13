using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReserveIt.Managers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Text;
using ReserveIt.Utilities.ConfigModels;
using ReserveIt.Utilities.Error;
using Microsoft.IdentityModel.Tokens;
using ReserveIt.Models.Request;
using ReserveIt.Models.Response;
using ReserveIt.Models;

namespace ReserveIt.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "registered-role")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        private readonly AuthTokenSettings _tokenSettings;

        public UsersController(Config.BaseControllerDependencies dependencies, IUserService userService) : base(dependencies)
        {
            // the dependency injection container sees that you need something that implements IUserService - you are asking for IUserService
            // .. the container has the instructions from AddScoped<IUserService, UserService> that it should instantiate the UserService class
            // .. any time it is asked to fulfill the IUserService dependency
            // it runs: IUserService userService = new UserService();
            _userService = userService;
            _tokenSettings = AppConfig.AuthTokenSettings;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] UserAuthenticationRequest model)
        {
            var user = await _userService.Authenticate(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            var buildUserClaims = _userService.BuildUserClaims(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = System.Text.Encoding.ASCII.GetBytes(_tokenSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return Ok(new
            {
                Id = user.Id,
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = tokenString
            });
        }
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserAddUpdateRequest model)
        {
            if (!ModelState.IsValid)
            {
                StringBuilder builder = new StringBuilder();

                var errors = ModelState.Values.Where(v => v.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                    .SelectMany(v => v.Errors.ToArray());

                foreach (var issue in errors)
                    builder.AppendLine(issue.ErrorMessage);

                return BadRequest(new { message = builder.ToString() });
            }

            try { await _userService.Create(model);
                return Ok();
            }
            catch (ServiceBadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllReadOnly();
            var model = Mapper.Map<IList<UserResponseModel>>(users);
            return Ok(model);
        }
            
            [HttpGet("{id}")]
            public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetById(id);
            var model = Mapper.Map<UserResponseModel>(user);
            return Ok(model);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "res-admin")]

        public async Task<IActionResult> Update(int id, [FromBody] UserAddUpdateRequest model)
        {
            var user = Mapper.Map<User>(model);
            user.Id = id;

            try
            {
                await _userService.Update(user, model.Password);
                return Ok();
            }
            catch (ServiceBadRequestException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }

    }
}
