using Vca.Models;

namespace Vca.Abstractions.Services
{
    /// <summary>
    /// Provides an abstraction that manages logged in user's contacts.
    /// </summary>
    public interface IContactManager
    {
        /// <summary>
        /// Queries user's contacts as an asynchronous opearation.
        /// </summary>
        /// <param name="userId">Current user's primary ket.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task{T}"/> that contains the result the asynchronous operation, containing an instance of <see cref="AppResult{T}"/>.</returns>
        Task<AppResult<IEnumerable<UserContactModel>>> GetContactsAsync(long userId, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new contact for the current user based on <paramref name="contactModel"/> as an asynchronous opearation.
        /// </summary>
        /// <param name="userId">Current user's primary ket.</param>
        /// <param name="contactModel"><see cref="UserContactModel"/> payload that contains contct info.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task{T}"/> that contains the result the asynchronous operation, containing an instance of <see cref="AppResult{T}"/>.</returns>
        Task<AppResult<UserContactModel>> CreateContactAsync(long userId, UserContactModel contactModel, CancellationToken cancellationToken);

        /// <summary>
        /// Removes specified <paramref name="contactId"/> as an asynchronous opearation.
        /// </summary>
        /// <param name="userId">Current user's primary key.</param>
        /// <param name="contactId">Primary key of the contact that needs to be removed.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task{T}"/> that contains the result the asynchronous operation, containing an instance of <see cref="AppResult"/>.</returns>
        Task<AppResult> DeleteContactAsync(long userId, long contactId, CancellationToken cancellationToken);

        /// <summary>
        /// Finds a contact info by <paramref name="contactId"/> as an asynchronous opearation.
        /// </summary>
        /// <param name="userId">Current user's primary ket.</param>
        /// <param name="contactId">Primary key of the contact that needs to be found.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task{T}"/> that contains the result the asynchronous operation, containing an instance of <see cref="AppResult{T}"/>.</returns>
        Task<AppResult<UserContactModel>> FindContactAsync(long userId, long contactId, CancellationToken cancellationToken);

        /// <summary>
        /// Updates contact info based on <paramref name="contactModel"/> as an asynchronous opearation.
        /// </summary>
        /// <param name="userId">Current user's primary ket.</param>
        /// <param name="contactModel"><see cref="UserContactModel"/> payload that contains contct info.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task{T}"/> that contains the result the asynchronous operation, containing an instance of <see cref="AppResult{T}"/>.</returns>
        Task<AppResult<UserContactModel>> UpdateContactAsync(long userId, UserContactModel contactModel, CancellationToken cancellationToken);
    }
}
