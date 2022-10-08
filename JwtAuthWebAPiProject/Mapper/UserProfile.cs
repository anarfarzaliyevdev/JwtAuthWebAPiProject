using AutoMapper;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.Mapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, CreateUserInputModel>().ReverseMap();
            CreateMap<User, User>().ReverseMap();
        }
    }
}
