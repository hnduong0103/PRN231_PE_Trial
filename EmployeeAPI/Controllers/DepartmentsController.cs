using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : ODataController
    {
        private readonly IDepartmentRepository repo;

        public DepartmentsController(IDepartmentRepository repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = repo.GetDepartmentList();
                return Ok(employees);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }
    }
}
