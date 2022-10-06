using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.Services
{
    public interface IAuthService
    {

        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
        string CreateToken(User user);
        void CreatePaswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    }
}
