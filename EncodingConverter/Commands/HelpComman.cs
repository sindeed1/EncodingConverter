using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter.Commands
{
    class HelpComman : ICommandLineCommand
    {
        public string Name => "help";

        public string ShortDescription => "Displays available commands and help";

        public string LongDescription => "";

        public bool Execute(string[] args, int argsStartIndex)
        {
            Console.WriteLine();

            var commands = Program.GetCommands();
            foreach (var command in commands)
            {
                Console.WriteLine(command.Name+ ": "+ command.ShortDescription);
                Console.WriteLine(command.LongDescription);
            }
            Console.WriteLine();
            return true;
        }
    }
}
