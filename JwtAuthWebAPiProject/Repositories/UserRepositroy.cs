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
    public class UserRepositroy : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;

        public UserRepositroy(AppDbContext appDbContext, IMapper mapper,IAuthService authService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _authService = authService;
        }


        public async Task<User> CreateAsync(CreateUserInputModel createUserInputModel)
        {
            var user = _mapper.Map<User>(createUserInputModel);
            user.CreatedDate = DateTime.Now;
            user.IsActive = true;

            _authService.CreatePaswordHash(createUserInputModel.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            var newUser = (await _appDbContext.Users.AddAsync(user)).Entity;
            
         
            await _appDbContext.SaveChangesAsync();
            return newUser;
        }
        

        public async Task<User> GetUserAsync(string email)
        {
            var user=await _appDbContext.Users.Include(u => u.Permissions).FirstOrDefaultAsync(u=>u.Email==email);
            return user;
        }

        public async Task<List<User>> GetUsers()
        {

            return await _appDbContext.Users.Include(u => u.Permissions).ToListAsync();
        }
    }
}
