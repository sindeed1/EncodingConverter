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
        public string Name => "console";

        public string ShortDescription => "Displays console and allows execution of internal commands.";

        public string LongDescription => "";

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
            Console.WriteLine("Encoding Converter");

            args = new string[1];
            while (true)
            {
                string read = Console.ReadLine();

                if (read == null || read.Trim().ToLower() == "end")
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

    }
}//namespace
