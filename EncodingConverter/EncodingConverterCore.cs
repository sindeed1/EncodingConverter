using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using href.Utils;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Runtime.CompilerServices;

namespace EncodingConverter
{
    class EncodingConverterCore
    {
        #region ...Events...
        public event EventHandler InputTextChanged;
        public event EventHandler PreferredInputEncodingChanged;
        public event EventHandler AutoDetectInputEncodingChanged;
        public event EventHandler InputEncodingChanged;
        public event EventHandler OutputEncodingChanged;
        public event EventHandler InputFilePathChanged;
        public event EventHandler OutputFilePathChanged;

        #endregion
        //#region ...Error codes...
        //const int ERR_CONVERT_InputEncodingNull = 1;
        //const int ERR_CONVERT_OutputEncodingNull = 2;
        //const int ERR_CONVERT_InputFileNotExist = 3;

        //#endregion
        EncodingInfo[] _Encodings;

        bool isDetectingInputEncoding;


        string _InputFilePath;
        string _OutputFilePath;

        Encoding _InputEncoding;
        Encoding _OutputEncoding;


        private bool _AutoCheckInputEncoding;
        private string _PreferredInputEncoding;
        private string _InputText;

        #region ...ctor...

        public EncodingConverterCore()
        {
            _Encodings = Encoding.GetEncodings();
        }
        #endregion


        /// <summary>
        /// Returns the input file as a text encoded using <see cref="InputEncoding"/>.
        /// </summary>
        public string InputText
        {
            get
            {
                //To save processing time, InputText will be processed one time and stored
                //in the _InputText. If it was null then we should read the file; Otherwise
                //we can just return the text:
                if (_InputText != null)
                    return _InputText;

                //There is no cashed _InputText, can we get the text:
                if (_InputEncoding == null || string.IsNullOrWhiteSpace(_InputFilePath))
                {
                    //There is no file to read, OR there is no encoding specified, return.
                    return null;
                }

                //Read the file:
                _InputText = File.ReadAllText(_InputFilePath, _InputEncoding);
                return _InputText;
            }
        }

        public string PreferredInputEncoding
        {
            get { return _PreferredInputEncoding; }
            set
            {
                if (_PreferredInputEncoding == value)
                    return;
                _PreferredInputEncoding = value;
                Trace.TraceInformation(nameof(EncodingConverterCore) + "." + nameof(this.PreferredInputEncoding) + ".set:'" + value + "'");
                OnPreferredInputEncodingChanged();
            }
        }

        public string InputFilePath
        {
            get { return _InputFilePath; }
            set
            {
                //ToDo: Change to let the OS/Framework to decide if the new value and _InputFile are the same.
                //The core should not be sensitive to changes in the path if it returns the same file.
                //A reasonable way to do that is by using FileInfo instead of FilePath as a variable.
                if (_InputFilePath == value)
                    return;

                _InputFilePath = value;

                RefreshInputFielPath();

                OnInputTextChanged();

            }
        }
        public string OutputFilePath
        {
            get { return _OutputFilePath; }
            set
            {
                if (_OutputFilePath == value)
                    return;

                _OutputFilePath = value;
                Trace.TraceInformation(nameof(EncodingConverterCore) + "." + nameof(this.OutputFilePath) + ".set:'" + value + "'");

                OnOutputFilePathChanged();
            }
        }


        public bool AutoDetectInputEncoding
        {
            get { return _AutoCheckInputEncoding; }
            set
            {
                if (_AutoCheckInputEncoding == value)
                    return;
                _AutoCheckInputEncoding = value;
                Trace.TraceInformation(nameof(EncodingConverterCore) + "." + nameof(this.AutoDetectInputEncoding) + ".set:'" + value + "'");

                OnAutoDetectInputEncodingChanged();
            }
        }

