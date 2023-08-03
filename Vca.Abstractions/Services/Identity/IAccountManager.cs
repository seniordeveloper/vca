using Vca.Models;
using Vca.Models.Identity;

namespace Vca.Abstractions.Services.Identity
{
    /// <summary>
    /// Provides an abstraction to manage authentication to the system.
    /// </summary>
    public interface IAccountManager
    {
        /// <summary>
        /// Verifies provided credentials as an asynchronous operation.
        /// </summary>
        /// <param name="signinModel"><see cref="SigninModel"/> that contains user credentials.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task{T}"/> that contains the result the asynchronous operation, containing an instance of <see cref="AppResult{T}"/>.</returns>
        Task<AppResult<UserModel>> SigninAsync(SigninModel signinModel, CancellationToken cancellationToken);

        /// <summary>
        /// Creates a new user based on <paramref name="signupModel"/> as an asynchronous operation.
        /// </summary>
        /// <param name="signupModel">An instance of <see cref="SignupModel"/> that contains user info.</param>
        /// <param name="cancellationToken">The <see cref="CancellationToken"/> used to propagate notifications that the operation should be canceled.</param>
        /// <returns>The <see cref="Task{T}"/> that contains the result the asynchronous operation, containing an instance of <see cref="AppResult"/>.</returns>
        Task<AppResult> CreateUserAsync(SignupModel signupModel, CancellationToken cancellationToken);
    }
}
