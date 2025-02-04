using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Medium
{
    /// <summary>
    /// 12
    /// </summary>
    internal class IntegerToRoman
    {
        private (int remainder, string roman) Convert(int num) => num switch
        {
            >= 1000 => (num - 1000, "M"),
            900 => (0, "CM"),
            >= 500 => (num - 500, "D"),
            400 => (0, "CD"),
            >= 100 => (num - 100, "C"),
            90 => (0, "XC"),
            >= 50 => (num - 50, "L"),
            40 => (0, "XL"),
            >= 10 => (num - 10, "X"),
            9 => (0, "IX"),
            >= 5 => (num - 5, "V"),
            4 => (0, "IV"),
            >= 1  => (num - 1, "I"),
            _ => (0,"")
        }; 

        public string IntToRoman(int num)
        {
            if (num < 1 || num > 3999) throw new ArgumentException("Invalid Input");            
            var roman = new StringBuilder();            

            for (int i = 0; i < 4; i++)
            {
                var remainder = num % 10;
                roman = ConvertToRoman(remainder, i).Append(roman);                
                num = num / 10;
            }    

            return roman.ToString();            
        }

        private StringBuilder ConvertToRoman(int number, int power)
        {                      
            var sb = new StringBuilder();
            int num = number * (int)Math.Pow(10, power);            

            while(num > 0)
            {
                (int remainder, string romanNum) = Convert(num);
                sb.Append(romanNum);
                num = remainder;
            }

            return sb;
        }

        [TestCase(3749, "MMMDCCXLIX")]
        [TestCase(58, "LVIII")]
        [TestCase(1994, "MCMXCIV")]
        [TestCase(1004, "MIV")]

        public void Test(int number, string expected)
        {
            var result = IntToRoman(number);
            result.ShouldBe(expected);
        }
    }
}
