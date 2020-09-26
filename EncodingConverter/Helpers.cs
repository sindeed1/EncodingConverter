using EncodingConverter.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EncodingConverter
{
    static class Helper
    {
        /// <summary>
        /// Searches a given <paramref name="sourceString"/> for the existence of <paramref name="searchStrings"/>.
        /// All the given must be present to return true.
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="searchStrings"></param>
        /// <returns></returns>
        public static bool Contains(this string sourceString, params string[] searchStrings)
        {
            //var searchStrings = searchText.Split(' ');
            //searchText = searchText.ToLower();
            for (int i = 0; i < searchStrings.Length; i++)
            {
                var txt = searchStrings[i];//.ToLower();
                if (!sourceString.Contains(txt))
                    return false;
            }
            return true;
        }

        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
        public static void For<T>(this IList<T> list, Action<T> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                action(list[i]);
            }
        }
        public static void Foreach(this IEnumerable enumerable, Action<object> action)
        {
            foreach (object item in enumerable)
            {
                action(item);
            }
        }
        public static void For(this IList list, Action<object> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                action(list[i]);
            }
        }

    }
    static class ExceptionHelper
    {
        public static void WriteToTrace(this Exception ex)
        {
            Trace.TraceError(ex.ToText());
        }
        public static string ToText(this Exception ex)
        {
            string result = string.Empty;
            result = result + nameof(ex.Message) + ": " + ex?.Message;
            result = result + nameof(ex.StackTrace) + ": " + ex.StackTrace;
            result = result + nameof(ex.InnerException) + ": " + ex.InnerException?.Message;

            return result;
        }
    }
    static class CommandLineHelper
    {
        const string CLARG_SWITCH = "-";

        //public static bool IsSwitch(this string arg, string switchName) { return arg.Substring(1, switchName.Length) == switchName; }
        public static bool IsSwitch(this string arg, string switchName) { return arg.StartsWith(switchName); }
        public static bool IsSwitch(this string arg) { return arg.StartsWith(CLARG_SWITCH); }
        public static string GetSwitchData(this string arg, string switchName)
        {
            string switchData;
            //int switchLength = CLARG_SWITCH.Length + switchName.Length;
            int switchLength = switchName.Length;
            switchData = arg.Substring(switchLength, arg.Length - switchLength);
            return switchData;
        }

        public static void ProcessCommadLineSwitches(this string[] args, int startingArgIndex, Func<string, bool>[] switches, Func<string, bool> defaultSwitch)
        {
            for (int i = startingArgIndex; i < args.Length; i++)
            {
                string arg = args[i].ToLower();
                if (!arg.IsSwitch())
                {
                    defaultSwitch?.Invoke(arg);
                    ////If it's not a switch then it should be the input file:
                    //this._InputFilePath = arg;
                    continue;
                }

                bool switchFound = false;
                for (int j = 0; j < switches.Length; j++)
                {
                    if (switches[j](arg))
                    {
                        switchFound = true;
                        break;
                    }
                }//j _CommandLineSwitches

                if (!switchFound)
                    defaultSwitch?.Invoke(arg);
            }//i args
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>Command line is something like this:
        /// [command] [filepath] </remarks>
        public static bool ProcessCommandLine(this string[] args, Func<ICommandLineCommand[]> getCommands, ICommandLineCommand defaultCommand)
        {
            Console.WriteLine("Processing command line...");

            if (args == null || args.Length <= 0)
            {
                Console.WriteLine("No command line arguments.");
                return ProcessDefaultCommand(args, defaultCommand);
            }

            ICommandLineCommand[] commands;
            commands = getCommands();

            if (commands == null || commands.Length <= 0)
            {
                Console.WriteLine("Command list not available.");
                return ProcessDefaultCommand(args, defaultCommand);
            }

            string commandName = args[0].Trim().ToLower();
            ICommandLineCommand cmd;
            cmd = commands.FirstOrDefault(x => x.Name.ToLower() == commandName);
            if (cmd == null)
            {
                return ProcessDefaultCommand(args, defaultCommand);
            }

            cmd.Execute(args, 1);

            return true;
        }

        static bool ProcessDefaultCommand(string[] args, ICommandLineCommand defaultCommand)
        {
            Console.WriteLine("No default command line. Further processing is not possible.");
            Trace.TraceInformation("Processing default command '");

            if (defaultCommand == null)
            {
                Console.WriteLine("No default command line. Further processing is not possible.");
                return false; ;
            }

            if (args == null || args.Length <= 0)
            {
                Trace.TraceInformation("Command line arguments are not provided.");
                Trace.TraceInformation("Execute the default command with no arguments");
                defaultCommand.Execute(args, 0);
            }
            else
            {
                if (args[0].Trim().ToLower() == defaultCommand.Name.Trim().ToLower())
                {
                    Trace.TraceInformation("Execute default command with first argument as command name...");
                    defaultCommand.Execute(args, 1);
                }
                else
                {
                    Trace.TraceInformation("Execute default command with no command name in the arguments...");
                    defaultCommand.Execute(args, 0);
                }
            }

            return true;
        }

        //public static Func<string[], bool> ProcessCommandLine(string[] args, Func<string[], bool>[] commands, Func<string[], bool> defaultCommand)
        //{
        //    Console.WriteLine("Processing command line...");
        //    if (args == null || args.Length <= 0)
        //    {
        //        Console.WriteLine("No command line arguments.");
        //        return null;
        //    }

        //    for (int i = 0; i < commands.Length; i++)
        //    {
        //        if (commands[i](args))
        //        {
        //            break;
        //        }
        //    }//for i

        //    if (_CommandLineCommand != null)
        //        return;

        //    //No Command was found. Load the switches:
        //    InitCommonSwitches();

        //    args.ProcessCommadLineSwitches(0, _CommonCommandLineSwitches, this.ProcessNoSwitch);
        //}


    }

    static class EncodingHelper
    {
        static object[] _OutputPathFormattingParameters;

        public static string FormatOutputpath(string inputPath, Encoding inputEncoding, Encoding outputEncoding)
        {
            if (_OutputPathFormattingParameters == null)
            {
                _OutputPathFormattingParameters = new object[23];
            }

            string formatString = Program.Settings.OutputFilePathFormatString;
            FileInfo file = new FileInfo(inputPath);
            string directory = file.DirectoryName;
            Trace.TraceInformation("Old current directory '" + Directory.GetCurrentDirectory() + "'");
            Directory.SetCurrentDirectory(directory);
            Trace.TraceInformation("New current directory '" + Directory.GetCurrentDirectory() + "'");

            string fileExtention = file.Extension;
            string fileName = file.Name;
            fileName = fileName.Substring(0, fileName.Length - fileExtention.Length);
            fileExtention = fileExtention.TrimStart('.');

            _OutputPathFormattingParameters[0] = directory;                     //{0} directory path
            _OutputPathFormattingParameters[1] = fileName;                      //{1} file name without extension
            _OutputPathFormattingParameters[2] = fileExtention;                 //{2} extension
                                                                                //{3-9} reserved and empty
            if (outputEncoding != null)
            {
                _OutputPathFormattingParameters[10] = outputEncoding.EncodingName;   //{10} Output encoding name
                _OutputPathFormattingParameters[11] = outputEncoding.BodyName;       //{11} Output encoding Body name
                _OutputPathFormattingParameters[12] = outputEncoding.CodePage;       //{12} Output encoding Code page
                                                                                     //{13-19} reserved and empty
            }
            if (inputEncoding != null)
            {
                _OutputPathFormattingParameters[20] = inputEncoding.EncodingName;    //{20} Input encoding name
                _OutputPathFormattingParameters[21] = inputEncoding.BodyName;        //{21} Input encoding Body name
                _OutputPathFormattingParameters[22] = inputEncoding.CodePage;        //{22} Input encoding Code page
            }

            string result;
            result = string.Format(formatString, _OutputPathFormattingParameters);
            //, directory                     //{0} directory path
            //, fileName                      //{1} file name without extension
            //, fileExtention                 //{2} extension
            //, "", "", "", "", "", "", ""    //{3-9} reserved and empty
            //, outputEncoding.EncodingName   //{10} Output encoding name
            //, outputEncoding.BodyName       //{11} Output encoding Body name
            //, outputEncoding.CodePage       //{12} Output encoding Code page
            //, "", "", "", "", "", "", ""    //{13-19} reserved and empty
            //, inputEncoding.EncodingName   //{20} Input encoding name
            //, inputEncoding.BodyName       //{21} Input encoding Body name
            //, inputEncoding.CodePage       //{22} Input encoding Code page
            ////, "", "", "", "", "", "", ""    //{23-29} reserved and empty
            //);

            return result;
        }

    }
}
