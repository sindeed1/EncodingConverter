﻿using System;
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
        object[] _OutputPathFormattingParameters;


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
            _OutputPathFormattingParameters = new object[23];
        }
        #endregion


        public string InputText
        {
            get
            {
                if (_InputText != null)
                    return _InputText;

                if (_InputEncoding == null || string.IsNullOrWhiteSpace(_InputFilePath))
                {
                    return null;
                }

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
                OnPreferredInputEncodingChanged();
            }
        }

        public string InputFilePath
        {
            get { return _InputFilePath; }
            set
            {
                if (_InputFilePath == value)
                    return;

                _InputFilePath = value;
                _InputText = null;

                OnInputFilePathChanged();

                //Encoding encoding;
                if (_AutoCheckInputEncoding)
                {
                    this.InputEncoding = DetectInputEncdoing(_InputFilePath, _PreferredInputEncoding);
                }

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

                OnAutoDetectInputEncodingChanged();
            }
        }

        public Encoding InputEncoding
        {
            get { return _InputEncoding; }
            set
            {
                if (_InputEncoding == value
                    || (_InputEncoding != null && value != null && _InputEncoding.CodePage == value.CodePage))
                    return;

                _InputEncoding = value;

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
                if (_OutputEncoding == value
                    || (_OutputEncoding != null && value != null && _OutputEncoding.CodePage == value.CodePage))
                    return;
                
                _OutputEncoding = value;

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
                throw new FileNotFoundException(Properties.Resources.Message_InputFileDoesNotExsist, inputPath);
            }

            // do auto-detect
            //isDetectingInputEncoding = true;

            byte[] buf;
            FileStream stream = null;
            try
            {
                stream = new FileStream(inputPath, FileMode.Open, FileAccess.Read);
                buf = new byte[stream.Length];
                stream.Read(buf, 0, (int)stream.Length);
            }
            finally
            {
                stream?.Close();
            }

            preferredString = preferredString.Trim();
            Encoding encoding;
            if (preferredString == null || preferredString.Length <= 0)
            {
                encoding = EncodingTools.DetectInputCodepage(buf);
            }
            else
            {
                var encodings = EncodingTools.DetectInputCodepages(buf, 10);
                var searchStrings = preferredString.ToLower().Split(' ');
                var prefferedEncodings = encodings.Where(x => x.EncodingName.ToLower().Contains(searchStrings)).ToArray();
                if (prefferedEncodings == null || prefferedEncodings.Length <= 0)
                {
                    encoding = encodings[0];
                }
                else
                {
                    Trace.TraceInformation(string.Format("Found '{0}' encodings with the preferred encoding text '{1}'", prefferedEncodings.Length, preferredString));
                    encoding = prefferedEncodings[0];
                }
            }

            return encoding;
            //encodingsTool_input.SelectedEncoding = encoding;
            //richTextBox_in.Lines = File.ReadAllLines(txtInputPath.Text, encoding);
            //isDetectingInputEncoding = false;
        }


        string FormatOutputpath(string inputPath, Encoding inputEncoding, Encoding outputEncoding)
        {
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
            _OutputPathFormattingParameters[10] = outputEncoding.EncodingName;   //{10} Output encoding name
            _OutputPathFormattingParameters[11] = outputEncoding.BodyName;       //{11} Output encoding Body name
            _OutputPathFormattingParameters[12] = outputEncoding.CodePage;       //{12} Output encoding Code page
                                                                                 //{13-19} reserved and empty
            _OutputPathFormattingParameters[20] = inputEncoding.EncodingName;    //{20} Input encoding name
            _OutputPathFormattingParameters[21] = inputEncoding.BodyName;        //{21} Input encoding Body name
            _OutputPathFormattingParameters[22] = inputEncoding.CodePage;        //{22} Input encoding Code page

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
    }

    #endregion//

    //class
}//namespace