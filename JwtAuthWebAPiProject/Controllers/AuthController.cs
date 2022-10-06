using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
using JwtAuthWebAPiProject.Services;
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
        private readonly IAuthService _authRepository;
        private readonly IMemoryCache _memoryCache;

        public AuthController(IUserRepository userRepository,IAuthService authRepository,IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _authRepository = authRepository;
            _memoryCache = memoryCache;
        }
        [HttpPost]
        public async Task<ActionResult<TokenOutputModel>> Login(LoginInputModel loginInputModel)
        {
            var user =await _userRepository.GetUserAsync(loginInputModel.Email);
          
            if(user == null)
            {
                return NotFound("Username or password is not correct");
            }
            if (!_authRepository.VerifyPasswordHash(loginInputModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                return NotFound("Username or password is not correct");
            }

            TokenOutputModel token = _authRepository.CreateToken(user);
            
            //_memoryCache.Set("UserEmail", user.Email);
            //_memoryCache.Set("Permissions", user.Permissions);
            return Ok(token);
        }
    }
}
