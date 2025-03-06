using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode
{
    internal static class Helpers
    {
        public static Func<int, Func<int, Func<int, int>>> Curry(this Func<int, int, int, int> func) => a => b => c => func(a, b, c); 

        public static Func<int, int> Apply(this Func<int, int, int> func, int value) => (a) => func(value, a);
        public static Func<int, int, int> Apply(this Func<int, int, int, int> func, int value) => (a,b) => func(value, a,b);
        public static Func<int> Apply(this Func<int, int> func, int value) => () => func(value);
    }
}
