using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
    public static class StringExtensions
    {
        public static bool IsContainsChar(this string text, int minCount)
        {
            var result= text.Select(x=>char.IsLetter(x));

            if (result.Any(x=>x=true))
            {
                return true;
            }

            return false;
        }
    }
}
