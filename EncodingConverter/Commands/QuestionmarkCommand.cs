using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace EncodingConverter.Commands
{
    class QuestionmarkCommand : ICommandLineCommand
    {
        const string CL_Name = "?";

        public string Name => CL_Name;

        public string ShortDescription => "Displays available commands with their short description.";

        public string LongDescription => ShortDescription;

        public int Execute(string[] args, int argsStartIndex)
        {
            if (!args.IsSwitch(argsStartIndex, this.Name))
            //if (!args[argsStartIndex].IsSwitch(CL_Name))
            {
                //Wrong command name:
                return argsStartIndex - 1;
            }

            Console.WriteLine();

            var commands = Program.GetCommands();
            string arg = string.Empty;

            commands.For(this.DisplayHelp);

            Console.WriteLine();
            return argsStartIndex;
        }

        void DisplayHelp(ICommandLineCommand command)
        {
            Console.WriteLine($"'{command.Name}' : {command.ShortDescription}");
            //Console.WriteLine(" " + command.LongDescription);
            Console.WriteLine();
        }

    }
}
