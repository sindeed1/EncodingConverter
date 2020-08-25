using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter.Commands
{
    class ConvertCommand : ICommandLineCommand
    {
        const string CLARG_SWITCH = "-";
        const string CLARG_InputEncoding = "ie:";
        const string CLARG_OutputEncoding = "oe:";
        const string CLARG_Convert = "convert";

        public string Name => CLARG_Convert;

        public string ShortDescription => "Converts a text file from a given encoding to another.";

        public string LongDescription => "";

        public bool Excute(string[] args)
        {
            throw new NotImplementedException();
        }

    }
}
