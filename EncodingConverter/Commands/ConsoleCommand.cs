using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter.Commands
{
    class ConsoleCommand : ICommandLineCommand
    {
        const string end = "end";
        public string Name => "console";

        public string ShortDescription => "Displays console and allows execution of internal commands.";

        public string LongDescription => "'end' is an internal command to console. Type 'end' while in console to exit the console.";

        public bool Execute(string[] args, int argsStartIndex)
        {
            Console.WriteLine("Starting console...");
            Win32Helper.AllocConsole();
            IntPtr stdHandle = Win32Helper.GetStdHandle(Win32Helper.STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            Encoding encoding = System.Text.Encoding.GetEncoding(Win32Helper.MY_CODE_PAGE);
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding)
            {
                AutoFlush = true
            };
            Console.SetOut(standardOutput);

            Console.WriteLine("Console started.");
            Console.WriteLine($"Type '{end}' any time to exit the console.'");

            //Any arguments that comes after the 'console' command name are interpreted as
            //a new command line that should be executed in the console.
            //This behavior helps when wanting to start the UI in console mode.
            //In this case then we call the program like this:
            //enco.exe console showui
            //This will start the console and then directly execute the command 'showui' from inside it.
            //Thus, we start the console and then the UI from a single command line.

            string allAfterArgs = string.Empty;
            for (int i = argsStartIndex; i < args.Length; i++)
            {
                allAfterArgs += args[i];
            }
            if (allAfterArgs != String.Empty)
            {
                Console.WriteLine(allAfterArgs);
                args.ProcessCommandLine(Program.GetCommands, null, argsStartIndex);
            }

            args = new string[1];
            while (true)
            {
                string read = Console.ReadLine();

                if (read == null || read.Trim().ToLower() == end)
                {
                    break;
                }

                try
                {
                    args = CmdLineToArgvW.SplitArgs(read);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Couldn't convert the given line '{read}' to argument!");
                    Console.WriteLine(ex.Message);
                    Console.WriteLine($"Please try again, or type '{end}' to exit.");
                    continue;
                }
                if (args == null)
                {
                    Console.WriteLine("Please enter a command name.");
                }
                else
                {
                    args.ProcessCommandLine(Program.GetCommands, null);
                }
                //if (args.ProcessCommandLine(Program.GetCommands, null))
                //    {
                //    break;
                //}
            }
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine("Ending console.");
            return true;
        }


    }

}//namespace
