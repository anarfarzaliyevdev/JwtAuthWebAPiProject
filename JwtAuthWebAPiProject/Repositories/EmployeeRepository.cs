using AutoMapper;
using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.DbContexts;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;

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
        public async Task<Employee> Create(CreateEmployeeInputModel createEmployeeInputModel)
        {
            var employee = _mapper.Map<Employee>(createEmployeeInputModel);
            employee.CreatedDate = DateTime.Now;
            employee.IsDeleted = false;
            employee.ModifiedDate = DateTime.Now;
            var newEmployee = (await _appDbContext.Employees.AddAsync(employee)).Entity;

            await _appDbContext.SaveChangesAsync();



            return newEmployee;
        }


        public Task<Employee> Delete(Employee employee)
        {
            throw new NotImplementedException();
        }

        public Task<List<Employee>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Employee> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Employee> Update(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
