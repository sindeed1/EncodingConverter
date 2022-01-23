using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;

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
            AllocConsole();
            IntPtr stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
            FileStream fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            Encoding encoding = System.Text.Encoding.GetEncoding(MY_CODE_PAGE);
            StreamWriter standardOutput = new StreamWriter(fileStream, encoding);
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);

            Console.WriteLine("Console started.");
            Console.WriteLine($"Type '{end}' any time to exit the console.'");

            args = new string[1];
            while (true)
            {
                string read = Console.ReadLine();

                if (read == null || read.Trim().ToLower() == "end")
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

        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool AllocConsole();

        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        private const int STD_OUTPUT_HANDLE = -11;
        private const int MY_CODE_PAGE = 437;

        void ConvertToArguments(string line)
        {

        }
    }

    static class CmdLineToArgvW
    {
        // The previous examples on this page used incorrect
        // pointer logic and were removed.

        public static string[] SplitArgs(string unsplitArgumentLine)
        {
            int numberOfArgs;
            IntPtr ptrToSplitArgs;
            string[] splitArgs;

            ptrToSplitArgs = CommandLineToArgvW(unsplitArgumentLine, out numberOfArgs);

            // CommandLineToArgvW returns NULL upon failure.
            if (ptrToSplitArgs == IntPtr.Zero)
                throw new ArgumentException("Unable to split argument.", new Win32Exception());

            // Make sure the memory ptrToSplitArgs to is freed, even upon failure.
            try
            {
                splitArgs = new string[numberOfArgs];

                // ptrToSplitArgs is an array of pointers to null terminated Unicode strings.
                // Copy each of these strings into our split argument array.
                for (int i = 0; i < numberOfArgs; i++)
                    splitArgs[i] = Marshal.PtrToStringUni(
                        Marshal.ReadIntPtr(ptrToSplitArgs, i * IntPtr.Size));

                return splitArgs;
            }
            finally
            {
                // Free memory obtained by CommandLineToArgW.
                LocalFree(ptrToSplitArgs);
            }
        }

        [DllImport("shell32.dll", SetLastError = true)]
        static extern IntPtr CommandLineToArgvW(
            [MarshalAs(UnmanagedType.LPWStr)] string lpCmdLine,
            out int pNumArgs);

        [DllImport("kernel32.dll")]
        static extern IntPtr LocalFree(IntPtr hMem);
    }
}//namespace
