using System.ComponentModel.DataAnnotations;

namespace Vca.Models
{
    /// <summary>
    /// Represents a contact info belonging to individual user.
    /// </summary>
    public class UserContactModel
    {
        /// <summary>
        /// Gets or sets the primary key for this entity.
        /// </summary>
        public long Id { get; set; }

        [Required]
        [MaxLength(50)]
        /// <summary>
        ///  Get or sets first name of this contact..
        /// </summary>
        public string Name { get; set; }

        [Required]
        [MaxLength(30)]
        [Phone]
        /// <summary>
        ///  Get or sets phone number of this contact..
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets date and time when thic contact is created.
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }
}
