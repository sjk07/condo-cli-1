// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRootCommand.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AM.Condo.CLI
{
    /// <summary>
    /// Defines the properties and methods required to implement a root-level command within the CLI.
    /// </summary>
    public interface IRootCommand : ICommandDescriptor
    {
        #region Properties and Indexers
        /// <summary>
        /// Gets the argument used to identify the root level command.
        /// </summary>
        string Argument { get; }
        #endregion

        #region Methods
        /// <summary>
        /// Runs the command with the specified <paramref name="args"/>.
        /// </summary>
        /// <param name="args">
        /// The arguments used to run the command.
        /// </param>
        /// <returns>
        /// The exit code of the command.
        /// </returns>
        int Run(string[] args);
        #endregion
    }
}