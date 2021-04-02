using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace JsonValidator
{
    public class StringValidator
    {
        const int UnicodeCharLength = 5;
        const int EscapedCharLength = 2;

        public static bool IsJsonString(string input)
            => !IsNullOrEmpty(input) &&
                IsDoubleQuoted(input) &&
                ContainsSpecialCharacters(input);

        static bool IsDoubleQuoted(string input)
        {
            int textSize = input.Length - 1;
            return textSize > 0 &&
                   IsQuotationMark(input[0]) &&
                   IsQuotationMark(input[textSize]);
        }

        static bool IsQuotationMark(char chr)
            => chr == '"';

        static bool IsNullOrEmpty(string input)
            => string.IsNullOrEmpty(input);

        static bool ContainsSpecialCharacters(string input)
        {
            int i = 0;
            while (i < input.Length)
            {
                if (IsReverseSolidus(input[i]) && i + 1 < input.Length)
                {
                    if (!TryParseEscapedCharacter(input, ref i))
                    {
                        return false;
                    }
                }
                else
                {
                    if (IsFakeControlCharacter(input[i]))
                    {
                        return false;
                    }

                    i++;
                }
            }

            return true;
        }

        private static bool TryParseEscapedCharacter(string input, ref int i)
        {
            if (IsUnicodeCharacter(input[i + 1]))
            {
                return EscapedCharacterValidator(input, ref i, UnicodeCharLength, IsUnicodeContext);
            }

            return EscapedCharacterValidator(input, ref i, EscapedCharLength, IsValidEscapedCharacter);
        }

        private static bool EscapedCharacterValidator(string input, ref int i, int charLength, Func<string, int, bool> tryParseCharacter)
        {
            if (tryParseCharacter(input, i))
            {
                i += charLength;
            }
            else
            {
                return false;
            }

            return true;
        }

        private static bool IsUnicodeContext(string input, int i)
        {
            const int unicodeCharLength = 5;
            return i + unicodeCharLength < input.Length - 1 &&
                IsUnicodeCharacter(input.Substring(i + 1, i + unicodeCharLength));
        }

        static bool IsUnicodeCharacter(char chr)
            => chr == 'u';

        static bool IsReverseSolidus(char chr)
            => chr == '\\';

        static bool IsValidEscapedCharacter(string input, int i)
            => IsQuotationMarkContext(input, i) || "nrtbf/\\".IndexOf(input[i + 1]) >= 0;

        static bool IsQuotationMarkContext(string input, int i)
        {
            const int controlCharLength = 2;
            return IsQuotationMark(input[i + 1]) && i + controlCharLength < input.Length;
        }

        static bool IsFakeControlCharacter(char chr)
        {
            const string parts = "\n\r\b\f\t";
            return parts.Contains(chr);
        }

        static bool IsUnicodeCharacter(string text)
        {
            const string pattern = "u[a-fA-F0-9]{4}";
            return Regex.IsMatch(text, pattern);
        }
    }
}