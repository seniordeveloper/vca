using Microsoft.AspNetCore.Identity;

namespace Vca.Data.Entities.Identity
{
    public partial class UserEntity : IdentityUser<long> 
    {
        /// <summary>
        /// Gets or sets the first name for this user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name for this user.
        /// </summary>
        public string LastName { get; set; }
    }
}
