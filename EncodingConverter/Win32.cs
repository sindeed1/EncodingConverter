using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Win32.SafeHandles;
using System.IO;
using System.Diagnostics;

namespace EncodingConverter
{
    static class Win32Helper
    {
        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool AllocConsole();

        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool AllocConsole();

        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        public static extern int AllocConsole();
        public const int STD_OUTPUT_HANDLE = -11;
        public const int MY_CODE_PAGE = 437;


        public static void StartConsole()
        {
            Trace.WriteLine("Starting console...");
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
        }

    }

    public static class CmdLineToArgvW
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

        public static string[] SplitArgsManaged(string line)
        {
            const char escapeChar = '\\';
            const char quoteChar = '"';

            List<string> arguments = new List<string>();

            string arg = string.Empty;
            bool includeWhiteSpace = false;
            bool escape = false;

            foreach (var item in line)
            {
                if (char.IsWhiteSpace(item))
                {
                    if (!includeWhiteSpace)
                    {
                        if (!string.IsNullOrEmpty(arg))
                        {
                            arguments.Add(arg);
                        }
                        arg = string.Empty;
                        continue;
                    }
                }
                else if (item == quoteChar && !escape)
                {
                    includeWhiteSpace = !includeWhiteSpace;
                    continue;
                }
                else if (item == escapeChar && !escape)
                {
                    escape = true;
                    continue;
                }

                arg += item;
                escape = false;
            }//foreach item

            arguments.Add(arg);

            return arguments.ToArray();
        }

    }

    static class ExceptionWinFormsHelper
    {
        /// <summary>
        /// Shows a <see cref="MessageBox"/> with Yes/No buttons. Yes will copy the exception to the clipboard.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="msg"></param>
        public static void ShowMessageBox(this Exception ex, string msg)
        {
            if (MessageBox.Show(msg + Environment.NewLine + Properties.Resources.Message_Q_Error_DoYouWantToCopyErrorMessage
                , Properties.Resources.Program_Titel
                , MessageBoxButtons.YesNo
                , MessageBoxIcon.Warning
                , MessageBoxDefaultButton.Button1
                ) == DialogResult.Yes)
            {
                Clipboard.SetText(ex.ToText());
            }
        }
    }
}
