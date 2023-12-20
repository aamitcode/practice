using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace src
{
    public class SumDigits
    {
        public static int Sum(int num)
        {
            if (num < 10)
            {
                return num;
            }
            else
            {
                int sum = 0;
                while (num > 0)
                {
                    sum += num % 10;
                    num /= 10;
                }
                return Sum(sum);
            }
        }
    }
}
