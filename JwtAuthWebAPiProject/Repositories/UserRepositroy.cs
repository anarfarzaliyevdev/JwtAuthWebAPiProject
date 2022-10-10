using AutoMapper;
using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DbContexts;
using JwtAuthWebAPiProject.DTOs;

using JwtAuthWebAPiProject.Models;
using JwtAuthWebAPiProject.Services;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace JwtAuthWebAPiProject.Repositories
{
    public class UserRepositroy : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _appDbContext;
    

        public UserRepositroy(AppDbContext appDbContext)
            :base(appDbContext)
        {
            _appDbContext = appDbContext;
         
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
           return await _appDbContext.Users.Include(u => u.Permissions).FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
