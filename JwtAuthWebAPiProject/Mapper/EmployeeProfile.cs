using AutoMapper;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.Mapper
{
    public class EmployeeProfile:Profile
    {
        public EmployeeProfile()
        {
            CreateMap<Employee,CreateEmployeeInputModel>().ReverseMap();
            CreateMap<Employee, UpdateEmployeeInputModel>().ReverseMap();
        }
    }
}
