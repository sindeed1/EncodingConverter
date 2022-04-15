using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace EncodingConverter.Commands
{
    class HelpCommand : ICommandLineCommand
    {
        const string CL_Name = "help";

        public string Name => CL_Name;

        public string ShortDescription => "Displays available commands with their short and long description.";

        public string LongDescription => $"<{CL_Name}>[ <CommandName>]" +
            $"{Environment.NewLine}    '<CommandName>' Optional, Name of a command to display its help. If it is not provided, help will be displayed for all commands." +
            $"{Environment.NewLine}    Displayed help is of the format:{Environment.NewLine}'<CommandName>' : <short descrpition>{Environment.NewLine} <long description>";

        public int Execute(string[] args, int argsStartIndex)
        {
            if (!args.IsSwitch(argsStartIndex, this.Name))
            {
                //Wrong command name:
                return argsStartIndex - 1;
            }

            Console.WriteLine();

            var commands = Program.GetCommands();
            string arg = string.Empty;

            if (args != null && args.Length > argsStartIndex + 1)
            {
                //There is at least a following argument.
                //Let's get it:
                argsStartIndex++;
                arg = args[argsStartIndex];
            }
            if (arg == null || arg.Trim() == string.Empty)
            {
                commands.For(this.DisplayHelp);
            }
            else
            {
                var command = commands.FirstOrDefault(x => x.Name == arg);
                if (command == null)
                {
                    Console.WriteLine($"No command of name '{arg}' was found!");
                }
                else
                {
                    DisplayHelp(command);
                }
            }

            Console.WriteLine();
            return argsStartIndex;
        }
        void DisplayHelp(ICommandLineCommand command)
        {
            Console.WriteLine($"'{command.Name}' : {command.ShortDescription}");
            Console.WriteLine(" " + command.LongDescription);
            Console.WriteLine();
        }
    }

}
