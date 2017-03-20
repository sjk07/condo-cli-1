// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandOption.cs" company="automotiveMastermind and contributors">
//   © automotiveMastermind and contributors. Licensed under MIT. See LICENSE and CREDITS for details.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace AM.Condo.CLI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using JetBrains.Annotations;

    /// <summary>
    /// Represents an option supported by a given command.
    /// </summary>
    public class CommandOption : ICommandProperty
    {
        #region Fields
        private const string FullNamePrefix = "--";

        private const string ShortNamePrefix = "-";

        private const string ValueNamePrefix = "<";

        private const string ValueNameSuffix = ">";

        private const string MultipleValueNameSuffix = ">...";

        private readonly char[] OptionSeparators = new[] { '|', ' ' };
        #endregion

        #region Constructors and Finalizers
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandOption"/> class.
        /// </summary>
        /// <param name="template">
        /// The template for the command option.
        /// </param>
        /// <param name="optionType">
        /// The type of the value supported by the command option.
        /// </param>
        public CommandOption([NotNull] string template, OptionType optionType)
        {
            if (template == null)
            {
                throw new ArgumentNullException(nameof(template));
            }

            if (template.Equals(string.Empty))
            {
                throw new ArgumentException($"The {nameof(template)} argument cannot be empty.", nameof(template));
            }

            this.Template = template;
            this.OptionType = optionType;

            // split the template parts
            var parts = template.Split(OptionSeparators, StringSplitOptions.RemoveEmptyEntries);

            // iterate over each part
            foreach (var part in parts)
            {
                // determine if the template part is a full name
                if (part.StartsWith(FullNamePrefix))
                {
                    // set the full name
                    this.FullName = part.Substring(2);

                    // move on immediately
                    continue;
                }

                // determine if the template part is a short name
                if (part.StartsWith(ShortNamePrefix))
                {
                    // capture the name
                    var name = part.Substring(1);

                    // determine if the name is 1 length and is a symbol
                    if (name.Length == 1 && char.IsSymbol(name[0]))
                    {
                        // set the symbol to the name
                        this.Symbol = name;

                        // move on immediately
                        continue;
                    }

                    // set the name
                    this.Name = name;

                    // move on immediately
                    continue;
                }

                // determine if the part is a value name
                if (part.StartsWith(ValueNamePrefix))
                {
                    // determine if the part is a value name (single value)
                    if (part.EndsWith(ValueNameSuffix))
                    {
                        // set the value name
                        this.ValueName = part.Substring(1, part.Length - 2);

                        // move on immediately
                        continue;
                    }

                    // determine if the part is a value name (multiple value)
                    if (optionType == OptionType.Multiple && part.EndsWith(MultipleValueNameSuffix))
                    {
                        // set the value name
                        this.ValueName = part.Substring(1, part.Length - 5);

                        // move on immediately
                        continue;
                    }
                }

                // throw an argument exception since the template could not be parsed
                throw new ArgumentException
                    ($"The {nameof(template)} specified: {template} could not be parsed.", nameof(template));
            }

            if (string.IsNullOrEmpty(this.FullName)
                && string.IsNullOrEmpty(this.Name)
                && string.IsNullOrEmpty(this.Symbol))
            {
                throw new ArgumentException
                    ($"The {nameof(template)} specified: {template} did not contain a name.", nameof(template));
            }
        }
        #endregion

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
        /// Gets a value indicating whether or not the current option as a boolean value is enabled.
        /// </summary>
        public bool? BooleanValue { get; private set; }

        /// <summary>
        /// Gets the template for the command option.
        /// </summary>
        public string Template { get; }

        /// <summary>
        /// Gets the type of value supported by the option.
        /// </summary>
        public OptionType OptionType { get; }

        /// <summary>
        /// Gets the full name for the command option.
        /// </summary>
        public string FullName { get; }

        /// <summary>
        /// Gets the value name for the command option.
        /// </summary>
        public string ValueName { get; }

        /// <summary>
        /// Gets the symbol associated with the command option.
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// Gets a value indicating whether or not the command option has a current value.
        /// </summary>
        public bool HasValue => this.Values.Any();
        #endregion

        #region Methods
        /// <summary>
        /// Attempts to parse the specified <paramref name="value"/>.
        /// </summary>
        /// <param name="value">
        /// The value to parse.
        /// </param>
        /// <returns>
        /// A value indicating whether or not the specified <paramref name="value"/> was successfully parsed.
        /// </returns>
        public bool TryParse(string value)
        {
            switch (this.OptionType)
            {
                case OptionType.Multiple:
                    this.Values.Add(value);
                    return true;

                case OptionType.Single:
                    if (this.HasValue)
                    {
                        return false;
                    }

                    this.Values.Add(value);
                    return true;

                case OptionType.Boolean:
                    if (this.HasValue)
                    {
                        return false;
                    }

                    if (value == null)
                    {
                        this.Values.Add(null);
                        this.BooleanValue = true;
                        return true;
                    }

                    bool boolean;

                    if (!bool.TryParse(value, out boolean))
                    {
                        return false;
                    }

                    this.Values.Add(value);
                    this.BooleanValue = true;
                    return true;

                case OptionType.None:
                    if (value != null)
                    {
                        return false;
                    }

                    this.Values.Add("present");
                    return true;

                default:
                    return false;
            }
        }
        #endregion
    }
}