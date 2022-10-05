using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;

namespace JwtAuthWebAPiProject.Abstractions
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAll();
        Task<Employee> GetById(int id);
        Task<Employee> Create(CreateEmployeeInputModel createEmployeeInputModel);
        Task<Employee> Update(Employee employee);
        Task<Employee> Delete(Employee employee);
        
    }
}
