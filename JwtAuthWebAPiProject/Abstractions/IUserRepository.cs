using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.Abstractions
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(CreateUserInputModel createUserInputModel);
        Task<User> GetUserAsync(string email);
        Task<List<User>> GetUsersAsync();
    }
}
