using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace EncodingConverter
{
    //, directory                     //{0} directory path
    //, fileName                      //{1} file name without extension
    //, fileExtention                 //{2} extension
    //, "", "", "", "", "", "", ""    //{3-9} reserved and empty
    //, outputEncoding.EncodingName   //{10} Output encoding name
    //, outputEncoding.BodyName       //{11} Output encoding Body name
    //, outputEncoding.CodePage       //{12} Output encoding Code page
    //, "", "", "", "", "", "", ""    //{13-19} reserved and empty
    //, inputEncoding.EncodingName    //{20} Input encoding name
    //, inputEncoding.BodyName        //{21} Input encoding Body name
    //, inputEncoding.CodePage        //{22} Input encoding Code page
    //, "", "", "", "", "", "", ""    //{23-29} reserved and empty
    //, CompanionFile directory       //{30} directory path of the companion file
    //, CompanionFile fileName        //{31} file name without extension of the companion file
    //, CompanionFile fileExtention   //{32} extension of the companion file
    //, "", "", "", "", "", "", ""    //{33-39} reserved and empty

    /// <summary>
    /// Formats the Output path using parameters calculated from different sources.
    /// </summary>
    /// <remarks>The <see cref="OutputPathFormatter"/> is an attached sub-system that uses the 
    /// <see cref="EncodingConverterCore"/> as one of the sources. It will update the formated output path back to the
    /// <see cref="EncodingConverterCore"/>.</remarks>
    class OutputPathFormatter
    {
        static char[] _SplitChars = { '|' };

        public static string PARAM_InputFile_DirectoryPath = "{0}";// directory path;
        public static string PARAM_InputFile_FileNameWithoutExtention = "{1}";// file name without extension
        public static string PARAM_InputFile_Extention = "{2}";// extension

        public event EventHandler FormatStringChanged;
        public event EventHandler CompanionFileSearchPatternChanged;
        public event EventHandler CompanionFileChanged;

        private EncodingConverterCore _ECC;
        private object[] _OutputPathFormattingParameters;

        private string _FormatString;
        private string _CompanionFileSearchPattern;
        private string[] _CompanionFileSearchPatterns;
        private string _CompanionFile;

        #region ...ctor...
        public OutputPathFormatter() : this(null, null, null) { }
        public OutputPathFormatter(EncodingConverterCore ecc) : this(ecc, null, null) { }
        public OutputPathFormatter(EncodingConverterCore ecc, string formatString) : this(ecc, formatString, null) { }
        public OutputPathFormatter(EncodingConverterCore ecc, string formatString, string companionFileSearchPattern)
        {
            _OutputPathFormattingParameters = new object[33];
            this.ECC = ecc;
            this.FormatString = formatString;
            this.CompanionFileSearchPattern = companionFileSearchPattern;
        }
        #endregion


        public string CompanionFile
        {
            get { return _CompanionFile; }
            set
            {
                if (_CompanionFile == value)
                    return;
                _CompanionFile = value;
                Trace.TraceInformation(nameof(OutputPathFormatter) + "." + nameof(this.CompanionFile) + ".set:'" + value + "'");

                CompanionFileChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string CompanionFileSearchPattern
        {
            get { return _CompanionFileSearchPattern; }
            set
            {
                if (_CompanionFileSearchPattern == value)
                    return;

                _CompanionFileSearchPattern = value;
                _CompanionFileSearchPatterns = _CompanionFileSearchPattern.Split(_SplitChars, StringSplitOptions.RemoveEmptyEntries);
                Trace.TraceInformation(nameof(OutputPathFormatter) + "." + nameof(this.CompanionFileSearchPattern) + ".set:'" + value + "'");


                UpdateFormattedText();
                CompanionFileSearchPatternChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public string FormatString
        {
            get { return _FormatString; }
            set
            {
                if (_FormatString == value)
                {
                    return;
                }
                _FormatString = value;
                Trace.TraceInformation(nameof(OutputPathFormatter) + "." + nameof(this.FormatString) + ".set:'" + value + "'");

                UpdateFormattedText();
                FormatStringChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public EncodingConverterCore ECC
        {
            get { return _ECC; }
            set
            {
                if (_ECC == value)
                {
                    return;
                }
                if (_ECC != null)
                    UnwireECC(_ECC);

                _ECC = value;

                if (_ECC != null)
                    WireECC(_ECC);

            }
        }


        void WireECC(EncodingConverterCore ecc)
        {
            ecc.InputEncodingChanged += Ecc_InputEncodingChanged;
            ecc.OutputEncodingChanged += Ecc_OutputEncodingChanged;
            ecc.InputFilePathChanged += Ecc_InputFilePathChanged;
        }
        void UnwireECC(EncodingConverterCore ecc)
        {
            ecc.InputEncodingChanged -= Ecc_InputEncodingChanged;
            ecc.OutputEncodingChanged -= Ecc_OutputEncodingChanged;
            ecc.InputFilePathChanged -= Ecc_InputFilePathChanged;
        }

        #region ...Events handlers...
        private void Ecc_InputFilePathChanged(object sender, EventArgs e)
        {
            //string formatString = Program.Settings.OutputFilePathFormatString;
            string inputPath = _ECC.InputFilePath;
            string directory;
            //string fileExtention;
            //string fileName;

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                //directory = fileExtention = fileName = "";
                FillPathParemeters(_OutputPathFormattingParameters, 0, null);
                FillPathParemeters(_OutputPathFormattingParameters, 30, null);
                //_CompanionFile = "";
                this.CompanionFile = "";
            }
            else
            {
                FileInfo file = new FileInfo(inputPath);
                directory = file.DirectoryName;
                Trace.TraceInformation($"{nameof(OutputPathFormatter)}: Old current directory '{Directory.GetCurrentDirectory()}'");
                Directory.SetCurrentDirectory(directory);
                Trace.TraceInformation($"{nameof(OutputPathFormatter)}: New current directory '{Directory.GetCurrentDirectory()}'");

                //Fill the formatting patterns with patterns that correspond to InputFile.
                //Those formatting arguments begins at index of 0:
                FillPathParemeters(_OutputPathFormattingParameters, 0, file);

                if (!string.IsNullOrWhiteSpace(_CompanionFileSearchPattern) && _CompanionFileSearchPatterns != null)
                {
                    var dir = file.Directory;
                    FileInfo compFile = null;
                    object[] inputFileFormattingParams = new object[3];
                    Array.Copy(_OutputPathFormattingParameters, 0, inputFileFormattingParams, 0, 3);
                    for (int i = 0; i < _CompanionFileSearchPatterns.Length; i++)
                    {
                        var searchPattern = _CompanionFileSearchPatterns[i];
                        searchPattern = string.Format(searchPattern, _OutputPathFormattingParameters);
                        var commpFiles = dir.EnumerateFiles(searchPattern, SearchOption.TopDirectoryOnly);
                        compFile = commpFiles.FirstOrDefault();
                        if (compFile != null)
                        {
                            break;
                        }
                    }
                    //var commps = dir.EnumerateFiles(_CompanionFileSearchPattern, SearchOption.TopDirectoryOnly);
                    //var comp = commps.FirstOrDefault();
                    FillPathParemeters(_OutputPathFormattingParameters, 30, compFile);
                    //_CompanionFile = compFile?.FullName;
                    this.CompanionFile = compFile?.FullName;
                }

                //fileExtention = file.Extension;
                //fileName = file.Name;
                //fileName = fileName.Substring(0, fileName.Length - fileExtention.Length);
                //fileExtention = fileExtention.TrimStart('.');
            }

            //_OutputPathFormattingParameters[0] = directory;                     //{0} directory path
            //_OutputPathFormattingParameters[1] = fileName;                      //{1} file name without extension
            //_OutputPathFormattingParameters[2] = fileExtention;                 //{2} extension

            //_OutputPathFormattingParameters[0] = directory;                     //{30} directory path
            //_OutputPathFormattingParameters[1] = fileName;                      //{31} file name without extension
            //_OutputPathFormattingParameters[2] = fileExtention;                 //{32} extension

            UpdateFormattedText();
        }

        void FillPathParemeters(object[] parameters, int startingIndex, FileInfo file)
        {
            string directory;
            string fileExtention;
            string fileName;

            if (file == null)
            {
                directory = fileExtention = fileName = "";
            }
            else
            {
                directory = file.DirectoryName;
                fileExtention = file.Extension;
                fileName = file.Name;
                fileName = fileName.Substring(0, fileName.Length - fileExtention.Length);
                fileExtention = fileExtention.TrimStart('.');
            }

            _OutputPathFormattingParameters[startingIndex] = directory;                     //{0} directory path
            _OutputPathFormattingParameters[startingIndex + 1] = fileName;                      //{1} file name without extension
            _OutputPathFormattingParameters[startingIndex + 2] = fileExtention;                 //{2} extension
        }
        private void Ecc_OutputEncodingChanged(object sender, EventArgs e)
        {
            Encoding outputEncoding = _ECC.OutputEncoding;
            if (outputEncoding == null)
            {
                _OutputPathFormattingParameters[10] = "";   //{10} Output encoding name
                _OutputPathFormattingParameters[11] = "";       //{11} Output encoding Body name
                _OutputPathFormattingParameters[12] = "";       //{12} Output encoding Code page

            }
            else
            {
                _OutputPathFormattingParameters[10] = outputEncoding.EncodingName;   //{10} Output encoding name
                _OutputPathFormattingParameters[11] = outputEncoding.BodyName;       //{11} Output encoding Body name
                _OutputPathFormattingParameters[12] = outputEncoding.CodePage;       //{12} Output encoding Code page
                                                                                     //{13-19} reserved and empty
            }
            UpdateFormattedText();
        }
        private void Ecc_InputEncodingChanged(object sender, EventArgs e)
        {
            Encoding inputEncoding = _ECC.InputEncoding;
            if (inputEncoding == null)
            {
                _OutputPathFormattingParameters[20] = "";    //{20} Input encoding name
                _OutputPathFormattingParameters[21] = "";        //{21} Input encoding Body name
                _OutputPathFormattingParameters[22] = "";        //{22} Input encoding Code page
            }
            else
            {
                _OutputPathFormattingParameters[20] = inputEncoding.EncodingName;    //{20} Input encoding name
                _OutputPathFormattingParameters[21] = inputEncoding.BodyName;        //{21} Input encoding Body name
                _OutputPathFormattingParameters[22] = inputEncoding.CodePage;        //{22} Input encoding Code page
            }
            UpdateFormattedText();
        }
        #endregion

        /// <summary>
        /// Updates the <see cref="EncodingConverterCore.OutputFilePath"/> with the
        /// new formatted value.
        /// </summary>
        void UpdateFormattedText()
        {
            if (_ECC == null)
                return;
            _ECC.OutputFilePath = this.FormatOutputpath();
        }
        /// <summary>
        /// Returns a formatted output path.
        /// </summary>
        /// <returns></returns>
        public string FormatOutputpath()
        {
            if (string.IsNullOrEmpty(_FormatString))
            {
                return "";
            }
            try
            {
                return string.Format(_FormatString, _OutputPathFormattingParameters);
            }
            catch (FormatException ex)
            {
                Trace.TraceWarning(ex.ToText());
                return "FORMAT ERROR!";
            }
        }

        //public string FormatOutputpath(string inputPath, Encoding inputEncoding, Encoding outputEncoding)
        //{
        //    if (_OutputPathFormattingParameters == null)
        //    {
        //        _OutputPathFormattingParameters = new object[23];
        //    }

        //    string formatString = Program.Settings.OutputFilePathFormatString;
        //    FileInfo file = new FileInfo(inputPath);
        //    string directory = file.DirectoryName;
        //    Trace.TraceInformation("Old current directory '" + Directory.GetCurrentDirectory() + "'");
        //    Directory.SetCurrentDirectory(directory);
        //    Trace.TraceInformation("New current directory '" + Directory.GetCurrentDirectory() + "'");

        //    string fileExtention = file.Extension;
        //    string fileName = file.Name;
        //    fileName = fileName.Substring(0, fileName.Length - fileExtention.Length);
        //    fileExtention = fileExtention.TrimStart('.');

        //    _OutputPathFormattingParameters[0] = directory;                     //{0} directory path
        //    _OutputPathFormattingParameters[1] = fileName;                      //{1} file name without extension
        //    _OutputPathFormattingParameters[2] = fileExtention;                 //{2} extension
        //                                                                        //{3-9} reserved and empty
        //    if (outputEncoding != null)
        //    {
        //        _OutputPathFormattingParameters[10] = outputEncoding.EncodingName;   //{10} Output encoding name
        //        _OutputPathFormattingParameters[11] = outputEncoding.BodyName;       //{11} Output encoding Body name
        //        _OutputPathFormattingParameters[12] = outputEncoding.CodePage;       //{12} Output encoding Code page
        //                                                                             //{13-19} reserved and empty
        //    }
        //    if (inputEncoding != null)
        //    {
        //        _OutputPathFormattingParameters[20] = inputEncoding.EncodingName;    //{20} Input encoding name
        //        _OutputPathFormattingParameters[21] = inputEncoding.BodyName;        //{21} Input encoding Body name
        //        _OutputPathFormattingParameters[22] = inputEncoding.CodePage;        //{22} Input encoding Code page
        //    }

        //    string result;
        //    result = string.Format(formatString, _OutputPathFormattingParameters);
        //    //, directory                     //{0} directory path
        //    //, fileName                      //{1} file name without extension
        //    //, fileExtention                 //{2} extension
        //    //, "", "", "", "", "", "", ""    //{3-9} reserved and empty
        //    //, outputEncoding.EncodingName   //{10} Output encoding name
        //    //, outputEncoding.BodyName       //{11} Output encoding Body name
        //    //, outputEncoding.CodePage       //{12} Output encoding Code page
        //    //, "", "", "", "", "", "", ""    //{13-19} reserved and empty
        //    //, inputEncoding.EncodingName   //{20} Input encoding name
        //    //, inputEncoding.BodyName       //{21} Input encoding Body name
        //    //, inputEncoding.CodePage       //{22} Input encoding Code page
        //    ////, "", "", "", "", "", "", ""    //{23-29} reserved and empty
        //    //);

        //    return result;
        //}

    }
}
