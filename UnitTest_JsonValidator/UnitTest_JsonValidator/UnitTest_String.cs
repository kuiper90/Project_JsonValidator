using JsonValidator;
using Xunit;

namespace UnitTest_JsonValidator
{
    public class UnitTest_String
    {
        [Fact]
        public void ShouldBe_True_UnicodeNumberAndNewline()
        {
            Assert.True(StringValidator.ValidateString("\"Test\\u0097\nAnother line\""));
        }

        [Fact]
        public void ShouldBe_True_StartAndEndDoubleQuotes()
        {
            Assert.True(StringValidator.ValidateString("\"abc\""));
        }

        [Fact]
        public void ShouldBe_False_EndExtraEscapedDoubleQuotes()
        {
            Assert.False(StringValidator.ValidateString("\"abc\"\""));
        }

        [Fact]
        public void ShouldBe_False_MidEscapedDoubleQuotes()
        {
            Assert.False(StringValidator.ValidateString("\"ab\"c\""));
        }

        [Fact]
        public void ShouldBe_False_UnicodeNumberOneHex()
        {
            Assert.False(StringValidator.ValidateString("\"ab12\\uc\""));
        }

        [Fact]
        public void ShouldBe_False_StartNoDoubleQuotes()
        {
            Assert.False(StringValidator.ValidateString("Test\""));
        }

        [Fact]
        public void ShouldBe_False_EndNoDoubleQuotes()
        {
            Assert.False(StringValidator.ValidateString("\"Test"));
        }

        [Fact]
        public void ShouldBe_False_StartEscapedReverseSolidus()
        {
            Assert.False(StringValidator.ValidateString("\"\\Test\""));
        }

        [Fact]
        public void ShouldBe_False_EscapeCarriageReturn()
        {
            Assert.True(StringValidator.ValidateString("\"ab12\\rc\""));
        }

        [Fact]
        public void ShouldBe_False_InternEscapedDoubleQuotes()
        {
            Assert.False(StringValidator.ValidateString("\"Te\"st\""));
        }

        [Fact]
        public void ShouldBe_False_InternDoubleReverseSolidus()
        {
            Assert.False(StringValidator.ValidateString("\"Te\\\\st\""));
        }

        [Fact]
        public void ShouldBe_True_DoubleQuotes()
        {
            Assert.True(StringValidator.ValidateString("\"\""));
        }
    }
}
