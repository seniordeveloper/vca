using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Vca.Models.Identity
{
    [ExcludeFromCodeCoverage]
    public class SigninModel
    {
        [Required]
        [EmailAddress]
        /// <summary>
        ///  Get or sets user's login.
        /// </summary>
        public string Login { get; set; }

        [Required]
        /// <summary>
        ///  Gets or sets user's password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets a boolean value of "remember me".
        /// </summary>
        public bool? RememberMe { get; set; }
    }
}
