using EncodingConverter.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EncodingConverter.Commands
{
    class ShowUICommand : ICommandLineCommand
    {
        const string CL_Name = "showui";
        const string CLARG_FORM = "form:";
        const string CLARG_SWITCH = "-";
        const string CLARG_InputEncoding = "ie:";
        const string CLARG_OutputEncoding = "oe:";

        Func<string, bool>[] _CommonCommandLineSwitches;

        Form _Form;

        public string Name => CL_Name;

        public string ShortDescription => "Shows the user interface.";

        public string LongDescription => "";

        public bool Execute(string[] args, int argsStartIndex)
        {
            //Check command name:
            //string switchName = CL_Name;
            //if (!args[0].IsSwitch(switchName))
            //    return false;

            //Now process the possible switches:
            InitSwitches();

            args.ProcessCommadLineSwitches(argsStartIndex, _CommonCommandLineSwitches, CommandLine.ProcessNoSwitch);

            StartUI();
            return true;
        }
        void StartUI()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (_Form == null)
                _Form = new FormMain2();
            Application.Run(_Form);
        }

        void InitSwitches()
        {
            if (_CommonCommandLineSwitches != null)
                return;

            _CommonCommandLineSwitches = new Func<string, bool>[3];
            int i = 0;
            _CommonCommandLineSwitches[i++] = this.ProcessFormCLArg;
            _CommonCommandLineSwitches[i++] = CommandLine.ProcessInputEncodingCLArg;
            _CommonCommandLineSwitches[i++] = CommandLine.ProcessOutputEncodingCLArg;
        }

        bool ProcessFormCLArg(string arg)
        {
            string switchName = CLARG_FORM;
            if (!arg.IsSwitch(switchName))
                return false;

            string switchData;
            switchData = arg.GetSwitchData(switchName);//
            switchData = switchData.Trim().ToLower();
            
            if (switchData == nameof(Forms.FormMain2).ToLower())
            {
                _Form = new Forms.FormMain2();
            }
            else if (switchData == nameof(AEC.FormMain).ToLower())
            {
                _Form = new AEC.FormMain();
            }
            else
            {
                _Form = null;
            }

            return true;
        }

    }
}
