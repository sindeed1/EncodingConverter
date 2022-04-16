using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncodingConverter.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace EncodingConverterMSTests.Commands
{
    [TestClass()]
    public class ConvertCommandTests
    {
        [TestMethod("Args: inputFile, ie:inputEncoding, oe:outputENcoding, outputFile")]
        public void ConvertCommandTest1()
        {

            ConvertCommandTest cmd = new ConvertCommandTest();

            string inputFile = "testinputfile";
            Encoding inputEncoding = Encoding.UTF8;
            string outputFile = "testoutputfile";
            Encoding outputEncoding = Encoding.ASCII;

            string[] args = new string[] { cmd.Name
                                            , inputFile
                                            , ConvertCommandTest.SwitchInputEncoding + ConvertCommandTest.DataSeparationChar + inputEncoding.HeaderName
                                            , ConvertCommandTest.SwitchOutputEncoding + ConvertCommandTest.DataSeparationChar + outputEncoding.HeaderName
                                            , outputFile
                                            };

            string inputContent = ECCTestHelper.CreateFile(inputFile, inputEncoding, 10);

            int lastArgIndex = cmd.Execute(args, 0);

            ECCTestHelper.AssertQuestion($"Command consumed all arguments length='{args.Length}'?", args.Length - 1 == lastArgIndex);

            cmd.ECC.AssertAll(inputFile, outputFile, inputEncoding, outputEncoding);
        }

        [TestMethod("Args: inputFile, ie, inputEncoding, oe, outputEncoding, outputFile")]
        public void ConvertCommandTest2()
        {

            ConvertCommandTest cmd = new ConvertCommandTest();

            string inputFile = "testinputfile";
            Encoding inputEncoding = Encoding.UTF8;
            string outputFile = "testoutputfile";
            Encoding outputEncoding = Encoding.ASCII;

            string[] args = new string[] { cmd.Name
                                            , inputFile
                                            , ConvertCommandTest.SwitchInputEncoding
                                            , inputEncoding.HeaderName
                                            , ConvertCommandTest.SwitchOutputEncoding
                                            , outputEncoding.HeaderName
                                            , outputFile
                                            };

            string inputContent = ECCTestHelper.CreateFile(inputFile, inputEncoding, 10);

            int lastArgIndex = cmd.Execute(args, 0);

            ECCTestHelper.AssertQuestion($"Command consumed all arguments length='{args.Length}'?", args.Length - 1 == lastArgIndex);

            cmd.ECC.AssertAll(inputFile, outputFile, inputEncoding, outputEncoding);
        }

        [TestMethod("Args: inputFile, oe, outputEncoding, outputFile")]
        public void ConvertCommandTest3()
        {

            ConvertCommandTest cmd = new ConvertCommandTest();

            string inputFile = "testinputfile";
            Encoding inputEncoding = Encoding.Unicode;
            string outputFile = "testoutputfile";
            Encoding outputEncoding = Encoding.UTF7;

            string[] args = new string[] { cmd.Name
                                            , inputFile
                                            , ConvertCommandTest.SwitchOutputEncoding
                                            , outputEncoding.HeaderName
                                            , outputFile
                                            };

            string inputContent = ECCTestHelper.CreateFile(inputFile, inputEncoding, 10);

            int lastArgIndex = cmd.Execute(args, 0);

            ECCTestHelper.AssertQuestion($"Command consumed all arguments length='{args.Length}'?", args.Length - 1 == lastArgIndex);

            cmd.ECC.AssertAll(inputFile, outputFile, inputEncoding, outputEncoding);
        }

        [TestMethod("Args: inputFile, " + ConvertCommandTest.SwitchOutputEncoding + ", outputEncoding, " + ConvertCommandTest.SwitchOutputPathFormat + ", outputPathFormat")]
        public void ConvertCommandTest4()
        {

            ConvertCommandTest cmd = new ConvertCommandTest();

            string inputFile = "testinputfile";
            Encoding inputEncoding = Encoding.Unicode;
            string outputFile = inputFile + ".test";
            Encoding outputEncoding = Encoding.UTF7;
            string outputPathFormat = "{1}.test";

            string[] args = new string[] { cmd.Name
                                            , inputFile
                                            , ConvertCommandTest.SwitchOutputEncoding
                                            , outputEncoding.HeaderName
                                            , ConvertCommandTest.SwitchOutputPathFormat
                                            , outputPathFormat
                                            };

            string inputContent = ECCTestHelper.CreateFile(inputFile, inputEncoding, 10);

            int lastArgIndex = cmd.Execute(args, 0);

            ECCTestHelper.AssertQuestion($"Command consumed all arguments length='{args.Length}'?", args.Length - 1 == lastArgIndex);

            ECCTestHelper.AssertQuestion($"ECC.Output path format'{cmd.OPF?.FormatString}' = '{outputPathFormat}'?", cmd.OPF?.FormatString == outputPathFormat);

            cmd.ECC.AssertAll(inputFile, outputFile, inputEncoding, outputEncoding);
        }

        [TestMethod("Args: inputFile, " + ConvertCommandTest.SwitchOutputPathFormat + ", outputPathFormat")]
        public void ConvertCommandTest5()
        {

            ConvertCommandTest cmd = new ConvertCommandTest();

            string inputFile = "testinputfile";
            Encoding inputEncoding = Encoding.Unicode;
            string outputFile = inputFile + ".test";
            Encoding outputEncoding = Encoding.UTF8;
            string outputPathFormat = "{1}.test";

            string[] args = new string[] { cmd.Name
                                            , inputFile
                                            , ConvertCommandTest.SwitchOutputPathFormat
                                            , outputPathFormat
                                            };

            string inputContent = ECCTestHelper.CreateFile(inputFile, inputEncoding, 10);

            int lastArgIndex = cmd.Execute(args, 0);

            ECCTestHelper.AssertQuestion($"Command consumed all arguments length='{args.Length}'?", args.Length - 1 == lastArgIndex);

            ECCTestHelper.AssertQuestion($"ECC.Output path format'{cmd.OPF?.FormatString}' = '{outputPathFormat}'?", cmd.OPF?.FormatString == outputPathFormat);

            cmd.ECC.AssertAll(inputFile, outputFile, inputEncoding, outputEncoding);
        }

    }
}
