using Microsoft.AspNetCore.Mvc;
using Vca.Abstractions.Services;
using Vca.Abstractions.Services.Identity;
using Vca.Controllers.Base;
using Vca.Models.Identity;

namespace Vca.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IAccountManager _accountManager;

        public AccountController(IAccountManager accountManager)
        {
            _accountManager = accountManager;
        }

        public IActionResult Signin()
        {
            return Redirect("/asp/Account/Signin.asp");
        }

        [HttpPost]
        public Task<IActionResult> Signin([FromForm] SigninModel signinModel) 
        {
            var url = "/asp/Account/Signin.asp";

            return WrapResponseAsync(url, async cancellationToken => 
            {
                var result = await _accountManager.SigninAsync(signinModel, cancellationToken);

                if (result.Succeded)
                {
                    HttpContext.Response.Cookies.Delete(Cookies.AccountDetails);
                    await SinginAsync(result.Data, signinModel.RememberMe.GetValueOrDefault());
                    HttpContext.Response.Cookies.Append(Cookies.AccountDetails, Newtonsoft.Json.JsonConvert.SerializeObject(result.Data));
                    return Redirect("/asp/Contacts/Default.asp");
                }
                return Redirect($"{url}?errorDescription={result.Error.Description}");
            });
        }

        [HttpPost]
        public Task<IActionResult> Signup([FromForm] SignupModel signupModel) 
        {
            var url = "/asp/Account/Signup.asp";
            return WrapResponseAsync(url, async cancellationToken => 
            {
                var result = await _accountManager.CreateUserAsync(signupModel, cancellationToken);

                if (result.Succeded)
                {
                    return Redirect("/asp/Account/Signin.asp?info=Registration is success. You can login now.");
                }

                return Redirect($"/asp/Account/Signup.asp?errorDescription={result.Error.Description}");
            });
        }
    }
}
