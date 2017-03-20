namespace AM.Condo.CLI.Abstractions
{
    using System;
    using Xunit;

    public class CommandOptionTest
    {
        [Fact]
        public void Ctor_WhenTemplateNull_Throws()
        {
            // arrange
            var template = default(string);
            var optionType = OptionType.None;

            // act
            Func<CommandOption> act = () => new CommandOption(template, optionType);

            // assert
            Assert.Throws<ArgumentNullException>(nameof(template), act);
        }

        [Fact]
        public void Ctor_WhenTemplateEmpty_Throws()
        {
            // arrange
            var template = string.Empty;
            var optionType = OptionType.None;

            // act
            Func<CommandOption> act = () => new CommandOption(template, optionType);

            // assert
            Assert.Throws<ArgumentException>(nameof(template), act);
        }

        [Fact]
        public void Ctor_WhenTemplateValid_ParsesTemplate()
        {
        }
    }
}
