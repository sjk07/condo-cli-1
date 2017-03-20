// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandArgument.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AM.Condo.CLI
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Represents an argument specified at the command line interface for a given root or sub command.
    /// </summary>
    public class CommandArgument : ICommandProperty
    {
        #region Properties and Indexers
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public ICollection<string> Values { get; } = new List<string>();

        /// <inheritdoc />
        public string Value => this.Values.FirstOrDefault();

        /// <summary>
        /// Gets or sets a value indicating whether or not the command argument accepts multiple values.
        /// </summary>
        public bool AllowMultipleValues { get; set; }
        #endregion
    }
}