        public Encoding InputEncoding
        {
            get { return _InputEncoding; }
            set
            {
                if (Equate(_InputEncoding, value))
                    return;

                _InputEncoding = value;
                Trace.TraceInformation(nameof(EncodingConverterCore) + "." + nameof(this.InputEncoding) + ".set: '"
                    + (value == null ? "NULL" : value.EncodingName + ", CodePage='" + value.CodePage.ToString() + "'")
                    + "'");

                _InputText = null;
                OnInputEncodingChanged();
                OnInputTextChanged();
            }
        }

        public Encoding OutputEncoding
        {
            get { return _OutputEncoding; }
            set
            {
                if (Equate(_OutputEncoding, value))
                    return;

                _OutputEncoding = value;
                Trace.TraceInformation(nameof(EncodingConverterCore) + "." + nameof(this.OutputEncoding) + ".set: '"
                    + (value == null ? "NULL" : value.EncodingName + ", CodePage='" + value.CodePage.ToString() + "'")
                    + "'");

                OnOutputEncodingChanged();
            }
        }


        public EncodingInfo[] Encodings { get { return _Encodings; } }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="PathTooLongException"></exception>
        /// <exception cref="DirectoryNotFoundException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="UnauthorizedAccessException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="System.Security.SecurityException"></exception>
        public void Convert()
        {
            if (_InputText == null)
            {
                Convert(_InputFilePath, _InputEncoding, _OutputFilePath, _OutputEncoding);
            }
            else
            {
                File.WriteAllText(_OutputFilePath, _InputText, _OutputEncoding);
            }
            return;
            if (_InputEncoding == null)
            {
                Console.WriteLine("Can not convert. Input encoding is not specified.");
                throw new ArgumentNullException(nameof(InputEncoding));
                return;
            }
            if (_OutputEncoding == null)
            {
                Console.WriteLine("Can not convert. Output encoding is not specified.");
                throw new ArgumentNullException(nameof(OutputEncoding));
                return;
            }

            if (!File.Exists(_InputFilePath))
            {
                Console.WriteLine("Can not convert. Input file '{0}' does not exist!", _InputFilePath);
                return;
            }
            if (_OutputFilePath == null || _OutputFilePath.Length == 0)
            {
                Console.WriteLine("Output file is not specified. Please enter a valid output file path.");
                return;
            }

            string[] lines;

            Console.WriteLine("Reading text from input file '{0}'", _InputFilePath);
            Console.WriteLine("Input encoding '{0}'", _InputEncoding);
            try
            {
                // Read all lines from the file
                lines = File.ReadAllLines(_InputFilePath, _InputEncoding);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while reading input file:");
                Console.WriteLine("Input file path: '{0}'", _InputFilePath);
                Console.WriteLine("Input encoding: '{0}'", _InputEncoding);
                Console.WriteLine("Raised Exception:");
                Console.WriteLine(ex);
                return;
                throw;
            }

            Console.WriteLine("Writing text to output file '{0}'", _OutputFilePath);
            Console.WriteLine("Output encoding '{0}'", _OutputEncoding);
            try
            {
                // Save it !
                File.WriteAllLines(_OutputFilePath, lines, _OutputEncoding);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while writing the encoded text to the output file:");
                Console.WriteLine("Output file path: '{0}'", _OutputFilePath);
                Console.WriteLine("Output encoding: '{0}'", _OutputEncoding);
                Console.WriteLine("Raised Exception:");
                Console.WriteLine(ex);
                return;
                throw;
            }
            // Done !!
            Console.WriteLine("Conversion finished successfully.");
        }
        public void RefreshInputFielPath()
        {
            //The InputFilePath has changed. The _InputText is no more valid:
            _InputText = null;

            Trace.TraceInformation(nameof(EncodingConverterCore) + "." + nameof(this.InputFilePath) + ".set:'" + _InputFilePath + "'");
            OnInputFilePathChanged();

            //Encoding encoding;
            if (_AutoCheckInputEncoding)
            {
                this.InputEncoding = DetectInputEncdoing(_InputFilePath, _PreferredInputEncoding);
            }
        }
        public static void Convert(string inputFile, Encoding inputEncoding, string outputFile, Encoding outputEncoding)
        {
            string text;
            text = File.ReadAllText(inputFile, inputEncoding);
            File.WriteAllText(outputFile, text, outputEncoding);
        }
        private static Encoding DetectInputEncdoing(string inputPath, string preferredString)
        {
            if (!File.Exists(inputPath))
            {
                TraceError(nameof(DetectInputEncdoing), nameof(inputPath) + " does not exist!");
                throw new FileNotFoundException(Properties.Resources.Message_InputFileDoesNotExsist, inputPath);
            }

            // do auto-detect
            //isDetectingInputEncoding = true;

            byte[] buf = null;
            FileStream stream = null;
            try
            {
                stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
                buf = new byte[stream.Length];
                stream.Read(buf, 0, (int)stream.Length);
            }
            catch (Exception ex)
            {
                TraceWarning(nameof(DetectInputEncdoing), "Exception while reading input file '" +
                    inputPath + "': \n"
                    );
                ex.WriteToTrace();
            }
            finally
            {
                stream?.Close();
            }

            preferredString = preferredString?.Trim();
            Encoding encoding;
            if (preferredString == null || preferredString.Length <= 0)
            {
                try
                {
                    encoding = EncodingTools.DetectInputCodepage(buf);
                }
                catch (Exception ex)
                {
                    TraceWarning("Error by detecting the encoding of the file '" + inputPath + "'.");
                    ex.WriteToTrace();
                    encoding = null;
                }
            }
            else
            {
                Encoding[] encodings;
                encodings = EncodingTools.DetectInputCodepages(buf, 10);
                var searchStrings = preferredString.ToLower().Split(' ');
                var prefferedEncodings = encodings.Where(x => x.EncodingName.ToLower().Contains(searchStrings)).ToArray();
                if (prefferedEncodings == null || prefferedEncodings.Length <= 0)
                {
                    encoding = encodings[0];
                }
                else
                {
                    TraceInformation(string.Format("Found '{0}' encodings with the preferred encoding text '{1}'", prefferedEncodings.Length, preferredString));
                    encoding = prefferedEncodings[0];
                }
            }

            return encoding;
            //encodingsTool_input.SelectedEncoding = encoding;
            //richTextBox_in.Lines = File.ReadAllLines(txtInputPath.Text, encoding);
            //isDetectingInputEncoding = false;
        }

