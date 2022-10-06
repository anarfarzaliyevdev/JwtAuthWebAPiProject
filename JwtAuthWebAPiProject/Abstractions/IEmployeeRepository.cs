using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.Abstractions
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task<Employee> CreateAsync(CreateEmployeeInputModel createEmployeeInputModel);
        Task<Employee> UpdateAsync(UpdateEmployeeInputModel employee);
        Task<Employee> DeleteAsync(int employeeId);
        
    }
}
