using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.Abstractions
{
    public interface IAuthRepository
    {
        
        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(User user);
    }
}
