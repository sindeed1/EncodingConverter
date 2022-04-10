using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace EncodingConverter.Commands
{
    /// <summary>
    /// Command line command that converts the encoding of a given file to another encoding.
    /// </summary>
    class ConvertCommand : ICommandLineCommand
    {
        const string CLARG_SWITCH = "-";//CommandLineArgument
        const string CLARG_InputEncoding = "-ie";// input encoding
        const string CLARG_OutputEncoding = "-oe";// output encoding
        const string CLARG_AutoDetectEncoding = "-ad";// auto detect input encoding
        const string CLARG_PreferredEncoding = "-pe";// preferred encoding
        const string CLARG_Convert = "convert";

        /* Syntax:
         * convert <inputFileName>[ -ie:<inputEncoding>| -ie <inputEncoding>]
         *                        [ -oe:<outputEncoding>| -oe <outputEncoding>]
         *                        [ <outputFileName>]
         *                        [ -ad| -ad:<true|false>| -ad <true|false>]
         *                        [ -pe <preferredEncoding>]
         * 
         * <inputFileName> is the file name and path to the input file.
         * -ie: is the encoding of the input file.
         */
        Func<string[], int, int>[] _CommonCommandLineSwitches;

        public string Name => CLARG_Convert;

        public string ShortDescription => "Converts a text file from a given encoding to another.";

        public string LongDescription => $"<{CLARG_Convert}>" +
            $" <inputFileName>" +
            $" <{CLARG_SWITCH + CLARG_InputEncoding}<encoding.CodePage|.Name>>" +
            $" <{CLARG_SWITCH + CLARG_OutputEncoding}<encoding.CodePage|.Name>>" +
            $" <outputFileName>" +
            $"{Environment.NewLine}    '{CLARG_SWITCH + CLARG_InputEncoding}' is the encoding of the <inputFileName>. Must be present and followed by either code page of the encoding or name of the encoding." +
            $"{Environment.NewLine}    '{CLARG_SWITCH + CLARG_OutputEncoding}' is the encoding of the <outputFileName>. Must be present and followed by either code page of the encoding or name of the encoding." +
            $"{Environment.NewLine}    <inputFileName> file name of the file that needs to be converted. The first argument that is not a switch (i.e. not input or output switch) is interpreted as the input file." +
            $"{Environment.NewLine}    <outputFileName> file name of the output file that will contain the converted encoding. The last argument that is not a switch (i.e. not input or output switch) is interpreted as the output file."
            ;

        public int Execute(string[] args, int argsStartIndex)
        {
            //string switchName = CLARG_Convert;
            if (!args.IsSwitch(argsStartIndex, this.Name))
                //if (!args[0].IsSwitch(switchName))
                return argsStartIndex - 1;

            InitCommonSwitches();

            /* Handling of switches in this command is done in a similar fashion to handling command line:
             * The switches here are the commands. Each switch handler will perform the necessary operations
             * and return. The logic will get the next argument/switch and try to execute it.
             * 
             * The only difference in the current implementation is that switch handling is done in a chained sequence
             * witch means the execution will continue to the next argument until a failure, i.e. no handler found, or
             * the end of arguments is met.
             */
            //args.ProcessCommadLineSwitches(argsStartIndex, _CommonCommandLineSwitches, CommandLine.ProcessNoSwitch);
            argsStartIndex = args.ChainProcessCommandLine(argsStartIndex, _CommonCommandLineSwitches, true, CommandLine.ProcessNoSwitch);

            Program.ECC.Convert();
            return argsStartIndex;
        }


        void InitCommonSwitches()
        {
            if (_CommonCommandLineSwitches != null)
                return;

            //Notice: Handler of no-switch is not included. It will be processed as the default handler
            //witch means it will be executed if no switch matches:
            _CommonCommandLineSwitches = new Func<string[], int, int>[4];
            _CommonCommandLineSwitches[0] = CommandLine.ProcessInputEncodingCLArg;
            _CommonCommandLineSwitches[1] = CommandLine.ProcessOutputEncodingCLArg;
            _CommonCommandLineSwitches[2] = this.ProcessAutoDetectCLArg;
            _CommonCommandLineSwitches[3] = this.ProcessPreferredEncodingCLArg;
        }

        //bool ProcessInputEncodingCLArg(string arg)
        //{
        //    string switchName = CLARG_InputEncoding;
        //    if (!arg.IsSwitch(switchName))
        //        return false;

        //    string switchData;
        //    switchData = arg.GetSwitchData(switchName);//

        //    EncodingInfo encodingInfo;
        //    encodingInfo = GetEncodingInfoFromSwitchData(switchData);
        //    if (encodingInfo == null)
        //    {
        //        Console.WriteLine("Switch InputEncoding '" + switchName + "' does not provide a recognizable code page '" + switchData + "'.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Input encoding '" + encodingInfo.DisplayName + "'.");
        //        Program.ECC.InputEncoding = encodingInfo.GetEncoding();
        //    }

        //    return true;
        //}
        //bool ProcessOutputEncodingCLArg(string arg)
        //{
        //    string switchName = CLARG_OutputEncoding;
        //    if (!arg.IsSwitch(switchName))
        //        return false;

        //    string switchData;
        //    switchData = arg.GetSwitchData(switchName);//

        //    EncodingInfo encodingInfo;
        //    encodingInfo = GetEncodingInfoFromSwitchData(switchData);
        //    if (encodingInfo == null)
        //    {
        //        Console.WriteLine("Switch OutputEncoding '" + switchName + "' does not provide a recognizable code page '" + switchData + "'.");
        //    }
        //    else
        //    {
        //        Console.WriteLine("Output encoding '" + encodingInfo.DisplayName + "'.");
        //        Program.ECC.OutputEncoding = encodingInfo.GetEncoding();
        //    }

        //    return true;
        //}
        //bool ProcessNoSwitch(string arg)
        //{
        //    if (string.IsNullOrWhiteSpace(Program.ECC.InputFilePath))
        //    {
        //        Program.ECC.InputFilePath = arg;
        //    }
        //    else
        //    {
        //        Program.ECC.OutputFilePath = arg;
        //    }
        //    return true;
        //}
        int ProcessAutoDetectCLArg(string[] args, int startingIndex)
        {
            string switchName = CLARG_AutoDetectEncoding;

            string switchData;
            int lastArgsIndex = args.GetSwitchData(startingIndex, switchName, CommandLine.CLARG_DataSeparator, out switchData);
            if (lastArgsIndex < startingIndex)
            {
                //Switch is NOT AutoDetect switch:
                Trace.TraceWarning($"Switch '{args[startingIndex]}' is not auto detect switch '{switchName}'!");
                return lastArgsIndex;
            }

            switchData = switchData.Trim().ToLower();
            if (string.IsNullOrEmpty(switchData))
            {
                //If no data available then it means AutoDetect is on:
                Program.ECC.AutoDetectInputEncoding = true;
                return lastArgsIndex;
            }

            bool autoDetect;
            if (bool.TryParse(switchData, out autoDetect))
            {
                Program.ECC.AutoDetectInputEncoding = autoDetect;
            }
            else
            {
                Console.WriteLine($"Failed to parse switch data '{switchName}' for switch '{switchName}' as boolean!");
            }
            return lastArgsIndex;
        }

        int ProcessPreferredEncodingCLArg(string[] args, int startingIndex)
        {
            string switchName = CLARG_PreferredEncoding;

            string switchData;
            int lastArgsIndex = args.GetSwitchData(startingIndex, switchName, CommandLine.CLARG_DataSeparator, out switchData);

            if (lastArgsIndex < startingIndex)
            {
                //Switch is NOT PreferredEncoding switch:
                Trace.TraceWarning($"Switch '{args[startingIndex]}' is not preferred encoding switch '{switchName}'!");
                return lastArgsIndex;
            }

            Program.ECC.PreferredInputEncoding = switchData;
            return lastArgsIndex;
        }


    }
}
