using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DataAccess.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace EmployeeManagementSite.Pages.LoginPages
{
    public class LoginModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string returnUrl { get; set; }
        [BindProperty]
        public UserModel UserInput { get; set; }

        public async Task OnGetAsync()
        {
            await HttpContext.SignOutAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                
            }
            return Page();
        }
    }
}
