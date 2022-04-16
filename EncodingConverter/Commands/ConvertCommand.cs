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
        //const string CLARG_SWITCH = "-";//CommandLineArgument
        public const string CLARG_InputEncoding = CommandLine.CLARG_InputEncoding;// "-ie";// input encoding
        public const string CLARG_OutputEncoding = CommandLine.CLARG_OutputEncoding;// "-oe";// output encoding
        public const string CLARG_AutoDetectEncoding = "-ad";// auto detect input encoding
        public const string CLARG_PreferredEncoding = "-pe";// preferred encoding
        public const string CLARG_OutputPathFormat = "-outform";// output path format

        public const string CLARG_CompFile = "-cf";// companion file
        public const string CLARG_CompFileSearchPattern = "-cfsearch";// companion file search pattern

        public const string CLARG_Name = "convert";

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

        public string Name => CLARG_Name;

        public string ShortDescription => "Converts a text file from a given encoding to another.";

        public string LongDescription => $"<{this.Name}>" +
            $" <inputFileName>" +
            $"[ <{CLARG_InputEncoding} <encoding.CodePage|.Name>>]" +
            $"[ <{CLARG_OutputEncoding} <encoding.CodePage|.Name>>]" +
            $"[ <outputFileName>|<{CLARG_OutputPathFormat} <outputPathFormat>>" +
            $"{Environment.NewLine}    '{CLARG_InputEncoding}' optional, is the encoding of the <inputFileName>. When present must be followed by either code page of the encoding or name of the encoding." +
                                        $" If not present, logic will try to detect the encoding of the input file." +
            $"{Environment.NewLine}    '{CLARG_OutputEncoding}', optional, is the encoding of the <outputFileName>. When present must be followed by either code page of the encoding or name of the encoding." +
                                        $" Default is {Encoding.UTF8.HeaderName}" +
            $"{Environment.NewLine}    '{CLARG_OutputPathFormat}' conditionally optional, is the format of output file to generate the output file path. Either this or '<outputFileName>' must be present!" +
            $"{Environment.NewLine}    <inputFileName> obligatory, file name of the file that needs to be converted. The first argument that is not a switch (i.e. not input or output switch) is interpreted as the input file." +
            $"{Environment.NewLine}    <outputFileName> conditionally optional, file name of the output file that will contain the converted encoding. The last argument that is not a switch (i.e. not input or output switch)" +
                                        $"is interpreted as the output file. If not present then switch '{CLARG_OutputPathFormat}' followed by <output file format> must be present!"
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
            //Set defaults:
            Program.ECC.OutputEncoding = Encoding.UTF8;
            Program.ECC.InputEncoding = null;
            Program.ECC.InputFilePath = null;

            if (_OPF != null)
            {
                _OPF.FormatString = "";
                _OPF.CompanionFileSearchPattern = "";
                _OPF.CompanionFile = "";
            }

            //Process the arguments, witch will also load the new values for ECC:
            argsStartIndex = args.ChainProcessCommandLine(argsStartIndex + 1, _CommonCommandLineSwitches, true, CommandLine.ProcessNoSwitch);

            if (Program.ECC.InputEncoding == null)
            {
                Program.ECC.DetectInputEncoding();
                if (Program.ECC.InputEncoding == null)
                {
                    Console.WriteLine("Error! Input encoding is not provided and could not detect input encoding!");
                    return argsStartIndex;
                }
            }

            //Execute the conversion:
            Program.ECC.Convert();


            //Clean up:
            //if (_OPF != null)
            //{
            //    _OPF.ECC = null;
            //    _OPF = null;
            //}

            return argsStartIndex;
        }


        void InitCommonSwitches()
        {
            if (_CommonCommandLineSwitches != null)
                return;

            //Notice: Handler of no-switch is not included. It will be processed as the default handler
            //witch means it will be executed if no switch matches:
            _CommonCommandLineSwitches = new Func<string[], int, int>[7];
            int i = 0;
            _CommonCommandLineSwitches[i++] = CommandLine.ProcessInputEncodingCLArg;
            _CommonCommandLineSwitches[i++] = CommandLine.ProcessOutputEncodingCLArg;
            _CommonCommandLineSwitches[i++] = this.ProcessAutoDetectCLArg;
            _CommonCommandLineSwitches[i++] = this.ProcessPreferredEncodingCLArg;
            _CommonCommandLineSwitches[i++] = this.ProcessOutputPathFormatCLArg;
            _CommonCommandLineSwitches[i++] = this.ProcessCompanionFileSearchPatternCLArg;
            _CommonCommandLineSwitches[i++] = this.ProcessCompanionFileCLArg;
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
                Trace.TraceWarning($"Argument '{args[startingIndex]}' is not auto detect switch '{switchName}'!");
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
                Trace.TraceWarning($"Argument '{args[startingIndex]}' is not preferred encoding switch '{switchName}'!");
                return lastArgsIndex;
            }

            Program.ECC.PreferredInputEncoding = switchData;
            return lastArgsIndex;
        }
        int ProcessOutputPathFormatCLArg(string[] args, int startingIndex)
        {
            string switchName = CLARG_OutputPathFormat;

            string switchData;
            int lastArgsIndex = args.GetSwitchData(startingIndex, switchName, CommandLine.CLARG_DataSeparator, out switchData);

            if (lastArgsIndex < startingIndex)
            {
                //Switch is NOT PreferredEncoding switch:
                Trace.TraceWarning($"Argument '{args[startingIndex]}' is not output path format switch '{switchName}'!");
                return lastArgsIndex;
            }

            this.OutputPathFormatter.FormatString = switchData;

            return lastArgsIndex;
        }
        int ProcessCompanionFileSearchPatternCLArg(string[] args, int startingIndex)
        {
            string switchName = CLARG_CompFileSearchPattern;

            string switchData;
            int lastArgsIndex = args.GetSwitchData(startingIndex, switchName, CommandLine.CLARG_DataSeparator, out switchData);

            if (lastArgsIndex < startingIndex)
            {
                //Switch is NOT PreferredEncoding switch:
                Trace.TraceWarning($"Argument '{args[startingIndex]}' is not companion file search pattern switch '{switchName}'!");
                return lastArgsIndex;
            }

            this.OutputPathFormatter.CompanionFileSearchPattern = switchData;

            return lastArgsIndex;
        }
        int ProcessCompanionFileCLArg(string[] args, int startingIndex)
        {
            string switchName = CLARG_CompFile;

            string switchData;
            int lastArgsIndex = args.GetSwitchData(startingIndex, switchName, CommandLine.CLARG_DataSeparator, out switchData);

            if (lastArgsIndex < startingIndex)
            {
                //Switch is NOT PreferredEncoding switch:
                Trace.TraceWarning($"Argument '{args[startingIndex]}' is not companion file switch '{switchName}'!");
                return lastArgsIndex;
            }

            this.OutputPathFormatter.CompanionFile = switchData;

            return lastArgsIndex;
        }

#if DEBUG == true
        public OutputPathFormatter _OPF;
#else
        private OutputPathFormatter _OPF;
#endif
        public OutputPathFormatter OutputPathFormatter
        {
            get
            {
                if (_OPF == null)
                    _OPF = new OutputPathFormatter(Program.ECC);
                return _OPF;
            }
        }


    }
}
