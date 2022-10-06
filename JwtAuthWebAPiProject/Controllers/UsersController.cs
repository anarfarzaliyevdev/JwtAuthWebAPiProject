using AutoMapper;
using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
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

        public UsersController(IUserRepository userRepository,IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<User>> GetUsers()
        {


            return Ok(await _userRepository.GetUsers());
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(CreateUserInputModel createUserInputModel)
        {

            var existingUser = await _userRepository.GetUser(createUserInputModel.Email);
            if(existingUser != null)
            {
                return BadRequest($"User with this email already exists ({createUserInputModel.Email}) ");
            }
            return Ok(await _userRepository.Create(createUserInputModel));
        }
        

    }
}
