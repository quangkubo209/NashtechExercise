using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFundamental_Day2
{
    internal class PrimeNumber
    {
        public async Task<bool> checkPrimeAsync(int number)
        {
            if (number < 2)
            {
                return false;
            }

            int sqrt = (int)Math.Sqrt(number);
            for (int i = 2; i <= sqrt; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
                await Task.Delay(5);
            }

            return true;
        }

        public async Task<List<int>> GetPrimeNumberAsync(int start, int end)
        {
            List<int> primeNumbers = new List<int>();
            for (int i = start; i <= end; i++)
            {
                if (await checkPrimeAsync(i))
                { 
                    primeNumbers.Add(i); 
                }
            }
            return primeNumbers;
        }


    }
}
