namespace Vca.Models
{
    /// <summary>
    /// Represents the result of an application operation.
    /// </summary>
    public class AppResult
    {
        protected AppResult(bool succeded, VcaError error)
        {
            Error = error;
            Succeded = succeded;
        }

        /// <summary>
        /// Flag indicating whether if the operation succeeded or not.
        /// </summary>
        /// <value>True if the operation succeeded, otherwise false.</value>
        public bool Succeded { get; protected set; }

        /// <summary>
        /// A <see cref="VcaError"/> containing error description and code that occured during the operation.
        /// </summary>
        public VcaError Error { get; private set; }

        /// <summary>
        /// Returns a <see cref="AppResult"/> that represents a failed operation.
        /// </summary>
        /// <param name="error">A <see cref="VenditError"/> containing error description and code.</param>
        /// <returns>A <see cref="AppResult"/> that represents a failed operation.</returns>
        public static AppResult Failed(VcaError error)
        {
            return new AppResult(false, error);
        }

        /// <summary>
        /// Returns a <see cref="AppResult"/> that represents a failed operation.
        /// </summary>
        /// <param name="description">Description of error message.</param>
        /// <returns>A <see cref="AppResult"/> that represents a failed operation.</returns>
        public static AppResult Failed(string description)
        {
            return new AppResult(false, VcaError.CustomUserDefinedError(description));
        }

        /// <summary>
        ///  Returns a <see cref="AppResult"/> that represents a failed operation.
        /// </summary>
        /// <param name="descriptions">A collection of error messages.</param>
        /// <returns>A <see cref="AppResult"/> that represents a failed operation.</returns>
        public static AppResult Failed(IEnumerable<string> descriptions)
        {
            return new AppResult(false, VcaError.CustomUserDefinedError(string.Join(", ", descriptions)));
        }

        /// <summary>
        /// Returns a <see cref="AppResult"/> that represents a successful operation.
        /// </summary>
        /// <returns>A <see cref="AppResult"/> that represents a successful operation.</returns>
        public static AppResult Success()
        {
            return new AppResult(true, null);
        }
    }

    /// <summary>
    ///  Represents the result of an application operation.
    /// </summary>
    /// <typeparam name="T">The type encapsulating a data.</typeparam>
    public class AppResult<T> : AppResult
    {
        /// <summary>
        ///  Creates a new instance of <see cref="AppResult{T}"/>.
        /// </summary>
        /// <param name="data">Encapsulated data.</param>
        /// <param name="succeded">A flag that indicates whether the operations is succeded or not.</param>
        /// <param name="description">The description of error message.</param>
        private AppResult(T data, bool succeded, VcaError error)
            : base(succeded, error)
        {
            Data = data;
        }

        public T Data { get; private set; }

        /// <summary>
        /// Returns a <see cref="AppResult{T}"/> that represents a successful operation.
        /// </summary>
        ///  <param name="data">Encapsulated data.</param>
        /// <returns>A <see cref="AppResult{T}"/> that represents a successful operation.</returns>
        public static AppResult<T> Success(T data)
        {
            return new AppResult<T>(data, true, null);
        }

        /// <summary>
        /// Returns a <see cref="AppResult{T}"/> that represents a failed operation.
        /// </summary>
        /// <param name="error">A <see cref="VenditError"/> containing error description and code that occured during the operation.</param>
        /// <returns>A <see cref="AppResult{T}"/> that represents a failed operation.</returns>
        public static new AppResult<T> Failed(VcaError error)
        {
            return new AppResult<T>(default(T), false, error);
        }

        /// <summary>
        /// Returns a <see cref="AppResult{T}"/> that represents a failed operation.
        /// </summary>
        /// <param name="description">Description of error message.</param>
        /// <returns>A <see cref="AppResult{T}"/> that represents a failed operation.</returns>
        public static new AppResult<T> Failed(string description)
        {
            return new AppResult<T>(default(T), false, VcaError.CustomUserDefinedError(description));
        }

        /// <summary>
        /// Returns a <see cref="AppResult{T}"/> that represents a failed operation.
        /// </summary>
        /// <param name="data">Encapsulated data.</param>
        /// <returns>A <see cref="AppResult{T}"/> that represents a failed operation.</returns>
        public static AppResult<T> Failed(T data)
        {
            return new AppResult<T>(data, false, null);
        }

        /// <summary>
        ///  Returns a <see cref="AppResult{T}"/> that represents a failed operation.
        /// </summary>
        /// <param name="descriptions">A collection of error messages.</param>
        /// <returns>A <see cref="AppResult{T}"/> that represents a failed operation.</returns>
        public static new AppResult<T> Failed(IEnumerable<string> descriptions)
        {
            return new AppResult<T>(default(T), false, VcaError.CustomUserDefinedError(string.Join(", ", descriptions)));
        }
    }
}
