namespace JsonValidator
{
    public class NumberValidator
    {
        public static bool IsJsonNumber(string input)
            => !IsNullOrEmpty(input) && IsNumber(input);

        static bool IsNumber(string input)
        {
            int i = 0;
            if (IsSign(input[0]))
            {
                i = 1;
            }

            if (input.IndexOf('.') >= 0)
            {
                return IsRational(input, i);
            }

            return IsNatural(input, i);
        }

        static bool IsNatural(string input, int i)
        {
            if (IsZero(input[i]) && i < input.Length - 1)
            {
                return false;
            }

            return SequenceValidator(input, i);
        }

        static bool IsRational(string input, int i)
            => IntegerPartValidator(input, ref i) &&
            i != input.Length &&
            FractionalPartValidator(input, ref i);

        static bool IntegerPartValidator(string input, ref int i)
        {
            if (input[i] == '0' && !IsPeriodContext(input, i + 1))
            {
                return false;
            }

            while (input[i] != '.')
            {
                if (!IsDigit(input[i]))
                {
                    return false;
                }

                i++;
            }

            i++;
            return true;
        }

        static bool FractionalPartValidator(string input, ref int i)
        {
            if (i == input.Length)
            {
                return false;
            }

            return SequenceValidator(input, i);
        }

        private static bool SequenceValidator(string input, int i)
        {
            int posExp = GetExponentIndex(input);
            if (posExp != -1)
            {
                if (!CheckDigitSequence(input, ref i, posExp) || i == input.Length - 1)
                {
                    return false;
                }

                if (IsSign(input[++i]) && i < input.Length - 1)
                {
                    i++;
                }
            }

            return CheckDigitSequence(input, ref i, input.Length);
        }

        static bool CheckDigitSequence(string input, ref int i, int end)
        {
            while (i < end)
            {
                if (!IsDigit(input[i]))
                {
                    return false;
                }

                i++;
            }

            return true;
        }

        static int GetExponentIndex(string input)
            => input.IndexOfAny(new[] { 'e', 'E' });

        static bool IsNullOrEmpty(string input)
            => string.IsNullOrEmpty(input);

        static bool IsDigit(char chr)
            => chr >= '0' && chr <= '9';

        static bool IsZero(char chr)
            => chr == '0';

        static bool IsPeriod(char chr)
            => chr == '.';

        static bool IsSign(char chr)
            => chr == '-' || chr == '+';

        static bool IsPeriodContext(string input, int i)
            => IsPeriod(input[i]) && i + 1 < input.Length && IsDigit(input[i + 1]);
    }
}