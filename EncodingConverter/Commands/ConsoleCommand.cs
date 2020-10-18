using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter.Commands
{
    class ConsoleCommand : ICommandLineCommand
    {
        public string Name => "console";

        public string ShortDescription => "";

        public string LongDescription => "";

        public bool Execute(string[] args, int argsStartIndex)
        {
            Console.WriteLine("Encoding Converter");
            Console.WriteLine("Starting console");

            args = new string[1];
            while (true)
            {
                string read = Console.ReadLine();

                if (read?.Trim().ToLower() == "end")
                {
                    break;
                }

                args[0] = read;
                args.ProcessCommandLine(Program.GetCommands, null);
                //if (args.ProcessCommandLine(Program.GetCommands, null))
                //    {
                //    break;
                //}
            }
            
            Console.WriteLine("Ending console.");
            return true;
        }
    }
}
