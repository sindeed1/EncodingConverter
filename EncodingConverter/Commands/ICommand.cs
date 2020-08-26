using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter.Commands
{
    interface ICommandLineCommand
    {
        string Name { get; }
        string ShortDescription { get; }
        string LongDescription { get; }

        bool Execute(string[] args, int argsStartIndex);
    }
}
