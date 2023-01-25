using JaniceChat.Api.Models;
using JaniceChat.Domain;
using JaniceChat.Repository.Abstraction;
using JaniceChat.Service.Abstraction.Services;
using JaniceChat.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JaniceChat.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _userService;

        public UsersController(IUserRepository repository, IUserService userService)
        {
            _repository = repository;
            _userService = userService;
        }

        [HttpPost("authorize")]
        public async Task<IActionResult> Authorize([FromBody] UserModel request)
        {
            var user = await _repository.GetUserByUserName(request.UserName);

            if (user == null) 
                return Unauthorized();

            var token = GenerateToken(user);

            return Ok(new AuthorizeModel
            {
                UserName = user.UserName,
                Token = token
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserModel request)
        {
            try
            {
                var user = await _userService.Create(request.UserName);
                return Created($"/users/{user.Id}", user);
            }
            catch (UserServiceException ex) // ideally this should be done in a middleware
            {
                return BadRequest(new { ex.Message });
            }
        }

        private string GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("fedaf7d8863b48e197b9287d492b708e");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}