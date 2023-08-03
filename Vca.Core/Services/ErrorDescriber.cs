using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vca.Abstractions.Services;
using Vca.Models;

namespace Vca.Core.Services
{
    /// <summary>
    /// Service to enable localization for application facing identity errors.
    /// </summary>
    /// <remarks>
    /// These errors are returned to controllers and are generally used as display messages to end users.
    /// </remarks>
    public class ErrorDescriber : IErrorDescriber
    {
        public VcaError DefaultError()
        {
            return new VcaError
            {
                Code = nameof(DefaultError),
                Description = GetProperMessage(nameof(DefaultError))
            };
        }

        public VcaError UserNameNotFound(string userName)
        {
            var code = nameof(UserNameNotFound);

            return new VcaError
            {
                Code = code,
                Description = string.Format(GetProperMessage(code), userName)
            };
        }

        public VcaError EmailAlreadyInUse(string email) 
        {
            var code = nameof(EmailAlreadyInUse);
            return new VcaError
            {
                Code = code,
                Description = string.Format(GetProperMessage(code), email)
            };
        }

        public VcaError ContactNotFound(long contactId) 
        {
            var code = nameof(ContactNotFound);
            return new VcaError
            {
                Code = code,
                Description = string.Format(GetProperMessage(code), contactId)
            };
        }

        protected virtual string GetProperMessage(string code) 
        {
            return _errors[code];
        }
        

        private static readonly Dictionary<string, string> _errors = new Dictionary<string, string>() 
        {
            { nameof(DefaultError),  "An error occured while performing the operation."},
            { nameof(UserNameNotFound),  "User {0} does not exist or password is incorrect."},
            { nameof(EmailAlreadyInUse),  "{0} is already in use."},
            { nameof(ContactNotFound),  "A contact info with {0} doesn't exist."}
        };


    }
}
