using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace EmployeeManagementSite.Pages.EmployeePages
{
    [Authorize(Roles = "Administrator")]
    public class CreateModel : PageModel
    {
        [BindProperty]
        public Employee Employee { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5000/odata/Departments");
            HttpContent content = response.Content;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var jdo = await System.Text.Json.JsonSerializer.DeserializeAsync<JDO<Department>>(content.ReadAsStream(), options);
            var department = jdo.value;
            ViewData["DepartmentId"] = new SelectList(department, "DepartmentId", "DepartmentName");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            HttpClient client = new HttpClient();
            var json = JsonSerializer.Serialize(Employee);

            HttpResponseMessage response = await client.PostAsync("http://localhost:5000/odata/Employees",
                new StringContent(json, Encoding.UTF8, "application/json"));
            if (response.StatusCode != System.Net.HttpStatusCode.Created)
            {
                return Page();
            }
            return RedirectToPage("./Index");
        }

    }
}
