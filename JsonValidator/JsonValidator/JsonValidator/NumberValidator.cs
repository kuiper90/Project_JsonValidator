using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonValidator
{
    public class NumberValidator
    {
        public struct Count
        {
            public int countPeriods;
            public int countE;
            public int eIndex;
            public int periodIndex;

            public Count(int countPeriods, int countE, int eIndex, int periodIndex)
            {
                this.countPeriods = countPeriods;
                this.countE = countE;
                this.eIndex = eIndex;
                this.periodIndex = periodIndex;
            }
        }

        public static bool IsNegativeSign(char ch)
        {
            return (ch == '-');
        }

        public static bool IsPositiveSign(char ch)
        {
            return (ch == '+');
        }

        public static bool IsZero(char ch)
        {
            return (ch == '0');
        }

        public static bool IsDigit(char ch)
        {
            return (((ch >= '0') && (ch <= '9')));
        }

        public static Count CountPeriods(string inputString, int sizeInputString)
        {
            Count countInput = new Count(0, 0, -1, -1);

            for (int i = 0; i < sizeInputString; i++)
            {
                if (inputString[i] == '.')
                {
                    countInput.periodIndex = i;
                    countInput.countPeriods++;
                }
                else if ((inputString[i] == 'e') || (inputString[i] == 'E'))
                {
                    countInput.eIndex = i;
                    countInput.countE++;
                }
                if ((countInput.countE > 1) || (countInput.countE > 1))
                    break;
            }
            return (countInput);
        }      

        public static bool CheckIfOnlyDigits(string inputString, int startPos, int endPos)
        {
            while (startPos < endPos)
            {
                if (!IsDigit(inputString[startPos++]))
                    return (false);
            }
            return (true);
        }

        public static bool CheckOnePeriod(string inputString, int startPos, int endPos)
        {
            int currentPos = startPos;
           
            if (IsZero(inputString[currentPos]))
            {
                if (inputString[currentPos + 1] != '.' || IsDigit(inputString[currentPos + 1]))
                    return (false);
                currentPos = currentPos + 2;
            }
            while (currentPos < endPos)
            {
                if (!IsDigit(inputString[currentPos]) && inputString[currentPos] != '.')
                    return (false);
                currentPos++;
            }
            return (true);
        }

        public static bool CheckNoPeriod(string inputString, int startPos, int endPos)
        {
            int currentPos = startPos;

            if (IsZero(inputString[currentPos]) && (endPos - startPos > 1))
                return (false);
            return (CheckIfOnlyDigits(inputString, currentPos, endPos));
        }

        public static bool CheckBeforeExp(string inputString, int endPos)
        {
            int currentPos = 0;
            Count countInput = CountPeriods(inputString, inputString.Length);

            if (IsNegativeSign(inputString[currentPos]))
                currentPos++;
            if (countInput.countPeriods == 1)
            {
                if (!IsDigit(inputString[countInput.periodIndex + 1]))
                    return (false);
                return CheckOnePeriod(inputString, currentPos, endPos);
            }
            else if (countInput.countPeriods == 0)
                return CheckNoPeriod(inputString, currentPos, endPos);
            else
                return (false);
        }

        public static bool CheckAfterExp(string inputString, int eIndex)
        {
            int currentPos = eIndex + 1;
           
            if (IsPositiveSign(inputString[currentPos]) || IsNegativeSign(inputString[currentPos]))
                currentPos++;
            if (currentPos > inputString.Length)
                return (false);
            return (CheckIfOnlyDigits(inputString, currentPos, inputString.Length));           
        }

        public static bool ValidateNumber(string inputString)
        {
            Count countInput = CountPeriods(inputString, inputString.Length);

            if (countInput.eIndex == 0 || countInput.periodIndex == 0)
                return (false);
            if (countInput.eIndex == inputString.Length - 1 || countInput.periodIndex == inputString.Length - 1)
                return (false);

            if (countInput.countE == 1)
            { 
                return (CheckBeforeExp(inputString, countInput.eIndex) && CheckAfterExp(inputString, countInput.eIndex));
            }
            else if (countInput.countE == 0)
            {
                return (CheckBeforeExp(inputString, inputString.Length));
            }
            else
                return (false);
        }
    }
}