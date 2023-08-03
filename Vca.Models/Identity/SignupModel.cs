using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Vca.Models.Identity
{
    [ExcludeFromCodeCoverage]
    public class SignupModel
    {
        [Required]
        [EmailAddress]
        /// <summary>
        ///  Get or sets user's email address.
        /// </summary>
        public string Email { get; set; }

        [Required]
        /// <summary>
        ///  Get or sets user's first name.
        /// </summary>
        public string FirstName { get; set; }

        [Required]
        /// <summary>
        ///  Get or sets user's last name.
        /// </summary>
        public string LastName { get; set; }

        [Required]
        /// <summary>
        ///  Gets or sets user's password.
        /// </summary>
        public string Password { get; set; }
    }
}
