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
#if DEBUG == true
    public
#endif
    class EncodingConverterCore
    {
        const int con_Default_MaxDetectedInputEncodings = 10;
        #region ...Events...
        public event EventHandler InputTextChanged;
        public event EventHandler PreferredInputEncodingChanged;
        public event EventHandler AutoDetectInputEncodingChanged;
        public event EventHandler InputEncodingChanged;
        public event EventHandler OutputEncodingChanged;
        public event EventHandler InputFilePathChanged;
        public event EventHandler OutputFilePathChanged;

        public event EventHandler DetectedEncodingsChanged;
        #endregion
        //#region ...Error codes...
        //const int ERR_CONVERT_InputEncodingNull = 1;
        //const int ERR_CONVERT_OutputEncodingNull = 2;
        //const int ERR_CONVERT_InputFileNotExist = 3;

        //#endregion
        EncodingInfo[] _Encodings;

        int _MaxDetectedInputEncodings;


        string _InputFilePath;
        string _OutputFilePath;

        Encoding _InputEncoding;
        Encoding _OutputEncoding;


        private bool _AutoDetectInputEncoding;
        private string _PreferredInputEncoding;
        private string _InputText;

        #region ...ctor...

        public EncodingConverterCore()
        {
            _Encodings = Encoding.GetEncodings();
            _MaxDetectedInputEncodings = con_Default_MaxDetectedInputEncodings;
        }
        #endregion


        /// <summary>
        /// Returns the input file as a text encoded using <see cref="InputEncoding"/>.
        /// </summary>
        /// <remarks>In the current implementation, the <see cref="InputText"/> is implemented as lazy load, witch
        /// means that the text will actually be loaded upon request and NOT directly after changing <see cref="InputFilePath"/>
        /// or <see cref="InputEncoding"/>.
        /// <para>This method does NOT check for the existence of <see cref="InputFilePath"/>. If it does not exist,
        /// an exception will be thrown.</para>
        /// <para>If <see cref="InputFilePath"/> or <see cref="InputEncoding"/> are null, empty, or white space, returns null.</para></remarks>
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

        /// <summary>
        /// Gets or sets the path of the input file that will be converted.
        /// </summary>
        /// <remarks>In the current implementation the logic does not check the file directly. Some methods
        /// like <see cref="Convert"/> and <see cref="DetectInputEncoding"/> will, however, read the file and throw
        /// exceptions if it does not work.
        /// <para>Also, if <see cref="AutoDetectInputEncoding"/> is true, then <see cref="DetectInputEncoding"/> will be automatically
        /// called, thus triggering an exception if the file does not exist or has access problem.</para></remarks>
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

                RefreshInputFilePath();

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
            get { return _AutoDetectInputEncoding; }
            set
            {
                if (_AutoDetectInputEncoding == value)
                    return;
                _AutoDetectInputEncoding = value;
                Trace.TraceInformation(nameof(EncodingConverterCore) + "." + nameof(this.AutoDetectInputEncoding) + ".set:'" + value + "'");

                OnAutoDetectInputEncodingChanged();
            }
        }

        public Encoding InputEncoding
        {
            get { return _InputEncoding; }
            set
            {
                if (_InputEncoding.EqualsEncoding(value))
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
                if (_OutputEncoding.EqualsEncoding(value))
                    return;

                _OutputEncoding = value;
                Trace.TraceInformation(nameof(EncodingConverterCore) + "." + nameof(this.OutputEncoding) + ".set: '"
                    + (value == null ? "NULL" : value.EncodingName + ", CodePage='" + value.CodePage.ToString() + "'")
                    + "'");

                OnOutputEncodingChanged();
            }
        }

        private Encoding[] _DetectedEncodings;

        /// <summary>
        /// Returns an array of encodings available to the current <see cref="InputFilePath"/>.
        /// The list will be generated only after the user calls <see cref="DetectInputEncoding"/>
        /// </summary>
        public Encoding[] DetectedEncodings
        {
            get { return _DetectedEncodings; }
            private set
            {
                if (value == _DetectedEncodings)
                    return;

                _DetectedEncodings = value;

                OnDetectedEncodingsChanged();
            }
        }

        /// <summary>
        /// Returns an array of all encodings in the system.
        /// </summary>
        public EncodingInfo[] Encodings { get { return _Encodings; } }


        /// <summary>
        /// Gets or sets the max count of detected encodings for <see cref="InputFilePath"/>. Must be >= 1.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">When value is less that one.</exception>
        public int MaxDetectedInputEncodings
        {
            get { return _MaxDetectedInputEncodings; }
            set 
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value), "value must be equal or larger to one.");
                }
                _MaxDetectedInputEncodings = value; 
            }
        }

        /// <summary>
        /// Detects the input encoding based on <see cref="InputFilePath"/> and <see cref="PreferredInputEncoding"/>.
        /// The detected encoding will be then automatically set to <see cref="InputEncoding"/>.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="FileNotFoundException"></exception>
        public void DetectInputEncoding()
        {
            Encoding[] detectedEncodings = null;

            var encoding = DetectInputEncoding(_InputFilePath, _PreferredInputEncoding, _MaxDetectedInputEncodings, out detectedEncodings);

            this.DetectedEncodings = detectedEncodings;

            this.InputEncoding = encoding;
        }
        /// <summary>
        /// Reads the <see cref="InputFilePath"/> file using the <see cref="InputEncoding"/>
        /// and writes it to <see cref="OutputFilePath"/> using the <see cref="OutputEncoding"/>.
        /// </summary>
        /// <remarks>This method will overwrite the <see cref="OutputFilePath"/> if it already exists.</remarks>
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
            //if (_InputEncoding == null)
            //{
            //    Console.WriteLine("Can not convert. Input encoding is not specified.");
            //    throw new ArgumentNullException(nameof(InputEncoding));
            //    return;
            //}
            //if (_OutputEncoding == null)
            //{
            //    Console.WriteLine("Can not convert. Output encoding is not specified.");
            //    throw new ArgumentNullException(nameof(OutputEncoding));
            //    return;
            //}

            //if (!File.Exists(_InputFilePath))
            //{
            //    Console.WriteLine("Can not convert. Input file '{0}' does not exist!", _InputFilePath);
            //    return;
            //}
            //if (_OutputFilePath == null || _OutputFilePath.Length == 0)
            //{
            //    Console.WriteLine("Output file is not specified. Please enter a valid output file path.");
            //    return;
            //}

            //string[] lines;

            //Console.WriteLine("Reading text from input file '{0}'", _InputFilePath);
            //Console.WriteLine("Input encoding '{0}'", _InputEncoding);
            //try
            //{
            //    // Read all lines from the file
            //    lines = File.ReadAllLines(_InputFilePath, _InputEncoding);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error while reading input file:");
            //    Console.WriteLine("Input file path: '{0}'", _InputFilePath);
            //    Console.WriteLine("Input encoding: '{0}'", _InputEncoding);
            //    Console.WriteLine("Raised Exception:");
            //    Console.WriteLine(ex);
            //    return;
            //    throw;
            //}

            //Console.WriteLine("Writing text to output file '{0}'", _OutputFilePath);
            //Console.WriteLine("Output encoding '{0}'", _OutputEncoding);
            //try
            //{
            //    // Save it !
            //    File.WriteAllLines(_OutputFilePath, lines, _OutputEncoding);

            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error while writing the encoded text to the output file:");
            //    Console.WriteLine("Output file path: '{0}'", _OutputFilePath);
            //    Console.WriteLine("Output encoding: '{0}'", _OutputEncoding);
            //    Console.WriteLine("Raised Exception:");
            //    Console.WriteLine(ex);
            //    return;
            //    throw;
            //}
            //// Done !!
            //Console.WriteLine("Conversion finished successfully.");
        }
        public void RefreshInputFilePath()
        {
            //The InputFilePath has changed. The _InputText is no more valid:
            _InputText = null;

            Trace.TraceInformation(nameof(EncodingConverterCore) + "." + nameof(this.InputFilePath) + ".set:'" + _InputFilePath + "'");
            OnInputFilePathChanged();

            //Encoding encoding;
            if (_AutoDetectInputEncoding)
            {
                DetectInputEncoding();
                //this.InputEncoding = DetectInputEncdoing(_InputFilePath, _PreferredInputEncoding);
            }
            else
            {
                this.DetectedEncodings = null;
            }
        }
        /// <summary>
        /// Converts the encoding of <paramref name="inputFile"/> from <paramref name="inputEncoding"/> to <paramref name="outputEncoding"/>
        /// and writes the result to <paramref name="outputFile"/>.
        /// </summary>
        /// <param name="inputFile">Input file to read from.</param>
        /// <param name="inputEncoding">Encoding of the input file to use while reading from.</param>
        /// <param name="outputFile">Encoding of the output to use when writing to.</param>
        /// <param name="outputEncoding">Out file to write the converted content to.</param>
        /// <remarks>If <paramref name="inputFile"/> and <paramref name="outputFile"/> are the same the <paramref name="inputFile"/>
        /// will be overwritten.</remarks>
        public static void Convert(string inputFile, Encoding inputEncoding, string outputFile, Encoding outputEncoding)
        {
            string text;
            text = File.ReadAllText(inputFile, inputEncoding);
            File.WriteAllText(outputFile, text, outputEncoding);
        }
        private static Encoding DetectInputEncoding(string inputPath, string preferredString)
        {
            if (!File.Exists(inputPath))
            {
                TraceError(nameof(DetectInputEncoding), nameof(inputPath) + " does not exist!");
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
                TraceWarning(nameof(DetectInputEncoding), "Exception while reading input file '" +
                    inputPath + $"': {Environment.NewLine}"
                    );
                ex.WriteToTraceAsError();
            }
            finally
            {
                stream?.Close();
            }

            preferredString = preferredString?.Trim();
            Encoding encoding;
            if (string.IsNullOrEmpty(preferredString))
            {
                try
                {
                    encoding = EncodingTools.DetectInputCodepage(buf);
                }
                catch (Exception ex)
                {
                    TraceWarning($"Error while detecting the encoding of the file '{inputPath}'.");
                    ex.WriteToTraceAsError();
                    TraceWarning("Execution will continue with no detected encodings.");
                    encoding = null;
                }
            }
            else
            {
                //User has provided a preferred encoding. we have to use it
                Encoding[] encodings = null;
                try
                {
                    encodings = EncodingTools.DetectInputCodepages(buf, 10);
                }
                catch (Exception ex)
                {
                    TraceWarning($"Error while detecting the encoding of the file '{inputPath}'.");
                    ex.WriteToTraceAsError();
                }
                if (encodings == null || encodings.Length <= 0)
                {
                    TraceWarning("Execution will continue with no detected encodings.");
                    encoding = null;
                }
                else
                {
                    var searchStrings = preferredString.ToLower().Split(' ');
                    var prefferedEncodings = encodings.Where(x => x.EncodingName.ToLower().Contains(searchStrings)).ToArray();
                    if (prefferedEncodings == null || prefferedEncodings.Length <= 0)
                    {
                        encoding = encodings[0];
                    }
                    else
                    {
                        TraceInformation($"Found '{prefferedEncodings.Length}' encodings with the preferred encoding text '{preferredString}'");
                        encoding = prefferedEncodings[0];
                    }
                }
            }

            return encoding;
            //encodingsTool_input.SelectedEncoding = encoding;
            //richTextBox_in.Lines = File.ReadAllLines(txtInputPath.Text, encoding);
            //isDetectingInputEncoding = false;
        }

        /// <summary>
        /// Detects the encoding of a given file path.
        /// </summary>
        /// <param name="inputPath">Path of the file to detect its encoding.</param>
        /// <param name="preferredString">A search string to be used if more that one encoding is detected.</param>
        /// <param name="maxEncodingsCount">Maximum count of encodings to be detected.</param>
        /// <param name="encodings">Returns an array of detected <see cref="Encoding"/>s.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="maxEncodingsCount"/> is less than 1.</exception>
        /// <exception cref="FileNotFoundException">If <paramref name="inputPath"/> does not exists</exception>
        private static Encoding DetectInputEncoding(string inputPath, string preferredString, int maxEncodingsCount, out Encoding[] encodings)
        {
            encodings = DetectInputEncodings(inputPath, maxEncodingsCount);
            if (encodings == null || encodings.Length <= 0)
            {
                return null;
            }

            preferredString = preferredString?.Trim();
            Encoding encoding;
            if (string.IsNullOrEmpty(preferredString))
            {
                encoding = encodings[0];// Get the first item in the detected encodings.
            }
            else
            {
                encoding = GetPreferredEncoding(encodings, preferredString);
            }

            return encoding;
        }

        private static Encoding[] DetectInputEncodings(string inputPath) { return DetectInputEncodings(inputPath, 10); }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPath"></param>
        /// <param name="maxEncodingsCount"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="maxEncodingsCount"/> is less than 1.</exception>
        /// <exception cref="FileNotFoundException">If <paramref name="inputPath"/> does not exists</exception>
        private static Encoding[] DetectInputEncodings(string inputPath, int maxEncodingsCount)
        {
            if (maxEncodingsCount < 1)
                throw new ArgumentOutOfRangeException(nameof(maxEncodingsCount), $"{nameof(maxEncodingsCount)} must be larger that 0!");

            if (!File.Exists(inputPath))
            {
                TraceError(nameof(DetectInputEncoding), nameof(inputPath) + " does not exist!");
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
            catch (OverflowException ex)
            {
                TraceWarning(nameof(DetectInputEncoding), $"A '{nameof(OverflowException)}' was encountered while reading input file '" +
                    inputPath + $". Most likely because the file is too large.': {Environment.NewLine}"
                    );
                ex.WriteToTraceAsError();
            }
            catch (Exception ex)
            {
                TraceWarning(nameof(DetectInputEncoding), "Exception while reading input file '" +
                    inputPath + $"': {Environment.NewLine}"
                    );
                ex.WriteToTraceAsError();
            }
            finally
            {
                stream?.Close();
            }

            //User has provided a preferred encoding. we have to use it
            Encoding[] encodings = null;
            try
            {
                encodings = EncodingTools.DetectInputCodepages(buf, maxEncodingsCount);
            }
            catch (Exception ex)
            {
                TraceWarning($"Error while detecting the encoding of the file '{inputPath}'.");
                ex.WriteToTraceAsError();
            }
            return encodings;
        }

        private static Encoding GetPreferredEncoding(Encoding[] encodings, string preferredString)
        {

            Encoding encoding = null;

            //User has provided a preferred encoding. we have to use it
            if (encodings == null || encodings.Length <= 0)
            {
                TraceWarning("Execution will continue with no detected encodings.");
                encoding = null;
            }
            else
            {
                var searchStrings = preferredString.ToLower().Split(' ');
                var prefferedEncodings = encodings.Where(x => x.EncodingName.ToLower().Contains(searchStrings)).ToArray();
                if (prefferedEncodings == null || prefferedEncodings.Length <= 0)
                {
                    encoding = encodings[0];
                }
                else
                {
                    TraceInformation($"Found '{prefferedEncodings.Length}' encodings with the preferred encoding text '{preferredString}'");
                    encoding = prefferedEncodings[0];
                }
            }

            return encoding;
        }

        #region ...Event invokers...
        protected void OnInputFilePathChanged() { InputFilePathChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnOutputFilePathChanged() { OutputFilePathChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnInputTextChanged() { InputTextChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnPreferredInputEncodingChanged() { PreferredInputEncodingChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnAutoDetectInputEncodingChanged() { AutoDetectInputEncodingChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnInputEncodingChanged() { InputEncodingChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnOutputEncodingChanged() { OutputEncodingChanged?.Invoke(this, EventArgs.Empty); }
        protected void OnDetectedEncodingsChanged() { DetectedEncodingsChanged?.Invoke(this, EventArgs.Empty); }

        #endregion


        #region Trace helpers
        public override string ToString()
        {
            return $"{nameof(InputFilePath)}={InputFilePath}, " +
                $"{nameof(InputEncoding)}={InputEncoding}, " +
                $"{nameof(OutputFilePath)}={OutputFilePath}, " +
                $"{nameof(OutputEncoding)}={OutputEncoding}, " + 
                $""
                ;
        }
        static void TraceInfo(string methodName, string msg) { Trace.TraceInformation(FormatTraceMessage(methodName, msg)); }
        static void TraceInformation(string msg) { Trace.TraceInformation(FormatTraceMessage(msg)); }
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
