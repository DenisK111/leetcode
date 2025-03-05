using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Leeetcode.Problems.Medium
{
    /// <summary>
    /// 1116
    /// </summary>
    public class ZeroEvenOdd
    {
        private int n;         
        private SemaphoreSlim semaphoreZero = new(1);
        private SemaphoreSlim semaphoreEven = new(0);
        private SemaphoreSlim semaphoreOdd = new(0);

        public ZeroEvenOdd() { }

        public ZeroEvenOdd(int n)
        {
            this.n = n;
        }     

        // printNumber(x) outputs "x", where x is an integer.
        public void Zero(Action<int> printNumber)
        {
            for (int i = 1; i <= n; i++)
            {
                semaphoreZero.Wait();
                printNumber(0);
                if (i % 2 != 0)
                {
                    semaphoreOdd.Release();
                }
                else
                {
                    semaphoreEven.Release();
                }

            }
        }

        public void Even(Action<int> printNumber)
        {
            for(int i = 2;i <= n; i+=2)
            {
                semaphoreEven.Wait();
                printNumber(i);
                semaphoreZero.Release();
            }
        }

        public void Odd(Action<int> printNumber)
        {
            for (int i = 1; i <= n; i += 2)
            {
                semaphoreOdd.Wait();
                printNumber(i);
                semaphoreZero.Release();
            }
        }     
        
    }


}
