using JwtAuthWebAPiProject.Abstractions;
using JwtAuthWebAPiProject.CustomAttributes;
using JwtAuthWebAPiProject.DTOs;
using JwtAuthWebAPiProject.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace JwtAuthWebAPiProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
       
        [HttpGet]
        public async Task<ActionResult<List<Employee>>>GetAll()
        {

       
                return Ok(await _employeeRepository.GetAllAsync());
          
           
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            
                var result = await _employeeRepository.GetByIdAsync(id);
                if (result == null)
                {
                    return NotFound(null);
                }
                return Ok(result);
           
        }
        [PermissonCheck("Permission", "Create")]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeInputModel createEmployeeInputModel)
        {
            
                if (createEmployeeInputModel == null)
                {
                    return BadRequest();
                }

                var newEmployee = await _employeeRepository.CreateAsync(createEmployeeInputModel);

                return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.Id }, newEmployee);
            
            
        }

        [PermissonCheck("Permission", "Update")]
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(UpdateEmployeeInputModel updateEmployeeInputModel)
        {
          

                var employeeToUpdate = await _employeeRepository.GetByIdAsync(updateEmployeeInputModel.Id);
                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with {updateEmployeeInputModel.Id} not found");
                }
                return await _employeeRepository.UpdateAsync(updateEmployeeInputModel);
            
        }
        [PermissonCheck("Permission", "Delete")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            
                var employee = await _employeeRepository.GetByIdAsync(id);
                if (employee == null)
                {
                    return NotFound($"Employee with {id} not found");
                }
                return Ok(await _employeeRepository.DeleteAsync(id));
           
            
        }
    }
}
