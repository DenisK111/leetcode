using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Easy
{
    /// <summary>
    /// 3174.
    /// </summary>
    internal class ClearDigitsProblem
    {


        public string ClearDigits(string s)
        {          
            byte toDelete = 0;
            var sb = new StringBuilder();

            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] >= '0' && s[i] <= '9')
                {                   
                    toDelete++;
                }

                else if (toDelete > 0)
                {                    
                    toDelete--;
                }
                else
                {
                    sb.Insert(0, s[i]);
                }
            }

            return sb.ToString();

        }
    }
}
