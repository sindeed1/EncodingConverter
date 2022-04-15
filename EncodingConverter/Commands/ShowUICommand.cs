using EncodingConverter.Forms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandLine;

namespace EncodingConverter.Commands
{
    class ShowUICommand : ICommandLineCommand
    {
        public const string CL_Name = "showui";
        public const string CLARG_FORM = "-form:";
        const string CLARG_SWITCH = "-";
        public const string CLARG_InputEncoding = CommandLine.CLARG_InputEncoding;// "-ie";
        public const string CLARG_OutputEncoding = CommandLine.CLARG_OutputEncoding;// "-oe";

        Func<string[], int, int>[] _CommonCommandLineSwitches;

        Form _Form;
        Type _FormType;

        string _InputFilePathArg, _OutputFilePathArg;
        bool _InputEncodingAvailable, _OutputEncodingAvailable;
        EncodingInfo _InputEncoding, _OutputEncoding;

        public string Name => CL_Name;

        public string ShortDescription => "Displays the user interface.";

        public string LongDescription => $"<{CL_Name}>" +
            $"[ {CLARG_FORM}<{CommandLine.CLARG_DataSeparator}<formName>| formName>>]" +
            $"[ {CLARG_InputEncoding}<{CommandLine.CLARG_DataSeparator}<encoding.CodePage|.Name>>| <encoding.CodePage|.Name>>]" +
            $"[ {CLARG_OutputEncoding}<{CommandLine.CLARG_DataSeparator}<encoding.CodePage|.Name>>| <encoding.CodePage|.Name>>]" +
            $"[ inputFileName]" +
            $"[ outputFileName]" +
            $"{Environment.NewLine}    '{CLARG_FORM}' Optional, Name of the form to be displayed." +
            $"{Environment.NewLine}      If this argument is not provided then it is interpreted as '{CLARG_FORM} mainform4'" +
            $"{Environment.NewLine}    '{CLARG_InputEncoding}' is the encoding of the <inputFileName>. Optional and must be followed by either code page of the encoding or name of the encoding." +
            $"{Environment.NewLine}    '{CLARG_OutputEncoding}' is the encoding of the <outputFileName>. Optional and must be followed by either code page of the encoding or name of the encoding." +
            $"{Environment.NewLine}    [inputFileName]  Optional, file name of the file that needs to be converted. The first argument that is not a switch (i.e. not input or output switch) is interpreted as the input file." +
            $"{Environment.NewLine}    [outputFileName] Optional, file name of the output file that will contain the converted encoding. The last argument that is not a switch (i.e. not input or output switch) is interpreted as the output file." +
            $"{Environment.NewLine}" +
            $"You can use this command to also load the UI with preferred starting value like input and output encodings or files. The loading of the values" +
            $" happens after loading of settings in the UI, witch means that this command will override the values saved in the settings."
            ;

        public int Execute(string[] args, int argsStartIndex)
        {
            //Check command name:
            //string switchName = CL_Name;
            if (!args.IsSwitch(argsStartIndex, this.Name))
                //if (!args[0].IsSwitch(this.Name))
                return argsStartIndex - 1;

            //Now process the possible switches:
            InitSwitches();

            var lastArgIndex = args.ChainProcessCommandLine(argsStartIndex + 1, _CommonCommandLineSwitches, true, this.ProcessNoSwitch);

            StartUI();
            return lastArgIndex;
        }
        void StartUI()
        {
            Application.EnableVisualStyles();
            if (_Form == null)
            {
                Application.SetCompatibleTextRenderingDefault(false);
            }

            if (_FormType == null)
            {
                InitForm(typeof(MainForm4));
            }
            else
            {
                InitForm(_FormType);
            }

            Trace.TraceInformation($"{nameof(ShowUICommand)}: Run Application using form: " + _Form.GetType().FullName);
            Application.Run(_Form);
        }

        void InitSwitches()
        {
            if (_CommonCommandLineSwitches != null)
                return;

            _CommonCommandLineSwitches = new Func<string[], int, int>[3];
            int i = 0;
            _CommonCommandLineSwitches[i++] = this.ProcessFormCLArg;
            _CommonCommandLineSwitches[i++] = this.ProcessInputEncodingCLArg;
            _CommonCommandLineSwitches[i++] = this.ProcessOutputEncodingCLArg;
        }

        void InitForm(Type formType)
        {
            var ini = formType.GetConstructor(Type.EmptyTypes);
            _Form = (Form)ini.Invoke(null);
            _Form.Load += _Form_Load;
        }

