using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using DataAccess.Models;

namespace EmployeeManagementSite.Pages.EmployeePages
{
    public class IndexModel : PageModel
    {
        private readonly DataAccess.Models.DepartmentEmployeePETrailContext _context;

        public IndexModel(DataAccess.Models.DepartmentEmployeePETrailContext context)
        {
            _context = context;
        }

        public IList<Employee> Employee { get;set; }

        public async Task OnGetAsync()
        {
            Employee = await _context.Employees
                .Include(e => e.Department).ToListAsync();
        }
    }
}
