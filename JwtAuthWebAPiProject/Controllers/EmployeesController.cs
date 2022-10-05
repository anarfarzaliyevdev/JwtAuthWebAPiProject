using JwtAuthWebAPiProject.CustomAttributes;
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
        [PermissonCheck("Permission", "Create")]
        [HttpGet]
        public void GetAll()
        {

        }
    }
}
