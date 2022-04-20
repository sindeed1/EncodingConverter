using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace EncodingConverter.Commands
{
    class DetectEncodingCommad : ICommandLineCommand
    {
        const string CL_Name = "/detect";
        //const string CLARG_FORM = "form:";
        //const string CLARG_SWITCH = "-";
        Func<string[], int, int>[] _Switches;

        public string Name => CL_Name;

        public string ShortDescription => "Detect the encoding of the provided file.";

        public string LongDescription => "";

        public int Execute(string[] args, int argsStartIndex)
        {
            if (!args.IsSwitch(argsStartIndex, this.Name))
                return argsStartIndex - 1;
            
            Win32Helper.StartConsole();

            InitCommonSwitches();

            argsStartIndex = args.ChainProcessCommandLine(argsStartIndex, _Switches, true, ProcessNoSwitch);

            //var result = Program.ECC.de;
            return argsStartIndex;
        }
        void InitCommonSwitches()
        {
            if (_Switches != null)
                return;

            _Switches = new Func<string[], int, int>[2];
            _Switches[0] = CommandLine.ProcessInputEncodingCLArg;
            _Switches[1] = CommandLine.ProcessOutputEncodingCLArg;
        }

        public static int ProcessNoSwitch(string[] args, int startingArgIndex)
        {
            if (string.IsNullOrWhiteSpace(Program.ECC.InputFilePath))
            {
                Program.ECC.InputFilePath = args[startingArgIndex];
            }
            return startingArgIndex;
        }

    }
}
