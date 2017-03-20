// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionType.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AM.Condo.CLI
{
    /// <summary>
    /// Represents the available types of options allowed within the CLI.
    /// </summary>
    public enum OptionType
    {
        /// <summary>
        /// NONE: No option type is specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// The option type is a boolean value.
        /// </summary>
        Boolean = 1,

        /// <summary>
        /// The option type is a single value.
        /// </summary>
        Single = 2,

        /// <summary>
        /// The option type is multiple value.
        /// </summary>
        Multiple = 3
    }
}