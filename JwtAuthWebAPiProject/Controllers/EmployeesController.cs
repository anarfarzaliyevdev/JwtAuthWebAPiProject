using AutoMapper;
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
        private readonly IMapper _mapper;

        public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<EmployeeOutputModel>>> GetAll()
        {
            var users = await _employeeRepository.GetAllAsync();
            //create employee output model to view
            var employeeOutputModel = new EmployeeOutputModel();
            var employeeOutputModels = users.Select(u=>_mapper.Map(u, employeeOutputModel)).ToList();
            _mapper.Map(users, employeeOutputModels);
            return Ok(employeeOutputModels);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var user = await _employeeRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound(null);
            }
            var employeeOutputModel = new EmployeeOutputModel();
            var result = _mapper.Map(user, employeeOutputModel);
           
            return Ok(result);
        }

        [PermissonCheck("CreateEmployee")]
        [HttpPost]
        public async Task<ActionResult<EmployeeOutputModel>> CreateEmployee(CreateEmployeeInputModel createEmployeeInputModel)
        {
            if (createEmployeeInputModel == null)
            {
                return BadRequest();
            }
            var employee = new Employee();
            _mapper.Map(createEmployeeInputModel, employee);
            var newEmployee = await _employeeRepository.CreateAsync(employee);
            //create employee output model to view
            var employeeOutputModel =new EmployeeOutputModel();
            _mapper.Map(newEmployee, employeeOutputModel);
            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.Id }, employeeOutputModel);
        }

        [PermissonCheck("UpdateEmployee")]
        [HttpPut]
        public async Task<ActionResult<EmployeeOutputModel>> UpdateEmployee(UpdateEmployeeInputModel updateEmployeeInputModel)
        {
            if (updateEmployeeInputModel == null)
            {
                return NotFound($"Employee with given data not found");
            }
            var employee = new Employee();
            _mapper.Map(updateEmployeeInputModel, employee);
            await _employeeRepository.UpdateAsync(employee);
            var updatedEmployee= await _employeeRepository.GetByIdAsync(employee.Id);
            //create employee output model to view
            var employeeOutputModel = new EmployeeOutputModel();
            _mapper.Map(updatedEmployee, employeeOutputModel);
            return employeeOutputModel;
        }

        [PermissonCheck("DeleteEmployee")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult<EmployeeOutputModel>> DeleteEmployee(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound($"Employee with {id} not found");
            }
            var deletedEmployee = await _employeeRepository.DeleteAsync(id);
            //create employee output model to view
            var employeeOutputModel = new EmployeeOutputModel();
            _mapper.Map(deletedEmployee, employeeOutputModel);
            return Ok(employeeOutputModel);
        }
    }
}
