using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter.Commands
{
    class DetectEncodingCommad : ICommandLineCommand
    {
        const string CL_Name = "detect";
        //const string CLARG_FORM = "form:";
        //const string CLARG_SWITCH = "-";
        Func<string, bool>[] _Switches;

        public string Name => CL_Name;

        public string ShortDescription => "Detect the encoding of the provided file.";

        public string LongDescription => "";

        public bool Execute(string[] args, int argsStartIndex)
        {
            InitCommonSwitches();

            args.ProcessCommadLineSwitches(argsStartIndex, _Switches, ProcessNoSwitch);

            //var result = Program.ECC.de;
            return true;
        }
        void InitCommonSwitches()
        {
            if (_Switches != null)
                return;

            _Switches = new Func<string, bool>[2];
            _Switches[0] = CommandLine.ProcessInputEncodingCLArg;
            _Switches[1] = CommandLine.ProcessOutputEncodingCLArg;
        }

        public static bool ProcessNoSwitch(string arg)
        {
            if (string.IsNullOrWhiteSpace(Program.ECC.InputFilePath))
            {
                Program.ECC.InputFilePath = arg;
            }
            return true;
        }

    }
}
