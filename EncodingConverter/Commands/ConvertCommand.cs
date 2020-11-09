using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter.Commands
{
    /// <summary>
    /// Command line command that converts the encoding of a given file to another encoding.
    /// </summary>
    class ConvertCommand : ICommandLineCommand
    {
        const string CLARG_SWITCH = "-";
        const string CLARG_InputEncoding = "ie:";
        const string CLARG_OutputEncoding = "oe:";
        const string CLARG_Convert = "convert";

        Func<string, bool>[] _CommonCommandLineSwitches;

        public string Name => CLARG_Convert;

        public string ShortDescription => "Converts a text file from a given encoding to another.";

        public string LongDescription => "";

        public bool Execute(string[] args, int argsStartIndex)
        {
            //string switchName = CLARG_Convert;
            //if (!args[0].IsSwitch(switchName))
            //    return false;

            InitCommonSwitches();

            args.ProcessCommadLineSwitches(argsStartIndex, _CommonCommandLineSwitches, CommandLine.ProcessNoSwitch);

            Program.ECC.Convert();
            return true;
        }


        void InitCommonSwitches()
        {
            if (_CommonCommandLineSwitches != null)
                return;

            _CommonCommandLineSwitches = new Func<string, bool>[2];
            _CommonCommandLineSwitches[0] = CommandLine.ProcessInputEncodingCLArg;
            _CommonCommandLineSwitches[1] = CommandLine.ProcessOutputEncodingCLArg;
        }

        bool ProcessInputEncodingCLArg(string arg)
        {
            string switchName = CLARG_InputEncoding;
            if (!arg.IsSwitch(switchName))
                return false;

            string switchData;
            switchData = arg.GetSwitchData(switchName);//

            EncodingInfo encodingInfo;
            encodingInfo = GetEncodingInfoFromSwitchData(switchData);
            if (encodingInfo == null)
            {
                Console.WriteLine("Switch InputEncoding '" + switchName + "' does not provide a recognizable code page '" + switchData + "'.");
            }
            else
            {
                Console.WriteLine("Input encoding '" + encodingInfo.DisplayName + "'.");
                Program.ECC.InputEncoding = encodingInfo.GetEncoding();
            }

            return true;
        }
        bool ProcessOutputEncodingCLArg(string arg)
        {
            string switchName = CLARG_OutputEncoding;
            if (!arg.IsSwitch(switchName))
                return false;

            string switchData;
            switchData = arg.GetSwitchData(switchName);//

            EncodingInfo encodingInfo;
            encodingInfo = GetEncodingInfoFromSwitchData(switchData);
            if (encodingInfo == null)
            {
                Console.WriteLine("Switch OutputEncoding '" + switchName + "' does not provide a recognizable code page '" + switchData + "'.");
            }
            else
            {
                Console.WriteLine("Output encoding '" + encodingInfo.DisplayName + "'.");
                Program.ECC.OutputEncoding = encodingInfo.GetEncoding();
            }

            return true;
        }
        bool ProcessNoSwitch(string arg)
        {
            if (string.IsNullOrWhiteSpace(Program.ECC.InputFilePath))
            {
                Program.ECC.InputFilePath = arg;
            }
            else
            {
                Program.ECC.OutputFilePath = arg;
            }
            return true;
        }

        EncodingInfo GetEncodingInfoFromSwitchData(string switchData)
        {
            EncodingInfo encodingInfo;
            int codePage;
            if (Int32.TryParse(switchData, out codePage))
            {
                encodingInfo = Program.ECC.Encodings.FirstOrDefault(x => x.CodePage == codePage);
            }
            else
            {
                encodingInfo = Program.ECC.Encodings.FirstOrDefault(x => x.Name == switchData);
            }

            return encodingInfo;
        }


    }
}
