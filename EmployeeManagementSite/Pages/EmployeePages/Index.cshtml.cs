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
    public class IndexModel : PageModel
    {
        public IList<Employee> Employee { get; set; }
        public string CurrentFilter { get; set; }

        public async Task OnGetAsync(string searchString)
        {
            Employee = new List<Employee>();
            if (!String.IsNullOrEmpty(searchString))
            {
                CurrentFilter = searchString;
                HttpClient client = new HttpClient();
                HttpResponseMessage response;
                response = await client.GetAsync($"http://localhost:5000/odata/Employees?$filter=contains(FullName, '{CurrentFilter}') or contains(JobTitle, '{CurrentFilter}')");

                HttpContent content = response.Content;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var jdo = await System.Text.Json.JsonSerializer.DeserializeAsync<JDO<Employee>>(content.ReadAsStream(), options);
                Employee = jdo.value;
            }
            else
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("http://localhost:5000/odata/Employees");
                HttpContent content = response.Content;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var jdo = await System.Text.Json.JsonSerializer.DeserializeAsync<JDO<Employee>>(content.ReadAsStream(), options);
                Employee = jdo.value;
            }
        }
    }
}
