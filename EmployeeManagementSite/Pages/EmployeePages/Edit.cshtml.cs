using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http;
using System.Text.Json;
using System.Text;

namespace EmployeeManagementSite.Pages.EmployeePages
{
    [Authorize(Roles = "Administrator")]
    public class EditModel : PageModel
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
            Employee _employee = null;
            _employee = await JsonSerializer.DeserializeAsync<Employee>(content.ReadAsStream(), options);

            Employee = _employee;

            if (Employee == null)
            {
                return NotFound();
            }
            client = new HttpClient();

            response = await client.GetAsync("http://localhost:5000/odata/Departments");
            content = response.Content;
            var jdo = await System.Text.Json.JsonSerializer.DeserializeAsync<JDO<Department>>(content.ReadAsStream(), options);
            var department = jdo.value;
            ViewData["DepartmentId"] = new SelectList(department, "DepartmentId", "DepartmentName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            HttpClient client = new HttpClient();
            var json = JsonSerializer.Serialize(Employee);

            HttpResponseMessage response = await client.PutAsync($"http://localhost:5000/odata/Employees({Employee.EmployeeId})",
                new StringContent(json, Encoding.UTF8, "application/json"));
            return RedirectToPage("./Index");
        }

    }
}
