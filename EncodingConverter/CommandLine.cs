using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter
{
    static class CommandLine
    {
        const string CLARG_InputEncoding = "ie:";
        const string CLARG_OutputEncoding = "oe:";

        /// <summary>
        /// Reads the argument. If it's an InputEncoding argument, loads the appropriate inputEncoding to the <see cref="Program.ECC"/>.
        /// </summary>
        /// <param name="arg">The command line argument to read</param>
        /// <returns></returns>
        public static bool ProcessInputEncodingCLArg(string arg)
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
                Program.ECC.InputEncoding = encodingInfo.GetEncoding();
            }

            return true;
        }

        /// <summary>
        /// Reads the argument. If it's an OutputEncoding argument, loads the appropriate outputEncoding to the <see cref="Program.ECC"/>.
        /// </summary>
        /// <param name="arg">The command line argument to read</param>
        /// <returns></returns>
        public static bool ProcessOutputEncodingCLArg(string arg)
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
                Program.ECC.OutputEncoding = encodingInfo.GetEncoding();
            }

            return true;
        }

        public static bool ProcessNoSwitch(string arg)
        {
            if (string.IsNullOrWhiteSpace(Program.ECC.InputFilePath))
            {
                Program.ECC.InputFilePath = arg;
            }
            else
            {
                Program.ECC.OutputFilePath = arg;
            }
            return true;
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