        bool Equate(Encoding enc1, Encoding enc2)
        {
            return (enc1 == enc2)
                    || (enc1 != null && enc2 != null && enc1.CodePage == enc2.CodePage);
        }
        #region ...Event invokers...
        protected void OnInputFilePathChanged() { InputFilePathChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnOutputFilePathChanged() { OutputFilePathChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnInputTextChanged() { InputTextChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnPreferredInputEncodingChanged() { PreferredInputEncodingChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnAutoDetectInputEncodingChanged() { AutoDetectInputEncodingChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnInputEncodingChanged() { InputEncodingChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnOutputEncodingChanged() { OutputEncodingChanged?.Invoke(this, EventArgs.Empty); }

        #endregion


        #region ...Command line...


        private Func<bool> _CommandLineCommand;

        public Func<bool> CommandLineCommand { get { return _CommandLineCommand; } }

        Func<string, bool>[] _CommonCommandLineSwitches;
        Func<string[], bool>[] _CommandLineCommands;

        const string CLARG_SWITCH = "-";
        const string CLARG_InputEncoding = CLARG_SWITCH + "ie:";
        const string CLARG_OutputEncoding = CLARG_SWITCH + "oe:";
        const string CLARG_OutputFile = CLARG_SWITCH + "of:";
        const string CLARG_InputFile = CLARG_SWITCH + "if:";

        const string CLARG_Convert = "convert";
        const string CLARG_Help = "help";


        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <remarks>Command line is something like this:
        /// [command] [filepath] </remarks>
        public void ProcessCommandLine(string[] args)
        {
            Console.WriteLine("Processing command line...");
            if (args == null || args.Length <= 0)
            {
                Console.WriteLine("No command line arguments.");
                return;
            }

            //string inputFile = args[0];// [0]: input file path.
            //Console.WriteLine("Input file:'" + inputFile + "'");

            if (_CommandLineCommands == null)
            {
                int ci = 0;// command counter
                _CommandLineCommands = new Func<string[], bool>[2];
                _CommandLineCommands[ci++] = this.ProcessConvertCLCommand;
                _CommandLineCommands[ci++] = this.ProcessHelpCLCommand;
                //_CommandLineCommands[1] = this.ProcessOutputEncodingCLArg;
            }

            for (int i = 0; i < _CommandLineCommands.Length; i++)
            {
                if (_CommandLineCommands[i](args))
                {
                    break;
                }
            }//for i

            if (_CommandLineCommand != null)
                return;

            //No Command was found. Load the switches:
            InitCommonSwitches();

            args.ProcessCommadLineSwitches(0, _CommonCommandLineSwitches, this.ProcessNoSwitch);
        }

        bool ProcessNoSwitch(string arg)
        {
            if (string.IsNullOrWhiteSpace(_InputFilePath))
            {
                _InputFilePath = arg;
            }
            else
            {
                _OutputFilePath = arg;
            }
            return true;
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
                _InputEncoding = encodingInfo.GetEncoding();
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
                _OutputEncoding = encodingInfo.GetEncoding();
            }

            return true;
        }

        EncodingInfo GetEncodingInfoFromSwitchData(string switchData)
        {
            EncodingInfo encodingInfo;
            int codePage;
            if (Int32.TryParse(switchData, out codePage))
            {
                encodingInfo = this.Encodings.FirstOrDefault(x => x.CodePage == codePage);
            }
            else
            {
                encodingInfo = this.Encodings.FirstOrDefault(x => x.Name == switchData);
            }

            return encodingInfo;
        }

        void InitCommonSwitches()
        {
            if (_CommonCommandLineSwitches != null)
                return;

            _CommonCommandLineSwitches = new Func<string, bool>[2];
            _CommonCommandLineSwitches[0] = this.ProcessInputEncodingCLArg;
            _CommonCommandLineSwitches[1] = this.ProcessOutputEncodingCLArg;
        }
        bool ProcessConvertCLCommand(string[] args)
        {
            string switchName = CLARG_Convert;
            if (!args[0].IsSwitch(switchName))
                return false;

            InitCommonSwitches();

            args.ProcessCommadLineSwitches(1, _CommonCommandLineSwitches, this.ProcessNoSwitch);

            _CommandLineCommand = this.CLConvert;
            return true;
        }

        bool ProcessHelpCLCommand(string[] args)
        {
            string switchName = CLARG_Help;
            if (!args[0].IsSwitch(switchName))
                return false;

            _CommandLineCommand = this.Help;
            return true;
        }

        bool CLConvert()
        {
            this.Convert();
            return true;
        }
        bool Help()
        {
            Console.WriteLine("Available commands:");
            Console.WriteLine(CLARG_Convert);
            Console.WriteLine(CLARG_Help);

            return true;
        }
        #endregion//

        #region Trace helpers
        static void TraceInfo(string methodName, string msg) { Trace.TraceInformation(FormatTraceMessage(methodName, msg)); }
        static void TraceInformation( string msg) { Trace.TraceInformation(FormatTraceMessage(msg)); }
        static void TraceError(string msg) { Trace.TraceError(FormatTraceMessage(msg)); }
        static void TraceError(string methodName, string msg) { Trace.TraceError(FormatTraceMessage(methodName, msg)); }
        static void TraceWarning(string msg) { Trace.TraceWarning(FormatTraceMessage(msg)); }
        static void TraceWarning(string methodName, string msg) { Trace.TraceWarning(FormatTraceMessage(methodName, msg)); }
        static string FormatTraceMessage(string methodName, string msg)
        {
            return nameof(EncodingConverterCore) + "." + methodName + ":" + msg;
        }
        static string FormatTraceMessage(string msg) { return nameof(EncodingConverterCore) + ":" + msg; }

        #endregion
    }//class
}//namespace
