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
            try
            {

                return Ok(await _employeeRepository.GetAll());
            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from db. For details review logs");
            }
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetById(id);
                if (result == null)
                {
                    return NotFound(null);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
               
                return StatusCode(StatusCodes.Status500InternalServerError, "Error getting data from db. For details review logs");
            }
        }
        [PermissonCheck("Permission", "Create")]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeInputModel createEmployeeInputModel)
        {
            try
            {
                if (createEmployeeInputModel == null)
                {
                    return BadRequest();
                }


                var newEmployee = await _employeeRepository.Create(createEmployeeInputModel);

                return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.Id }, newEmployee);
            }
            catch (Exception ex)
            {
          
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data in db. For details review logs");
            }
        }
        [PermissonCheck("Permission", "Update")]
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(UpdateEmployeeInputModel updateEmployeeInputModel)
        {
            try
            {

                var employeeToUpdate = await _employeeRepository.GetById(updateEmployeeInputModel.Id);
                if (employeeToUpdate == null)
                {
                    return NotFound($"Employee with {updateEmployeeInputModel.Id} not found");
                }
                return await _employeeRepository.Update(updateEmployeeInputModel);
            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data in db. For details review logs");
            }
        }
        [PermissonCheck("Permission", "Delete")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeRepository.GetById(id);
                if (employee == null)
                {
                    return NotFound($"Employee with {id} not found");
                }
                return Ok(await _employeeRepository.Delete(id));
            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data in db. For details review logs");
            }
        }
    }
}
