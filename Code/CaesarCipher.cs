using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code
{
    public static class CaesarCipher
    {
        
        public static string caesarCipher(string s, int k)
        {
            char[] chars = { 'a', 'b','c' ,'d','e','f','g','h','i', 'j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z'};
            StringBuilder sb = new StringBuilder(); 
            foreach(var c in s)
            {
                if ((c < 'a' || c > 'z') && (c < 'A' || c > 'Z'))
                    sb.Append(c);
                else
                {
                    bool isUper = (c >= 'A' && c <= 'Z');
                    int rotation = isUper ?
                        (c - 'A' + k ) % 26 :
                    (c - 'a' + k) % 26;
                    char n = isUper ? char.ToUpper(chars[rotation]) : chars[rotation];
                    sb.Append(n);
                }
            }
            return sb.ToString();
        }




    }
}
