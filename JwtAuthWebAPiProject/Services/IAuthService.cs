using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
using System.Security.Claims;

namespace JwtAuthWebAPiProject.Services
{
    public interface IAuthService
    {

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        TokenOutputModel CreateToken(User user);
        void CreatePaswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
        string GenerateRefreshToken();
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
    }
}
