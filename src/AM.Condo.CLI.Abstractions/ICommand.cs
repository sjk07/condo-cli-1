// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApp.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AM.Condo.CLI
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Defines the properties and methods required to implement a command.
    /// </summary>
    public interface ICommand : ICommandDescriptor
    {
        #region Properties and Indexers
        /// <summary>
        /// Gets or sets the parent command of this app.
        /// </summary>
        ICommand Parent { get; set; }

        /// <summary>
        /// Gets or sets the full name of the command.
        /// </summary>
        string FullName { get; set; }

        /// <summary>
        /// Gets or sets the syntax of the command.
        /// </summary>
        string Syntax { get; set; }

        /// <summary>
        /// Gets the command option for help information.
        /// </summary>
        CommandOption HelpOption { get; }

        /// <summary>
        /// Gets the command option for version information.
        /// </summary>
        CommandOption VersionOption { get; }

        /// <summary>
        /// Gets the collection of commands associated with the current command.
        /// </summary>
        ICollection<ICommand> Commands { get; }

        /// <summary>
        /// Gets the collection of command options associated with the command.
        /// </summary>
        ICollection<CommandOption> Options { get; }

        /// <summary>
        /// Gets the collection of command arguments associated with the command.
        /// </summary>
        ICollection<CommandArgument> Arguments { get; }

        /// <summary>
        /// Gets or sets the collection of remaining arguments associated with the command.
        /// </summary>
        ICollection<string> RemainingArguments { get; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to handle response files.
        /// </summary>
        bool HandleResponseFiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to allow additional argument separator.
        /// </summary>
        bool AllowArgumentSeparator { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether or not to handle remaining arguments.
        /// </summary>
        bool HandleRemainingArguments { get; set; }

        /// <summary>
        /// Gets or sets the help associated with the argument separator.
        /// </summary>
        string ArgumentSeparatorHelp { get; set; }

        /// <summary>
        /// Gets the full name and version of the command.
        /// </summary>
        string FullNameAndVersion { get; }

        /// <summary>
        /// Gets the delegate used to invoke the command.
        /// </summary>
        Func<int> Invoker { get; }
        #endregion

        #region Methods
        ICommand AddCommand(ICommand command, bool allowUnexpectedArguments);

        CommandOption AddOption
            (string template, string description, OptionType optionType);

        CommandArgument AddArgument
            (string name, string description, bool allowMultipleValues);

        void OnExecute(Func<int> invoker);

        void OnExecuteAsync(Func<Task<int>> invoker);

        int Execute(string[] args);

        CommandOption SetHelpOption(string template);

        CommandOption SetVersionOption(string template, string informationalVersion, string version);

        void ShowHint();

        void ShowHelp(string name);

        void ShowVersion();

        void ShowRootCommandFullNameAndVersion();
        #endregion
    }
}