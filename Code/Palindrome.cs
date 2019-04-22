using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class Palindrome
    {
        public static int palindromeIndex(string s)
        {
            if (isPalinDrome(s))
                return -1;
            for (int i = 0; i < s.Length; i++)
            {
                if (s[0 + i] != s[s.Length - 1 - i])
                {
                    if (isPalinDrome(s.Substring(i + 1, (s.Length - 1 - i)-(i ))))
                        return i;
                    if (isPalinDrome(s.Substring(i , (s.Length - 1 - i) - (i))))
                        return s.Length - 1 - i ;
                }
            }
            return -1;
        }
        private static bool isPalinDrome(string s)
        {
            if(s.Length <= 1)
                return true;
            for(int i = 0; i < s.Length/2; i++)
            {
                if (s[0 + i] != s[s.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }
    }
}
