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
        public async Task<ActionResult<List<Employee>>> GetAll()
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
        [PermissonCheck("CreateEmployee")]
        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(CreateEmployeeInputModel createEmployeeInputModel)
        {

            if (createEmployeeInputModel == null)
            {
                return BadRequest();
            }
            var employee = new Employee();
            _mapper.Map(createEmployeeInputModel, employee);
            var newEmployee = await _employeeRepository.CreateAsync(employee);

            return CreatedAtAction(nameof(GetEmployee), new { id = newEmployee.Id }, newEmployee);


        }

        [PermissonCheck("UpdateEmployee")]
        [HttpPut]
        public async Task<ActionResult<Employee>> UpdateEmployee(UpdateEmployeeInputModel updateEmployeeInputModel)
        {


           
            if (updateEmployeeInputModel == null)
            {
                return NotFound($"Employee with given data not found");
            }
            var employee = new Employee();
            _mapper.Map(updateEmployeeInputModel, employee);
            await _employeeRepository.UpdateAsync(employee);

            return await _employeeRepository.GetByIdAsync(employee.Id);



        }
        [PermissonCheck("DeleteEmployee")]
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
