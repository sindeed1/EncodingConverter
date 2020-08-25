using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            result = result + nameof(ex.Message) + ": " + ex.Message;
            result = result + nameof(ex.StackTrace) + ": " + ex.StackTrace;
            result = result + nameof(ex.InnerException) + ": " + ex.InnerException.Message;

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
                    defaultSwitch(arg);
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
                    defaultSwitch(arg);
            }//i args
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
}
