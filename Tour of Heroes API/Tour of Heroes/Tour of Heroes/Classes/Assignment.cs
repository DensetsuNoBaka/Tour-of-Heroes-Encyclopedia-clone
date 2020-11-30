using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tour_of_Heroes.Classes
{
    public class Assignment
    {
        private readonly Dictionary<char, string> dictionary = new Dictionary<char, string>
        {
            {'0', "zero"},
            {'1', "one"},
            {'2', "two"},
            {'3', "three"},
            {'4', "four"},
            {'5', "five"},
            {'6', "six"},
            {'7', "seven"},
            {'8', "eight"},
            {'9', "nine"}
        };

        public Assignment() { }

        public string ConvertNumber(int number)
        {
            return ConvertNumber(number.ToString());
        }

        public string ConvertNumber(string number)
        {
            string convertedNumber = "";
            for(int c = 0; c < number.Length; c++)
            {
                if (c > 0) convertedNumber += "|";
                convertedNumber += dictionary[number[c]];
            }

            return convertedNumber;
        }
    }
}
