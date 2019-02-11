using JsonValidator;
using Xunit;

namespace UnitTest_JsonValidator
{
    public class UnitTest_Number
    {
        [Fact]
        public void ShouldBe_True_UnsignedByte()
        {
            Assert.True(NumberValidator.ValidateNumber("234"));
        }

        [Fact]
        public void ShouldBe_True_SignedByte()
        {
            Assert.True(NumberValidator.ValidateNumber("-123"));
        }

        [Fact]
        public void ShouldBe_True_Float()
        {
            Assert.True(NumberValidator.ValidateNumber("12.34"));
        }

        [Fact]
        public void ShouldBe_True_FloatWithPositiveExponent1()
        {
            Assert.True(NumberValidator.ValidateNumber("12.123e3"));
        }

        [Fact]
        public void ShouldBe_True_FloatWithPositiveExponent2()
        {
            Assert.True(NumberValidator.ValidateNumber("12.123E+3"));
        }

        [Fact]
        public void ShouldBe_True_FloatWithNegativeExponent()
        {
            Assert.True(NumberValidator.ValidateNumber("12.123E-2"));
        }

        [Fact]
        public void ShouldBe_False_UnsignedByteStartWithZero()
        {
            Assert.False(NumberValidator.ValidateNumber("012"));
        }

        [Fact]
        public void ShouldBe_False_FloatNoExp()
        {
            Assert.False(NumberValidator.ValidateNumber("12.123E"));
        }

        [Fact]
        public void ShouldBe_False_UnsignedByteEndWithPeriod()
        {
            Assert.False(NumberValidator.ValidateNumber("12."));
        }

        [Fact]
        public void ShouldBe_False_Period()
        {
            Assert.False(NumberValidator.ValidateNumber("."));
        }

        [Fact]
        public void ShouldBe_False_PeriodDigit()
        {
            Assert.False(NumberValidator.ValidateNumber(".5"));
        }

        [Fact]
        public void ShouldBe_False_ZeroPeriod()
        {
            Assert.False(NumberValidator.ValidateNumber("0."));
        }

        [Fact]
        public void ShouldBe_False_FloatNoFractionalPartWithExp()
        {
            Assert.False(NumberValidator.ValidateNumber("123.E15"));
        }

        [Fact]
        public void ShouldBe_False_NegFloatNoFractionalPartWithNegExp()
        {
            Assert.False(NumberValidator.ValidateNumber("-123.E-15"));
        }

        [Fact]
        public void ShouldBe_True_NegFloatWithNegExp()
        {
            Assert.True(NumberValidator.ValidateNumber("-123.1E-15"));
        }

        [Fact]
        public void ShouldBe_False_Exp()
        {
            Assert.False(NumberValidator.ValidateNumber("e"));
        }

        [Fact]
        public void ShouldBe_False_FractionalExp()
        {
            Assert.False(NumberValidator.ValidateNumber(".e"));
        }

        [Fact]
        public void ShouldBe_False_FloatExp()
        {
            Assert.False(NumberValidator.ValidateNumber("e."));
        }

        [Fact]
        public void ShouldBe_False_DoubleZero()
        {
            Assert.False(NumberValidator.ValidateNumber("00"));
        }

        [Fact]
        public void ShouldBe_True_Zero()
        {
            Assert.True(NumberValidator.ValidateNumber("0"));
        }

        [Fact]
        public void ShouldBe_True_NegFloatZeroFractionalPartWithNegExp()
        {
            Assert.True(NumberValidator.ValidateNumber("-123.0E-15"));
        }

        [Fact]
        public void ShouldBe_False_NegFloatWithNegFractionalExp()
        {
            Assert.False(NumberValidator.ValidateNumber("-123.01E-1.5"));
        }

        [Fact]
        public void ShouldBe_False_NegFloatWithDoubleNegExp()
        {
            Assert.False(NumberValidator.ValidateNumber("-123.01Ee-15"));
        }

        [Fact]
        public void ShouldBe_False_NegZeroMinusExp()
        {
            Assert.False(NumberValidator.ValidateNumber("-0-e12"));
        }

        [Fact]
        public void ShouldBe_True_NegFloatZeroFractionalPartWithPosEx()
        {
            Assert.True(NumberValidator.ValidateNumber("12.0E+2"));
        }

        [Fact]
        public void ShouldBe_False_EmtpyNumber()
        {
            Assert.False(NumberValidator.ValidateNumber(""));
        }
    }
}
