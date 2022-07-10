using DataAccess.Models;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
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
    public class EmployeesController : ODataController
    {
        private readonly IEmployeeRepository repo;

        public EmployeesController(IEmployeeRepository repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var employees = repo.GetEmployeeList();
                return Ok(employees);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [EnableQuery]
        public async Task<IActionResult> Get([FromODataUri] int key)
        {
            try
            {
                var _employee = repo.GetEmployeeById(key);

                if (_employee == null)
                {
                    return NotFound();
                }

                return Ok(_employee);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [EnableQuery]
        public async Task<IActionResult> Post(Employee createModel)
        {
            try
            {
                repo.Add(createModel);
                return Created(createModel);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [EnableQuery]
        public async Task<IActionResult> Put([FromODataUri] int key, [FromBody] Employee requestModel)
        {
            if (key != requestModel.EmployeeId)
            {
                return BadRequest();
            }
            try
            {
                var temp = repo.GetEmployeeById(key);

                if (temp == null)
                {
                    return NotFound();
                }

                repo.Update(key, requestModel);
                return Updated(requestModel);
            }
            catch (Exception e)
            {
                return e.Message.Equals("This employee doesn't exist.")
                    ? NotFound()
                    : StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
