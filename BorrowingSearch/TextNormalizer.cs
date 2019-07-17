using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorrowingSearch
{
    static class TextNormalizer
    {
        public static string Normalize(string input)
        {
            string output = input.ToLower();
            output = output.Replace('\t', ' ');
            output = output.Replace('\n', ' ');
            output = output.Replace('\r', ' ');
            output = string.Join(" ", output.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            return output;
        }
    }
}
