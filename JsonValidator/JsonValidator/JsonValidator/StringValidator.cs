﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JsonValidator
{
    public class StringValidator
    {
        public static bool IsQuotationMark(char ch)
        {
            return ((ch == '"') ? true : false);
        }

        public static bool IsControlChar(char ch)
        {
            int j = "\n\r\t\b\f".IndexOf(ch);
            if (j >= 0)
                return (false);
            return((ch < 32) ? true : false);
        }

        public static bool IsUnicodeChar(string inputString, int i, int sizeinputString)
        {
            if ((i + 5 < sizeinputString))
            {
                if ((Regex.IsMatch(inputString.Substring(i), "[a-fA-F\\d]{4}")))
                    return (true);
            }
            return (false);
        }

        public static bool ValidateString(string inputString)
        {
            int sizeinputString = inputString.Length - 1;

            if (!(IsQuotationMark(inputString[0]) && IsQuotationMark(inputString[sizeinputString])))
                return (false);
            for (int i = 1; i < sizeinputString; i++)
            {
                if (IsControlChar(inputString[i]))
                    return (false);
                if (inputString[i] == '"')
                    return (false);
                if ((inputString[i] == '\\') && (i + 1 < sizeinputString))
                {
                    int j = "\\nrtbfu/".IndexOf(inputString[i + 1]);
                    if (j <= 0)
                        return (false);
                    if(inputString[i + 1] == 'u' && !(IsUnicodeChar(inputString, i, sizeinputString)))
                        return (false);
                }
                
            }
            return (true);
        }

        public static void Main()
        {
            Console.WriteLine((ValidateString("\"Test\\u0097\nAnother line\"") == true));
            Console.WriteLine((ValidateString("\"abc\"") == true));
            Console.WriteLine((ValidateString("\"abc\"\"") == false));
            Console.WriteLine((ValidateString("\"ab\"c\"") == false));
            Console.WriteLine((ValidateString("\"ab12\\uc\"") == false));

            Console.WriteLine((ValidateString("Test\"") == false));
            Console.WriteLine((ValidateString("\"Test") == false));
            Console.WriteLine((ValidateString("\"\\Test\"") == false));
            Console.WriteLine((ValidateString("\"ab12\\rc\"") == false));
            Console.WriteLine((ValidateString("\"Te\"st\"") == false));

            Console.ReadLine();
        }
    }
}