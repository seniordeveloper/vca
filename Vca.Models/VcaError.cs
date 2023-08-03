using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vca.Models
{
    /// <summary>
    /// Encapsulates an error from the system.
    /// </summary>
    public class VcaError
    {
        /// <summary>
        /// Gets or sets the code for this error.
        /// </summary>
        /// <value>
        /// The code for this error.
        /// </value>
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the description for this error.
        /// </summary>
        /// <value>
        /// The description for this error.
        /// </value>
        public string Description { get; set; }

        public static VcaError CustomUserDefinedError(string description) => new VcaError() { Code = _customUserDefinedError, Description = description };

        protected static readonly string _customUserDefinedError = "CustomUserDefinedError";
    }
}
