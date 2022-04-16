using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using EncodingConverter;

namespace EncodingConverterMSTests
{
    static internal class ECCTestHelper
    {
        public static void AssertAll(this EncodingConverterCore ecc, string inputFile, string outputFile, Encoding ie, Encoding oe)
        {
            ecc.AssertConversion();

            ecc.AssertECCInputEncoding(ie.HeaderName);
            ecc.AssertECCInputFile(inputFile);
            ecc.AssertECCOutputEncoding(oe.HeaderName);
            ecc.AssertECCOutputFile(outputFile);
        }

        public static void AssertConversion(this EncodingConverterCore ecc)
        {
            string input = File.ReadAllText(ecc.InputFilePath, ecc.InputEncoding);
            string output = File.ReadAllText(ecc.OutputFilePath, ecc.OutputEncoding);

            AssertQuestion("File converted successfully?", input == output);
            //Assert.AreEqual(input, output);
        }
        public static void AssertECCInputFile(this EncodingConverterCore ecc, string inputFile)
        {
            AssertQuestion($"ECC.InputFilePath'{ecc.InputFilePath}'.Contains('{inputFile}') ?"
                , !string.IsNullOrWhiteSpace(ecc.InputFilePath)
                    && ecc.InputFilePath.ToLower().Contains(inputFile));
        }
        public static void AssertECCOutputFile(this EncodingConverterCore ecc, string outputFile)
        {
            AssertQuestion($"ECC.OutputFilePath'{ecc.OutputFilePath}'.Contains('{outputFile}') ?"
                , !string.IsNullOrWhiteSpace(ecc.OutputFilePath)
                    && ecc.OutputFilePath.ToLower().Contains(outputFile));
        }
        public static void AssertECCOutputEncoding(this EncodingConverterCore ecc, string encodingArg)
        {
            AssertQuestion($"ECC.OutputEncoding'{ecc.OutputEncoding?.BodyName}' = '{encodingArg}' ?"
            , ecc.OutputEncoding != null
                && (ecc.OutputEncoding.BodyName.ToLower().Contains(encodingArg)
                    || ecc.OutputEncoding.EncodingName.ToLower().Contains(encodingArg)));
        }
        public static void AssertECCInputEncoding(this EncodingConverterCore ecc, string encodingArg)
        {
            AssertQuestion($"ECC.InputEncoding'{ecc.InputEncoding?.BodyName}' = '{encodingArg}' ?"
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

        public static string CreateFile(string path, Encoding encoding, int length)
        {
            string str = new string('1', length);

            File.WriteAllText(path, str, encoding);
            return str;
        }

    }
}
