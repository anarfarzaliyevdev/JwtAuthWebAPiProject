using AutoMapper;
using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DbContexts;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace JwtAuthWebAPiProject.Repositories
{
    public class UserRepositroy : IUserRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public UserRepositroy(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }


        public async Task<User> Create(CreateUserInputModel createUserInputModel)
        {
            var user = _mapper.Map<User>(createUserInputModel);
            user.CreatedDate = DateTime.Now;
            user.IsActive = true;
            var newUser = (await _appDbContext.Users.AddAsync(user)).Entity;
            CreatePaswordHash(createUserInputModel.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            await _appDbContext.SaveChangesAsync();
            return newUser;
        }
        public void CreatePaswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<User> GetUser(string email)
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
