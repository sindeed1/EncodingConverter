using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter
{
    static class Helper
    {
        /// <summary>
        /// Searches a given <paramref name="sourceString"/> for the existence of <paramref name="searchStrings"/>.
        /// All the given must be present to return true.
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="searchStrings"></param>
        /// <returns></returns>
        public static bool Contains(this string sourceString,params string[] searchStrings)
        {
            //var searchStrings = searchText.Split(' ');
            //searchText = searchText.ToLower();
            for (int i = 0; i < searchStrings.Length; i++)
            {
                var txt = searchStrings[i];//.ToLower();
                if (!sourceString.Contains(txt))
                    return false;
            }
            return true;
        }

    }
}
