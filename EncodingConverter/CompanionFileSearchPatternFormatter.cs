using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter
{
    //, directory                     //{0} directory path
    //, fileName                      //{1} file name without extension
    //, fileExtention                 //{2} extension

    /// <summary>
    /// Formats the companion file search pattern using parameters from search pattern.
    /// </summary>
    /// <remarks>The <see cref="CompanionFileSearchPatternFormatter"/> is an attached sub-system that uses the 
    /// <see cref="EncodingConverterCore"/> as one of the sources. It will update the formated output path back to the
    /// <see cref="EncodingConverterCore"/>.</remarks>
    class CompanionFileSearchPatternFormatter
    {
        static char[] _SplitChars = { '|' };

        public static string PARAM_InputFile_DirectoryPath = "{0}";// directory path;
        public static string PARAM_InputFile_FileNameWithoutExtention = "{1}";// file name without extension
        public static string PARAM_InputFile_Extention = "{2}";// extension

    }
}
