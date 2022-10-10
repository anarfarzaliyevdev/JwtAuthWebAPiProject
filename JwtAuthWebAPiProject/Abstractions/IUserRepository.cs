using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.Abstractions
{
    public interface IUserRepository:IGenericRepository<User>
    {
     
        Task<User> GetUserByEmailAsync(string email);
      
    }
}