        private void _Form_Load(object sender, EventArgs e)
        {
            //First, remove the method from the event:
            _Form.Load -= _Form_Load;

            //Program.ECC.AutoDetectInputEncoding = true;

            //After constructing the form and before loading it we will load the init values
            //that are passed in the arguments. This will rewrite the values to the form after
            //loading from the settings:
            if (_InputEncodingAvailable)
            {
                //Program.ECC.AutoDetectInputEncoding = false;
                Program.ECC.InputEncoding = _InputEncoding?.GetEncoding();
            }

            if (_InputFilePathArg != null)
            {
                //Program.ECC.AutoDetectInputEncoding = false;
                Program.ECC.InputFilePath = _InputFilePathArg;
                //try
                //{
                //    Program.ECC.InputFilePath = _InputFilePathArg;
                //}
                //catch (Exception ex)
                //{
                //    Trace.TraceWarning($"An exception was encountered while setting a new value " +
                //        $"{nameof(EncodingConverterCore)}.{nameof(EncodingConverterCore.InputFilePath)}='{_InputFilePathArg}'.");
                //    Trace.WriteLine($"Exception: {ex}");
                //}
            }
            if (_OutputEncodingAvailable)
            {
                //Program.ECC.AutoDetectInputEncoding = false;
                Program.ECC.OutputEncoding = _OutputEncoding?.GetEncoding();
            }
            if (_OutputFilePathArg != null)
            {
                //Properties.Settings.Default.AutoDetectInputEncoding = false;
                Program.ECC.OutputFilePath = _OutputFilePathArg;
            }
        }

        int ProcessFormCLArg(string[] args, int argStartingIndex)
        {
            string switchName = CLARG_FORM;
            string arg = args[argStartingIndex];


            string switchData;
            int lastArgsIndex = args.GetSwitchData(argStartingIndex, switchName, CommandLine.CLARG_DataSeparator, out switchData);
            if (lastArgsIndex < argStartingIndex)
                //if (!arg.IsSwitch(switchName))
                return argStartingIndex - 1;

            //switchData = arg.GetSwitchData(switchName);//
            switchData = switchData.Trim().ToLower();

            if (switchData == nameof(MainForm3).ToLower())
            {
                _FormType = typeof(MainForm3);
            }
            else if (switchData == nameof(MainForm4).ToLower())
            {
                _FormType = typeof(MainForm4);
            }
            else if (switchData == nameof(MainForm5).ToLower())
            {
                _FormType = typeof(MainForm5);
            }
            else
            {
                _Form = null;
            }

            return argStartingIndex;
        }


        public int ProcessNoSwitch(string[] args, int startinIndex)
        {

            if (string.IsNullOrWhiteSpace(_InputFilePathArg))
            {
                _InputFilePathArg = args[startinIndex];
            }
            else
            {
                _OutputFilePathArg = args[startinIndex];
            }
            return startinIndex;
        }
        public int ProcessInputEncodingCLArg(string[] args, int startingIndex)
        {
            //string arg = args[startingIndex];

            string switchName = CLARG_InputEncoding;

            string switchData;
            int lastArgsIndex = CommandLine.GetSwitchData(args, startingIndex, switchName, CommandLine.CLARG_DataSeparator, out switchData);
            if (lastArgsIndex < startingIndex || string.IsNullOrEmpty(switchData))
            {
                Trace.TraceWarning($"Switch '{args[startingIndex]}' is not input encoding switch '{switchName}'!");
                return lastArgsIndex;
            }

            EncodingInfo encodingInfo;
            encodingInfo = CommandLine.GetEncodingInfoFromSwitchData(switchData);
            if (encodingInfo == null)
            {
                Console.WriteLine($"Switch InputEncoding '{switchName}' does not provide a recognizable code page '{switchData}'.");
                _InputEncodingAvailable = false;
                _InputEncoding = null;
            }
            else
            {
                Console.WriteLine($"Input encoding '{encodingInfo.DisplayName}'.");

                _InputEncodingAvailable = true;
                _InputEncoding = encodingInfo;
            }

            return lastArgsIndex;
        }

        public int ProcessOutputEncodingCLArg(string[] args, int startingIndex)
        {
            //string arg = args[startingIndex];

            string switchName = CLARG_OutputEncoding;

            string switchData;
            int lastArgsIndex = CommandLine.GetSwitchData(args, startingIndex, switchName, CommandLine.CLARG_DataSeparator, out switchData);
            if (lastArgsIndex < startingIndex || string.IsNullOrEmpty(switchData))
            {
                Trace.TraceWarning($"Switch '{args[startingIndex]}' is not output encoding switch '{switchName}'!");
                return lastArgsIndex;
            }

            EncodingInfo encodingInfo;
            encodingInfo = CommandLine.GetEncodingInfoFromSwitchData(switchData);
            if (encodingInfo == null)
            {
                Console.WriteLine($"Switch OutputEncoding '{switchName}' does not provide a recognizable code page '{switchData}'.");
                _OutputEncodingAvailable = false;
                _OutputEncoding = null;
            }
            else
            {
                Console.WriteLine($"Output encoding '{encodingInfo.DisplayName}'.");

                _OutputEncodingAvailable = true;
                _OutputEncoding = encodingInfo;
            }

            return lastArgsIndex;
        }


    }
}
