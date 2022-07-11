using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text.Json;

namespace EmployeeManagementSite.Pages.EmployeePages
{
    [Authorize(Roles = "Administrator")]
    public class DetailsModel : PageModel
    {
        [BindProperty]
        public Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync($"http://localhost:5000/odata/Employees({id})");
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            Employee _employee;
            _employee = await JsonSerializer.DeserializeAsync<Employee>(content.ReadAsStream(), options);

            Employee = _employee;

            if (Employee == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
