using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace EncodingConverter.Commands
{
    class ConsoleCommand : ICommandLineCommand
    {
        const string end = "end";
        public string Name => "console";

        public string ShortDescription => "Displays console and allows execution of internal commands.";

        public string LongDescription => "'end' is an internal command to console. Type 'end' while in console to exit the console.";

        public int Execute(string[] args, int argsStartIndex)
        {
            //if (args[argsStartIndex].Trim().ToLower() != this.Name)
            if (!args.IsSwitch(argsStartIndex, this.Name))
            {
                return argsStartIndex - 1;
            }

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

            Console.Write("Done.");
            Console.WriteLine($"Type '{end}' any time to exit the console.'");

            //Any arguments that comes after the 'console' command name are interpreted as
            //a new command line that should be executed in the console.
            //This behavior helps when wanting to start the UI in console mode.
            //In this case then we call the program like this:
            //enco.exe console showui
            //This will start the console and then directly execute the command 'showui' from inside it.
            //Thus, we start the console and then the UI from a single command line.

            string allAfterArgs = string.Empty;

            int lastUsedArgIndex = args.Length;
            //if (allAfterArgs.Trim() != String.Empty)
            if (args.Length > argsStartIndex + 1)//Is there any arguments after the startingArg to process directly?
            {
                //Combine all the after arguments into a single string to be written to console:
                for (int i = argsStartIndex; i < args.Length; i++)
                    allAfterArgs += args[i] + " ";

                Console.WriteLine(allAfterArgs);
                //args.ProcessCommandLine(argsStartIndex + 1, Program.GetCommands().Select<ICommandLineCommand, Func<string[], int, int>>(x => x.Execute).ToArray(), null, out commandIndex);
                //Process the rest of the arguments as a new command line:
                args.ProcessCommandLine(argsStartIndex + 1, Program.GetCommands());
            }

            ////args.ProcessCommandLine(argsStartIndex, Program.GetCommands().Select<ICommandLineCommand, Func<string[], int, int>>(x => x.Execute).ToArray(), out commandIndex);
            //args.ProcessCommandLine(argsStartIndex, Program.GetCommands(), out commandIndex);

            //Now start the console loop to execute commands:
            args = new string[1];
            while (true)
            {
                //Read line:
                string read = Console.ReadLine();

                //Check internal command 'end':
                if (read == null || read.Trim().ToLower() == end)
                {
                    break;
                }

                //Get arguments from the entered line:
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
                //Check the arguments if valid:
                if (args == null || args.Length == 0)
                {
                    Console.WriteLine("Please enter a command name.");
                }
                else
                {
                    //Process and execute the arguments:
                    lastUsedArgIndex = args.ProcessCommandLine(0, Program.GetCommands());
                    if (lastUsedArgIndex <= -1)
                    {
                        Console.WriteLine($"Wrong command name. '{args[0]}' is not recognized as internal or external command!");
                    }
                }
            }//loop

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
            Console.WriteLine("Ending console.");
            return args.Length - 1;
        }


    }

}//namespace
