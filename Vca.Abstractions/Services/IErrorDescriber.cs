using Vca.Models;

namespace Vca.Abstractions.Services
{
    /// <summary>
    /// Provides an abstraction for describing errors.
    /// </summary>
    public interface IErrorDescriber
    {
        /// <summary>
        /// Returns a <see cref="VcaError"/> containing the default message.
        /// </summary>
        /// <returns>A <see cref="VcaError"/> representing the default message.</returns>
        VcaError DefaultError();

        /// <summary>
        /// Returns a <see cref="VcaError"/> indicating a user <paramref name="userName"/> does not exist.
        /// </summary>
        /// <param name="userName">The invalid username.</param>
        /// <returns>A <see cref="VcaError"/> indicating a user <paramref name="userName"/> does not exist.</returns>
        VcaError UserNameNotFound(string userName);

        /// <summary>
        /// Returns a <see cref="VcaError"/> indicating a given <paramref name="email"/> is already in use.
        /// </summary>
        /// <param name="email">Already in use email.</param>
        /// <returns>A <see cref="VcaError"/> indicating a given <paramref name="email"/> is already in use.</returns>
        VcaError EmailAlreadyInUse(string email);

        /// <summary>
        /// Returns a <see cref="VcaError"/> indicating a contact info with <paramref name="contactId"/> doesn't exists.
        /// </summary>
        /// <param name="contactId">A unique identifier of contact.</param>
        /// <returns>A <see cref="VcaError"/> indicating a contact info with <paramref name="contactId"/> doesn't exists.</returns>
        VcaError ContactNotFound(long contactId);
    }
}
