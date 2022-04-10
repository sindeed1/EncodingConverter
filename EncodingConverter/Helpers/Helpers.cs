using EncodingConverter.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace EncodingConverter
{
    public class EncodingsCollection : List<int>
    {
        public EncodingsCollection() { }
        public EncodingsCollection(int capacity) : base(capacity) { }
    }
    static class Helper
    {
        /// <summary>
        /// Searches a given <paramref name="sourceString"/> for the existence of <paramref name="searchStrings"/>.
        /// All the given must be present to return true.
        /// </summary>
        /// <param name="sourceString"></param>
        /// <param name="searchStrings"></param>
        /// <returns></returns>
        public static bool Contains(this string sourceString, params string[] searchStrings)
        {
            //var searchStrings = searchText.Split(' ');
            //searchText = searchText.ToLower();
            for (int i = 0; i < searchStrings.Length; i++)
            {
                var txt = searchStrings[i];//.ToLower();
                if (!sourceString.Contains(txt))
                    return false;
            }
            return true;
        }

        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }
        public static void For<T>(this IList<T> list, Action<T> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                action(list[i]);
            }
        }
        public static void Foreach(this IEnumerable enumerable, Action<object> action)
        {
            foreach (object item in enumerable)
            {
                action(item);
            }
        }
        public static void For(this IList list, Action<object> action)
        {
            for (int i = 0; i < list.Count; i++)
            {
                action(list[i]);
            }
        }

    }
    static class ExceptionHelper
    {
        /// <summary>
        /// Writes the <see cref="Exception"/> to the trace as <see cref="Trace.TraceError(string)"/>.
        /// </summary>
        /// <param name="ex"></param>
        public static void WriteToTrace(this Exception ex)
        {
            Trace.TraceError(ex.ToText());
        }
        public static string ToText(this Exception ex)
        {
            string result = string.Empty;
            result = result + nameof(ex.Message) + ": " + ex?.Message;
            result = result + nameof(ex.StackTrace) + ": " + ex.StackTrace;
            result = result + nameof(ex.InnerException) + ": " + ex.InnerException?.Message;

            return result;
        }
    }
    

    static class EncodingHelper
    {
        static object[] _OutputPathFormattingParameters;

        public static string FormatOutputpath(string inputPath, Encoding inputEncoding, Encoding outputEncoding)
        {
            if (_OutputPathFormattingParameters == null)
            {
                _OutputPathFormattingParameters = new object[23];
            }

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
            if (outputEncoding != null)
            {
                _OutputPathFormattingParameters[10] = outputEncoding.EncodingName;   //{10} Output encoding name
                _OutputPathFormattingParameters[11] = outputEncoding.BodyName;       //{11} Output encoding Body name
                _OutputPathFormattingParameters[12] = outputEncoding.CodePage;       //{12} Output encoding Code page
                                                                                     //{13-19} reserved and empty
            }
            if (inputEncoding != null)
            {
                _OutputPathFormattingParameters[20] = inputEncoding.EncodingName;    //{20} Input encoding name
                _OutputPathFormattingParameters[21] = inputEncoding.BodyName;        //{21} Input encoding Body name
                _OutputPathFormattingParameters[22] = inputEncoding.CodePage;        //{22} Input encoding Code page
            }

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

        /// <summary>
        /// Determines if two <see cref="Encoding"/>s are equal. Null is acceptable.
        /// </summary>
        /// <param name="enc1"></param>
        /// <param name="enc2"></param>
        /// <returns></returns>
        public static bool EqualsEncoding(this Encoding enc1, Encoding enc2)
        {
            return (enc1 == enc2)//Either the two encodings are the same
                    || (enc1 != null && enc2 != null && enc1.CodePage == enc2.CodePage);//Or they have the same CodePage.
        }

    }
}
