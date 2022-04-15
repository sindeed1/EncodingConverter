using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncodingConverter.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
//using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace EncodingConverter.Commands.Tests
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

            string inputContent = CreateFile(inputFile, inputEncoding, 10);

            int lastArgIndex = cmd.Execute(args, 0);

            ECCTestHelper.AssertQuestion($"Command consumed all arguments length='{args.Length}'?", args.Length - 1 == lastArgIndex);

            cmd.ECC.AssertAll(inputFile,outputFile,inputEncoding,outputEncoding);
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

            string inputContent = CreateFile(inputFile, inputEncoding, 10);

            int lastArgIndex = cmd.Execute(args, 0);

            ECCTestHelper.AssertQuestion($"Command consumed all arguments length='{args.Length}'?", args.Length - 1 == lastArgIndex);

            cmd.ECC.AssertAll(inputFile, outputFile, inputEncoding, outputEncoding);
        }


        static string CreateFile(string path, Encoding encoding, int length)
        {
            string str = new string('1', length);

            File.WriteAllText(path, str, encoding);
            return str;
        }
    }
}
