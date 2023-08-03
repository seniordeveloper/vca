namespace Vca.Models
{
    public class AuthorizedUserModel
    {
        /// <summary>
        /// Gets or sets the primary key for this user.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the email address for this user.
        /// </summary>
        public string Email { get; set; }

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
