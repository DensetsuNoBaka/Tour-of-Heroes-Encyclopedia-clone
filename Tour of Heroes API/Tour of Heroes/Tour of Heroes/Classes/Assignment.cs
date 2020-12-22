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

        public string WrathTest(int tests)
        {
            string text = "";
            int round1 = 0, round2 = 0, round3 = 0;
            int low = 9999, high = 0, failures = 0, earlyWins = 0, closeLosses = 0, average = 0;
            int hp = 4800;

            for (int c = 0; c < tests; c++)
            {
                text += $"Test {c+1}\n";
                round1 = testFirstRound(text, out text);
                round2 = testSecondRound(text, out text);
                round3 = testThirdRound(text, out text);
                text += $"Total damage: {round1 + round2 + round3}\n";
                average += round1 + round2 + round3;
                if (low > round1 + round2 + round3) low = round1 + round2 + round3;
                if (high < round1 + round2 + round3) high = round1 + round2 + round3;
                if (round1 + round2 + round3 < hp) failures++;
                if (round1 >= hp || round1 + round2 >= hp) earlyWins++;
                if (hp - round1 - round2 - round3 <= hp / 10 && hp - round1 - round2 - round3 > 0) closeLosses++;
            }

            text += $"Lowest damage: {low}, Highest damage: {high}, Average damage: {average/tests}, Early Wins: {earlyWins}, Close losses: {closeLosses}, Failures: {failures}";
            return text;
        }

        private int testFirstRound(string inText, out string outText)
        {
            Random rnd = new Random();
            int ifiri = rnd.Next(1, 999) + rnd.Next(1, 999), 
                tortle = rnd.Next(1, 999) + rnd.Next(1, 999), 
                hogs = rnd.Next(1, 999), 
                pro = rnd.Next(1, 999);

            outText = inText + $"Round 1: {ifiri}+{tortle}+{hogs}+{pro}={ifiri + tortle + hogs + pro}\n";
            return ifiri + tortle + hogs + pro;
        }

        private int testSecondRound(string inText, out string outText)
        {
            Random rnd = new Random();
            int ifiri = rnd.Next(1, 20),
                tortle = rnd.Next(1, 20),
                hogs = rnd.Next(1, 20),
                pro = rnd.Next(1, 20),
                savingThrow = 10;

            outText = inText + $"Round 2 saving throws: I:{ifiri+4}, T:{tortle+3}, H:{hogs+3}, P:{pro+2}\n";

            ifiri = ifiri == 20 ? 999*2 : ifiri + 4 >= savingThrow ? rnd.Next(1, 999) + rnd.Next(1, 999) : 0;
            tortle = tortle == 20 ? 999*2 : tortle + 3 >= savingThrow ? rnd.Next(1, 999) + rnd.Next(1, 999) : 0;
            hogs = hogs == 20 ? 999 : hogs + 3 >= savingThrow ? rnd.Next(1, 999) : 0;
            pro = pro == 20 ? 999 : pro + 2 >= savingThrow ? rnd.Next(1, 999) : 0;

            outText += $"Round 2: {ifiri}+{tortle}+{hogs}+{pro}={ifiri + tortle + hogs + pro}\n";
            return ifiri + tortle + hogs + pro;
        }

        private int testThirdRound(string inText, out string outText)
        {
            Random rnd = new Random();
            int ifiri = rnd.Next(1, 20),
                tortle = rnd.Next(1, 20),
                hogs = rnd.Next(1, 20),
                pro = rnd.Next(1, 20),
                savingThrow = 12;

            outText = inText + $"Round 3 saving throws: I:{ifiri+4}, T:{tortle+3}, H:{hogs+3}, P:{pro+2}\n";

            ifiri = ifiri == 20 ? 999 * 2 : ifiri + 4 >= savingThrow ? rnd.Next(1, 999) + rnd.Next(1, 999) : 0;
            tortle = tortle == 20 ? 999 * 2 : tortle + 3 >= savingThrow ? rnd.Next(1, 999) + rnd.Next(1, 999) : 0;
            hogs = hogs == 20 ? 999 : hogs + 3 >= savingThrow ? rnd.Next(1, 999) : 0;
            pro = pro == 20 ? 999 : pro + 2 >= savingThrow ? rnd.Next(1, 999) : 0;

            outText += $"Round 3: {ifiri}+{tortle}+{hogs}+{pro}={ifiri + tortle + hogs + pro}\n";
            return ifiri + tortle + hogs + pro;
        }
    }
}
