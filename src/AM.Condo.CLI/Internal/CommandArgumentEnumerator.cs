// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandArgumentEnumerator.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AM.Condo.CLI.Internal
{
    using System.Collections;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an enumerator of command line arguments which may or may not contain an argument with multiple
    /// values.
    /// </summary>
    public class ArgumentEnumerator : IEnumerator<CommandArgument>
    {
        #region Fields
        private readonly IEnumerator<CommandArgument> enumerator;
        #endregion

        #region Constructors and Initializers
        /// <summary>
        /// Initializes a new instance of the <see cref="ArgumentEnumerator"/> class.
        /// </summary>
        /// <param name="enumerator">
        /// The enumerator used to enumerate a collection of command arguments.
        /// </param>
        /// <param name="commandName">
        /// The name of the command associated with the enumerator.
        /// </param>
        public ArgumentEnumerator(IEnumerator<CommandArgument> enumerator, string commandName)
        {
            // set the enumerator
            this.enumerator = enumerator;

            // set the name of the command
            this.CommandName = commandName;
        }
        #endregion

        #region Properties and Indexers
        /// <summary>
        /// Gets or sets the name of the command associated with the enumerator.
        /// </summary>
        public string CommandName { get; }

        /// <inheritdoc />
        public CommandArgument Current => this.enumerator.Current;

        /// <inheritdoc />
        object IEnumerator.Current => this.Current;
        #endregion

        #region Methods
        /// <inheritdoc />
        public bool MoveNext()
        {
            var current = this.Current;

            if (current == null || !current.AllowMultipleValues)
            {
                return this.enumerator.MoveNext();
            }

            return true;
        }

        /// <inheritdoc />
        public void Reset()
        {
            this.enumerator.Reset();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            this.enumerator.Dispose();
        }
        #endregion
    }
}