// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializeCommand.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace AM.Condo.CLI
{
    /// <summary>
    /// Represents the root level command used to initialize a project with condo.
    /// </summary>
    public class InitializeCommand : IRootCommand
    {
        #region Properties and Indexers
        /// <inheritdoc />
        public string Argument => "init";

        /// <inheritdoc />
        public string Name => "Initialize";

        /// <inheritdoc />
        public string Description => "Initializes a project with condo";
        #endregion

        #region Methods
        /// <inheritdoc />
        public int Run(string[] args)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}