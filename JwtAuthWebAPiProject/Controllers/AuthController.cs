using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace JwtAuthWebAPiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IMemoryCache _memoryCache;

        public AuthController(IUserRepository userRepository,IAuthRepository authRepository,IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _memoryCache = memoryCache;
        }
        [HttpPost]
        public async Task<ActionResult<string>> Login(LoginInputModel loginInputModel)
        {
            var user =await _userRepository.GetUser(loginInputModel.Email);
          
            if(user == null)
            {
                return BadRequest("User not exist");
            }
            if (!_authRepository.VerifyPasswordHash(loginInputModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                return BadRequest("Bad request");
            }

            string token = _authRepository.CreateToken(user);
            
            _memoryCache.Set("UserEmail", user.Email);
            _memoryCache.Set("Permissions", user.Permissions);
            return Ok(token);
        }
    }
}
