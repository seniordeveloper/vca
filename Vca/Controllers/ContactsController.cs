using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vca.Abstractions.Services;
using Vca.Controllers.Base;
using Vca.Models;

namespace Vca.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class ContactsController : BaseController
    {
        private readonly IContactManager _contactManager;

        public ContactsController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        [HttpGet]
        public Task<IActionResult> Index()
        {
            var url = "/asp/Contacts/Default.asp";
            return WrapResponseAsync(url, async cancellationToken => 
            {
                DeleteCookies();

                var result = await _contactManager.GetContactsAsync(GetUserId(), cancellationToken);

                if (result.Succeded)
                {
                    var userContacts = result.Data;
                    HttpContext.Response.Cookies.Append(Cookies.ContactsDefault, Newtonsoft.Json.JsonConvert.SerializeObject(userContacts));
                    return Redirect(url);
                }
                return Redirect($"{url}?errorDescription={result.Error.Description}");
            });
        }

        [HttpGet]
        public Task<IActionResult> Details(long id)
        {
            var url = "/asp/Contacts/Details.asp";
            return WrapResponseAsync(url, async cancellationToken =>
            {
                HttpContext.Response.Cookies.Delete(Cookies.ContactDetails);

                var result = await _contactManager.FindContactAsync(GetUserId(), id, cancellationToken);

                if (result.Succeded)
                {
                    var userContact = result.Data;
                    HttpContext.Response.Cookies.Append(Cookies.ContactDetails, Newtonsoft.Json.JsonConvert.SerializeObject(userContact));
                    return Redirect(url);
                }
                return Redirect($"{url}?errorDescription={result.Error.Description}");
            });
        }

        [HttpPost]
        public Task<IActionResult> Create([FromForm] UserContactModel contact)
        {
            var url = "/asp/Contacts/Create.asp";
            return WrapResponseAsync(url, async cancellationToken => 
            {
                var creationResult = await _contactManager.CreateContactAsync(GetUserId(), contact, cancellationToken);
                if (creationResult.Succeded)
                {
                    return Redirect($"{url}?info=A new contact has been created successfully. You can create more!");
                }
                return Redirect($"{url}?errorDescription={creationResult.Error.Description}");
            });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id, CancellationToken cancellationToken) 
        {
            var deletionResult = await _contactManager.DeleteContactAsync(GetUserId(), id, cancellationToken);
            return Ok(deletionResult.Succeded);
        }

        [HttpGet]
        public Task<IActionResult> Edit(int id) 
        {
            var url = "/asp/Contacts/Edit.asp";
            return WrapResponseAsync(url, async cancellationToken =>
            {
                HttpContext.Response.Cookies.Delete(Cookies.EditableContact);

                var result = await _contactManager.FindContactAsync(GetUserId(), id, cancellationToken);

                if (result.Succeded)
                {
                    var userContact = result.Data;
                    HttpContext.Response.Cookies.Append(Cookies.EditableContact, Newtonsoft.Json.JsonConvert.SerializeObject(userContact));
                    return Redirect(url);
                }
                return Redirect($"{url}?errorDescription={result.Error.Description}");
            });
        }

        [HttpPost]
        public Task<IActionResult> Edit([FromForm] UserContactModel contact) 
        {
            var url = "/asp/Contacts/Edit.asp";
            return WrapResponseAsync(url, async cancellationToken =>
            {
                HttpContext.Response.Cookies.Delete(Cookies.EditableContact);

                var result = await _contactManager.UpdateContactAsync(GetUserId(), contact, cancellationToken);

                if (result.Succeded)
                {
                    var userContact = result.Data;
                    return Redirect("/Contacts/Index");
                }
                return Redirect($"{url}?errorDescription={result.Error.Description}");
            });
        }
    }
}
