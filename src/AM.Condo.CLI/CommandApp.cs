// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandApp.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AM.Condo.CLI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using JetBrains.Annotations;

    /// <summary>
    /// Represents an application used to execute a command line interface command.
    /// </summary>
    class CommandApp : ICommand
    {
        #region Properties and Indexers
        /// <inheritdoc />
        public string Name { get; set; }

        /// <inheritdoc />
        public string Description { get; set; }

        /// <inheritdoc />
        public ICommand Parent { get; set; }

        /// <inheritdoc />
        public string FullName { get; set; }

        /// <inheritdoc />
        public string Syntax { get; set; }

        /// <inheritdoc />
        public CommandOption HelpOption { get; private set; }

        /// <inheritdoc />
        public CommandOption VersionOption { get; private set; }

        /// <inheritdoc />
        public bool HandleResponseFiles { get; set; }

        /// <inheritdoc />
        public bool AllowArgumentSeparator { get; set; }

        /// <inheritdoc />
        public bool HandleRemainingArguments { get; set; }

        /// <inheritdoc />
        public string ArgumentSeparatorHelp { get; set; }

        /// <inheritdoc />
        public string FullNameAndVersion { get; private set; }

        /// <inheritdoc />
        public ICollection<ICommand> Commands { get; } = new List<ICommand>();

        /// <inheritdoc />
        public ICollection<CommandOption> Options { get; } = new List<CommandOption>();

        /// <inheritdoc />
        public ICollection<CommandArgument> Arguments { get; } = new List<CommandArgument>();

        /// <inheritdoc />
        public ICollection<string> RemainingArguments { get; } = new List<string>();

        /// <inheritdoc />
        public Func<int> Invoker { get; private set; }
        #endregion

        #region Methods
        /// <inheritdoc />
        public ICommand AddCommand([NotNull] ICommand command, bool allowUnexpectedArguments)
        {
            // determine if the command is null
            if (command == null)
            {
                // throw a new argument null exception
                throw new ArgumentNullException(nameof(command));
            }

            // set the parent
            command.Parent = this;

            // add the command to the current command
            this.Commands.Add(command);

            // return the command
            return command;
        }

        /// <inheritdoc />
        public CommandOption AddOption([NotNull] string template, string description, OptionType optionType)
        {
            // initialize the option
            var option = new CommandOption(template, optionType)
            {
                Description = description
            };

            // add the option
            this.Options.Add(option);

            // return the option
            return option;
        }

        /// <inheritdoc />
        public CommandArgument AddArgument([NotNull] string name, string description, bool allowMultipleValues)
        {
            // get the last argument
            var last = this.Arguments.LastOrDefault();

            // determine if the last argument exists and allows multiple values
            if (last != null && last.AllowMultipleValues)
            {
                // throw a new invalid operation exception
                // tricky: only one argument can accept multiple values
                throw new InvalidOperationException();
            }

            // create a new argument
            var argument = new CommandArgument
            {
                Name = name,
                Description = description,
                AllowMultipleValues = allowMultipleValues
            };

            // add the argument to the collection
            this.Arguments.Add(argument);

            // return the argument
            return argument;
        }

        /// <inheritdoc />
        public void OnExecute([NotNull] Func<int> invoker)
        {
            // set the invoker
            this.Invoker = invoker;
        }

        /// <inheritdoc />
        public void OnExecuteAsync([NotNull] Func<Task<int>> invoker)
        {
            // set the invoker
            this.Invoker = () => invoker().GetAwaiter().GetResult();
        }

        /// <inheritdoc />
        public int Execute(params string[] args)
        {
            var command = this;

            return 0;
        }

        /// <inheritdoc />
        public CommandOption SetHelpOption([NotNull] string template)
        {
            return this.HelpOption = this.AddOption(template, "Show Help", OptionType.None);
        }

        /// <inheritdoc />
        public CommandOption SetVersionOption([NotNull] string template, string informationalVersion, string version)
        {
            if (version == null)
            {
            }

            return this.VersionOption = this.AddOption(template, "Show Version", OptionType.None);
        }

        /// <inheritdoc />
        public void ShowHint()
        {
            if (this.HelpOption == null)
            {
                return;
            }
        }

        /// <inheritdoc />
        public void ShowVersion()
        {
            Console.WriteLine(this.FullName);
        }

        /// <inheritdoc />
        public void ShowHelp(string command)
        {
        }

        /// <inheritdoc />
        public void ShowRootCommandFullNameAndVersion()
        {
            // capture the current command
            var root = (ICommand)this;

            // continue iterating until we have found the root command
            while (root.Parent != null)
            {
                // set the root to the parent of the current command
                root = root.Parent;
            }

            // write the current root full name and version
            Console.WriteLine(root.FullNameAndVersion);
        }
        #endregion
    }
}