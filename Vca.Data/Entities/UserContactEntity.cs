using Vca.Data.Entities.Identity;

namespace Vca.Data.Entities
{
    /// <summary>
    /// An entity class used by EF to map/manipulate [UserContacts] table.
    /// </summary>
    public partial class UserContactEntity
    {
        /// <summary>
        /// Gets or sets the primary key for this entity.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Gets or sets the foreign key that references user's table.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="UserEntity"/> referenced by this entity.
        /// </summary>
        public UserEntity User { get; set; }

        /// <summary>
        /// Gets or sets name of this contact.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets phone number of this contact.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets date and time when thic contact is created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
