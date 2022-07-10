using DataAccess.Models;
using DataAccess.Repositories;
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
    public class AccountDbsController : ODataController
    {
        private readonly IUserRepository repo;

        public AccountDbsController(IUserRepository repo)
        {
            this.repo = repo;
        }

        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

        [EnableQuery]
        public async Task<IActionResult> Post([FromBody] UserModel model)
        {
            try
            {
                var loggedUser = repo.Login(model);
                return Ok(loggedUser);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
