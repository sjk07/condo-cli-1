// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandProperty.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AM.Condo.CLI
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the properties and methods required to implement a property (argument or option) of a command.
    /// </summary>
    public interface ICommandProperty : ICommandDescriptor
    {
        #region Properties and Indexers
        /// <summary>
        /// Gets the values associated with the command property.
        /// </summary>
        ICollection<string> Values { get; }

        /// <summary>
        /// Gets the current value associated with the command property.
        /// </summary>
        string Value { get; }
        #endregion
    }
}