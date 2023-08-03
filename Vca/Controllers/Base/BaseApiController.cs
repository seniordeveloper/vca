using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;
using System.Security.Claims;
using Vca.Models;

namespace Vca.Controllers.Base
{
    public class BaseController : Controller
    {
        protected virtual async Task SinginAsync(UserModel user, bool isPersistent) 
        {
            var claims = new List<Claim>()
            {
                  new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.Id)),
                  new Claim(ClaimTypes.Email, user.Email),
                  new Claim(ClaimTypes.Name, user.Email),
                  new Claim(ClaimTypes.GivenName, user.FirstName),
                  new Claim(ClaimTypes.Surname, user.LastName),
            };
            
            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
              
            var principal = new ClaimsPrincipal(identity);
            
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
            {
                IsPersistent = isPersistent
            });
        }

        protected long GetUserId() 
        {
            var stringedId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new NullReferenceException("Developer's mistake: ensure controller/action method is decorated with AuthorizeAttribute");
            
            return long.Parse(stringedId);
        }

        protected string GetUserEmail()
        {
            return User.FindFirstValue(ClaimTypes.Email)
                ?? throw new NullReferenceException("Developer's mistake: ensure controller/action method is decorated with AuthorizeAttribute");
        }

        protected AuthorizedUserModel GetUser() 
        {
            var stringedId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? throw new NullReferenceException("Developer's mistake: ensure controller/action method is decorated with AuthorizeAttribute");

            return new AuthorizedUserModel 
            {
                Id = int.Parse(stringedId),
                Email = User.FindFirstValue(ClaimTypes.Email),
                FirstName = User.FindFirstValue(ClaimTypes.GivenName),
                LastName = User.FindFirstValue(ClaimTypes.Surname),
            };
        }

        protected async Task<IActionResult> WrapResponseAsync<T>(string url, Func<CancellationToken, Task<T>> actionBody) where T : IActionResult
        {
            if (!ModelState.IsValid)
            {
                return Redirect($"{url}?errorDescription={string.Join("<br>", ModelState.Values.SelectMany(v => v.Errors).Select(x => x.ErrorMessage))}.");
            }
            var cancellationToken = HttpContext.RequestAborted;

            try
            {
                var actionResult = await actionBody(cancellationToken);
                return actionResult;
            }
            catch (Exception ex)
            {
                return Redirect($"{url}?errorDescription={ex.Message}");
            }
        }

        protected virtual void DeleteCookies() 
        {
            HttpContext.Response.Cookies.Delete(Cookies.ContactsDefault);
            HttpContext.Response.Cookies.Delete(Cookies.ContactDetails);
            HttpContext.Response.Cookies.Delete(Cookies.EditableContact);
        }
    }
}
