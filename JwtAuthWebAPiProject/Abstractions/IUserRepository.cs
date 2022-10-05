using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.Abstractions
{
    public interface IUserRepository
    {
        Task<User> Create(CreateUserInputModel createUserInputModel);
        Task<User> GetUser(string email);
        Task<List<User>> GetUsers();
    }
}
