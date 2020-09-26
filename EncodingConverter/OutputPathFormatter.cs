using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace EncodingConverter
{
    class OutputPathFormatter
    {
        private EncodingConverterCore _ECC;
        private object[] _OutputPathFormattingParameters;

        private string _FormatString;

        #region ...ctor...
        public OutputPathFormatter()
        {
            _OutputPathFormattingParameters = new object[23];
        }
        public OutputPathFormatter(EncodingConverterCore ecc)
        {
            _OutputPathFormattingParameters = new object[23];
            this.ECC = ecc;
        }
        public OutputPathFormatter(EncodingConverterCore ecc, string formatString)
        {
            _OutputPathFormattingParameters = new object[23];
            this.ECC = ecc;
            this.FormatString = formatString;
        }
        #endregion
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
                UpdateFormattedText();
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
            string fileExtention;
            string fileName;

            if (string.IsNullOrEmpty(inputPath))
            {
                directory = fileExtention = fileName = "";
            }
            else
            {
                FileInfo file = new FileInfo(inputPath);
                directory = file.DirectoryName;
                Trace.TraceInformation("Old current directory '" + Directory.GetCurrentDirectory() + "'");
                Directory.SetCurrentDirectory(directory);
                Trace.TraceInformation("New current directory '" + Directory.GetCurrentDirectory() + "'");

                fileExtention = file.Extension;
                fileName = file.Name;
                fileName = fileName.Substring(0, fileName.Length - fileExtention.Length);
                fileExtention = fileExtention.TrimStart('.');
            }

            _OutputPathFormattingParameters[0] = directory;                     //{0} directory path
            _OutputPathFormattingParameters[1] = fileName;                      //{1} file name without extension
            _OutputPathFormattingParameters[2] = fileExtention;                 //{2} extension

            UpdateFormattedText();
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

        void UpdateFormattedText()
        {
            if (_ECC == null)
                return;
            _ECC.OutputFilePath = this.FormatOutputpath();
        }
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
