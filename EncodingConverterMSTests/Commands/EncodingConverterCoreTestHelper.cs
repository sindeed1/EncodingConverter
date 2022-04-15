using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncodingConverter.Commands.Tests
{
    static internal class EncodingConverterCoreTestHelper
    {
        public static void AssertECCInputFile(this EncodingConverterCore ecc, string inputFile)
        {
            AssertQuestion($"ECC.InputFilePath.Contains('{inputFile}') ?"
                , !string.IsNullOrWhiteSpace(ecc.InputFilePath)
                    && ecc.InputFilePath.ToLower().Contains(inputFile));
        }
        public static void AssertECCOutputFile(this EncodingConverterCore ecc, string outputFile)
        {
            AssertQuestion($"ECC.OutputFilePath.Contains('{outputFile}') ?"
                , !string.IsNullOrWhiteSpace(ecc.OutputFilePath)
                    && ecc.OutputFilePath.ToLower().Contains(outputFile));
        }
        public static void AssertECCOutputEncoding(this EncodingConverterCore ecc, string encodingArg)
        {
            AssertQuestion($"ECC.OutputEncoding = '{encodingArg}' ?"
            , ecc.OutputEncoding != null
                && (ecc.OutputEncoding.BodyName.ToLower().Contains(encodingArg)
                    || ecc.OutputEncoding.EncodingName.ToLower().Contains(encodingArg)));
        }
        public static void AssertECCInputEncoding(this EncodingConverterCore ecc, string encodingArg)
        {
            AssertQuestion($"ECC.InputEncoding = '{encodingArg}' ?"
            , ecc.InputEncoding != null
                && (ecc.InputEncoding.BodyName.ToLower().Contains(encodingArg)
                    || ecc.InputEncoding.EncodingName.ToLower().Contains(encodingArg)));
        }

        public static void AssertQuestion(string questionMsg, bool condition)
        {
            Trace.WriteLine("");
            Trace.Write(questionMsg);
            if (condition)
            {
                Trace.Write("[OK]");
            }
            else
            {
                Trace.Write("[ERROR!]");
            }
            Assert.IsTrue(condition);
        }

    }
}
