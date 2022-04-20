using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using CommandLine;

namespace EncodingConverter
{
    /// <summary>
    /// Contains common command line processing methods.
    /// </summary>
    static class CommandLine
    {
        public const string CLARG_InputEncoding = "/ie";
        public const string CLARG_OutputEncoding = "/oe";
        public const char CLARG_DataSeparator = ':';

        /// <summary>
        /// Reads the argument. If it's an InputEncoding argument, loads the appropriate inputEncoding to the <see cref="Program.ECC"/>.
        /// </summary>
        /// <param name="args">The command line arguments</param>
        /// <param name="startingIndex">Index of the argument that represents the input encoding switch.</param>
        /// <returns></returns>
        public static int ProcessInputEncodingCLArg(string[] args, int startingIndex)
        {
            //string arg = args[startingIndex];

            string switchName = CLARG_InputEncoding;

            string switchData;
            int lastArgsIndex = GetSwitchData(args, startingIndex, switchName, CLARG_DataSeparator, out switchData);
            if (lastArgsIndex < startingIndex || string.IsNullOrEmpty(switchData))
            {
                Trace.TraceWarning($"Argument '{args[startingIndex]}' is not input encoding switch '{switchName}'!");
                return lastArgsIndex;
            }

            EncodingInfo encodingInfo;
            encodingInfo = GetEncodingInfoFromSwitchData(switchData);
            if (encodingInfo == null)
            {
                Console.WriteLine($"Switch InputEncoding '{switchName}' does not provide a recognizable code page '{switchData}'.");
            }
            else
            {
                Console.WriteLine($"Input encoding '{encodingInfo.DisplayName}'.");
                Program.ECC.InputEncoding = encodingInfo.GetEncoding();
            }

            return lastArgsIndex;
        }



        /// <summary>
        /// Reads the argument. If it's an OutputEncoding argument, loads the appropriate outputEncoding to the <see cref="Program.ECC"/>.
        /// </summary>
        /// <param name="arg">The command line argument to read</param>
        /// <returns></returns>
        public static int ProcessOutputEncodingCLArg(string[] args, int startingIndex)
        {
            string arg = args[startingIndex];

            string switchName = CLARG_OutputEncoding;

            string switchData;
            int lastArgsIndex = GetSwitchData(args, startingIndex, switchName, CLARG_DataSeparator, out switchData);
            if (lastArgsIndex < startingIndex || string.IsNullOrEmpty(switchData))
            {
                Trace.TraceWarning($"Argument '{args[startingIndex]}' is not output encoding switch '{switchName}'!");
                return lastArgsIndex;
            }

            EncodingInfo encodingInfo;
            encodingInfo = GetEncodingInfoFromSwitchData(switchData);
            if (encodingInfo == null)
            {
                Console.WriteLine($"Switch OutputEncoding '{switchName}' does not provide a recognizable code page '{switchData}'.");
            }
            else
            {
                Console.WriteLine($"Output encoding '{encodingInfo.DisplayName}'.");
                Program.ECC.OutputEncoding = encodingInfo.GetEncoding();
            }

            return lastArgsIndex;
        }
        /// <summary>
        /// Get data of a switch from arguments. Supports data-separator mode and argument mode for data retrieval.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="startingIndex"></param>
        /// <param name="switchName"></param>
        /// <param name="switchDataSeparator"></param>
        /// <param name="switchData"></param>
        /// <returns></returns>
        /// <remarks>Supports 2 modes of switches:
        /// <para>Data-separator mode: In this mode there is separator of type <see cref="char"/> directly after the switch name and the 
        /// switch data comes directly after that within the same argument.</para>
        /// <para>Argument mode: Here there is data separator after the switch name and the switch data
        /// comes in the following argument</para>
        /// </remarks>
        public static int GetSwitchData(this string[] args, int startingIndex, string switchName, char switchDataSeparator, out string switchData)
        {
            string arg = args[startingIndex];
            switchData = string.Empty;
            int switchDataIndex;

            if (!arg.IsSwitch(switchName))
                return startingIndex - 1;


            if (arg.Length > switchName.Length && arg[switchName.Length] == switchDataSeparator)
            {
                //We are in the data-separator mode.
                //In this mode the data comes directly after the switch in the same argument:
                switchData = arg.GetSwitchData(switchName + switchDataSeparator);//
                switchDataIndex = startingIndex;
            }
            else
            {
                //We are in the argument mode.
                //In this mode the data comes in the directly following argument:
                switchData = args[startingIndex + 1];
                switchDataIndex = startingIndex + 1;
            }

            return switchDataIndex;
        }

        public static int ProcessNoSwitch(string[] args, int startinIndex)
        {
            if (string.IsNullOrWhiteSpace(Program.ECC.InputFilePath))
            {
                Program.ECC.InputFilePath = args[startinIndex];
            }
            else
            {
                Program.ECC.OutputFilePath = args[startinIndex];
            }
            return startinIndex;
        }


        public static EncodingInfo GetEncodingInfoFromSwitchData(string switchData)
        {
            EncodingInfo encodingInfo;
            int codePage;
            if (Int32.TryParse(switchData, out codePage))
            {
                encodingInfo = Program.ECC.Encodings.FirstOrDefault(x => x.CodePage == codePage);
            }
            else
            {
                encodingInfo = Program.ECC.Encodings.FirstOrDefault(x => x.Name == switchData);
            }

            return encodingInfo;
        }

    }
}
