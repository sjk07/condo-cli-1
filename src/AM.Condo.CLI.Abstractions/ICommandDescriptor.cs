// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandDescriptor.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AM.Condo.CLI
{
    /// <summary>
    /// Defines the properties and methods required to implement a descriptor of a command line app, argument, or
    /// option.
    /// </summary>
    public interface ICommandDescriptor
    {
        #region Properties and Indexers
        /// <summary>
        /// Gets the name of the command property.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the command property.
        /// </summary>
        string Description { get; }
        #endregion
    }
}