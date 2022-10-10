using AutoMapper;
using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
using JwtAuthWebAPiProject.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthWebAPiProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<UserOutputModel>> GetUsers()
        {
            var users = await _userRepository.GetAllAsync();
            //create employee output model to view
            var userOutputModel = new UserOutputModel();
            var userOutputModels = users.Select(u => _mapper.Map(u, userOutputModel)).ToList();
            _mapper.Map(users, userOutputModels);
            return Ok(userOutputModels);
        }

        [HttpPost]
        public async Task<ActionResult<UserOutputModel>> Create(CreateUserInputModel createUserInputModel)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(createUserInputModel.Email);
            if (existingUser != null)
            {
                return BadRequest($"User with this email already exists ({createUserInputModel.Email}) ");
            }

            User user = new User();
            _mapper.Map(createUserInputModel, user);

            //output model for created user
            var createdUser= await _userRepository.CreateAsync(user);
            var userInputModel = new UserOutputModel();
            _mapper.Map(createdUser, userInputModel);
            return Ok(userInputModel);


        }


    }
}
