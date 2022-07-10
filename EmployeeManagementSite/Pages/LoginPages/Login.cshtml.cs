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
                HttpClient client = new HttpClient();
                var json = JsonSerializer.Serialize(UserInput);

                HttpResponseMessage response = await client.PostAsync("http://localhost:5000/odata/AccountDbs",
                    new StringContent(json, Encoding.UTF8, "application/json"));
                HttpContent content = response.Content;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                AccountDb UserInfo = null;
                if (response.StatusCode != System.Net.HttpStatusCode.NoContent)
                {
                    UserInfo = await JsonSerializer.DeserializeAsync<AccountDb>(content.ReadAsStream(), options);
                }

                if (UserInfo != null)
                {
                    HttpContext.Session.SetString("UserID", UserInfo.UserId);
                    var claims = new List<Claim>();
                    claims.Add(new Claim("Email", UserInput.Email));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, UserInput.Password));
                    if (UserInfo.AccountRole == 1)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                        returnUrl = "/EmployeePages/Index";
                    }
                    else if (UserInfo.AccountRole == 2)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Manager"));
                        returnUrl = "/AccessDenied";
                    } else
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Staff"));
                        returnUrl = "/AccessDenied";
                    }
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimPrincipal);
                    return RedirectToPage(returnUrl);
                }
                else
                {
                    TempData["Error"] = "Invalid password/username";
                }
            }
            if (UserInput.Email != null && UserInput.Password != null)
            {
                ViewData["Message"] = string.Format("Your account or password is incorrect. Try again!");
            }
            return Page();
        }
    }
}
