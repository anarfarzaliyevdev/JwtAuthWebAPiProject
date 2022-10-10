using AutoMapper;
using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DbContexts;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace JwtAuthWebAPiProject.Repositories
{
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;
        public EmployeeRepository(AppDbContext appDbContext)
              : base(appDbContext)
        {
            _appDbContext = appDbContext;
        }

    }
}
