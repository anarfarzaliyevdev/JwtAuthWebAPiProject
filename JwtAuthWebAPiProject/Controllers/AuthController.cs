using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
using JwtAuthWebAPiProject.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.IdentityModel.Tokens.Jwt;

namespace JwtAuthWebAPiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IMemoryCache _memoryCache;


        public AuthController(IUserRepository userRepository,IAuthService authService,IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _authService = authService;
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
            if (!_authService.VerifyPasswordHash(loginInputModel.Password, user.PasswordHash, user.PasswordSalt))
            {
                return NotFound("Username or password is not correct");
            }

            TokenOutputModel tokenOutputModel = _authService.CreateToken(user);
            user.RefreshToken = tokenOutputModel.RefreshToken;
            user.RefreshTokenExpireDate = tokenOutputModel.RefreshTokenExpireDate;
            await _userRepository.UpdateUserAsync(user);
            _memoryCache.Set("UserEmail", user.Email);
            //_memoryCache.Set("Permissions", user.Permissions);
            return Ok(tokenOutputModel);
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<ActionResult<TokenOutputModel>> RefreshToken(RefreshTokenInputModel refreshTokenInputModel)
        {
            if (refreshTokenInputModel is null)
            {
                return BadRequest("Invalid client request");
            }

            string? accessToken = refreshTokenInputModel.AccessToken;
            string? refreshToken = refreshTokenInputModel.RefreshToken;

            var principal = _authService.GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return BadRequest("Invalid access token or refresh token");
            }


             _memoryCache.TryGetValue("UserEmail", out string username);


            var user = await _userRepository.GetUserAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpireDate<= DateTime.Now)
            {
                return BadRequest("Invalid access token or refresh token");
            }

            var newTokenOutputModel = _authService.CreateToken(user);
            user.RefreshToken = newTokenOutputModel.RefreshToken;
            user.RefreshTokenExpireDate = newTokenOutputModel.RefreshTokenExpireDate;

            await _userRepository.UpdateUserAsync(user);

            return Ok(newTokenOutputModel);
           

           
        }

    }
}
