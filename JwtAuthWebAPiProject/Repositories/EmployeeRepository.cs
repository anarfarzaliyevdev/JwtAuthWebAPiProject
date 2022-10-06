using AutoMapper;
using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DbContexts;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace JwtAuthWebAPiProject.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public EmployeeRepository(AppDbContext appDbContext,IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<Employee> CreateAsync(CreateEmployeeInputModel createEmployeeInputModel)
        {
            var employee = _mapper.Map<Employee>(createEmployeeInputModel);
            employee.CreatedDate = DateTime.Now;
            employee.IsDeleted = false;
            employee.ModifiedDate = DateTime.Now;
            var newEmployee = (await _appDbContext.Employees.AddAsync(employee)).Entity;

            await _appDbContext.SaveChangesAsync();



            return newEmployee;
        }


        public async Task<Employee> DeleteAsync(int employeeId)
        {
            var employee = await _appDbContext.Employees
                   .FirstOrDefaultAsync(e=>e.Id==employeeId);
            if (employee != null)
            {
                employee.IsDeleted = true;
                await _appDbContext.SaveChangesAsync();
                return employee;
            }
            return null;
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _appDbContext.Employees.ToListAsync();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _appDbContext.Employees
                  .FirstOrDefaultAsync(e =>e.Id == id);
        }

        public async Task<Employee> UpdateAsync(UpdateEmployeeInputModel updateEmployeeInputModel)
        {
            var employee = await _appDbContext.Employees
                .FirstOrDefaultAsync(e =>e.Id == updateEmployeeInputModel.Id);
            if (employee != null)
            {
              
               _mapper.Map(updateEmployeeInputModel,employee);
                employee.ModifiedDate = DateTime.Now;
                await _appDbContext.SaveChangesAsync();

                return employee;
            }
            return null;
        }
    }
}
