#if DEBUG == true
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
using EncodingConverter;

namespace EncodingConverterMSTests
{
    [TestClass()]
    public class ECCTests
    {
        [TestMethod()]
        public void ECCInputFileTest()
        {
            EncodingConverterCore ecc = new EncodingConverterCore();

            string inputFile = "testinputfile";
            ecc.InputFilePath = inputFile;
            Assert.AreEqual(inputFile, ecc.InputFilePath);

            inputFile = "testinputfile2";
            ecc.InputFilePath = inputFile;
            Assert.AreEqual(inputFile, ecc.InputFilePath);
        }
        [TestMethod()]
        public void ECCOutputFileTest()
        {

            EncodingConverterCore ecc = new EncodingConverterCore();

            string outputFile = "testoutputfile";
            ecc.OutputFilePath = outputFile;
            Assert.AreEqual(outputFile, ecc.OutputFilePath);

            outputFile = "testoutputfile2";
            ecc.OutputFilePath = outputFile;
            Assert.AreEqual(outputFile, ecc.OutputFilePath);
        }
        [TestMethod()]
        public void ECCInputEncodingTest()
        {

            EncodingConverterCore ecc = new EncodingConverterCore();

            Encoding ie = Encoding.ASCII;
            ecc.InputEncoding = ie;
            Assert.AreEqual(ie, ecc.InputEncoding);

            ie = Encoding.UTF8;
            ecc.InputEncoding = ie;
            Assert.AreEqual(ie, ecc.InputEncoding);
        }
        [TestMethod()]
        public void ECCOutputEncodingTest()
        {

            EncodingConverterCore ecc = new EncodingConverterCore();

            Encoding oe = Encoding.ASCII;
            ecc.OutputEncoding = oe;
            Assert.AreEqual(oe, ecc.OutputEncoding);

            oe = Encoding.UTF8;
            ecc.OutputEncoding = oe;
            Assert.AreEqual(oe, ecc.OutputEncoding);
        }

        [TestMethod()]
        public void ECCAutoDetectTest()
        {
            EncodingConverterCore ecc = new EncodingConverterCore();
            bool ad = true;
            ecc.AutoDetectInputEncoding = ad;
            Assert.AreEqual(ad, ecc.AutoDetectInputEncoding);

            ad = false;
            ecc.AutoDetectInputEncoding = ad;
            Assert.AreEqual(ad, ecc.AutoDetectInputEncoding);

            ad = true;
            ecc.AutoDetectInputEncoding = ad;
            Assert.AreEqual(ad, ecc.AutoDetectInputEncoding);
        }

        [TestMethod()]
        public void ECCInputTextTest()
        {
            EncodingConverterCore ecc = new EncodingConverterCore();

            string inputFile = "testinputfile";
            Encoding ie = Encoding.ASCII;
            int length = 20;

            string text = ECCTestHelper.CreateFile(inputFile, ie, length);

            ecc.InputEncoding = ie;
            ecc.InputFilePath = inputFile;

            Assert.AreEqual(text, ecc.InputText);
        }
        /// <summary>
        /// In the current implementation, <see cref="EncodingConverterCore.InputText"/> is supposed to throw an exception if it could not read the file
        /// for any reason.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the test couldn't delete the test file before beginning the test.</exception>
        [TestMethod()]
        public void ECCInputTextExceptionTest()
        {
            EncodingConverterCore ecc = new EncodingConverterCore();

            string inputFile = "testinputfile";
            if (File.Exists(inputFile))
            {
                File.Delete(inputFile);
                if (File.Exists(inputFile))
                {
                    throw new InvalidOperationException($"Test could not delete file '{inputFile}'");
                }
            }

            string text;

            ecc.InputEncoding = Encoding.ASCII;
            ecc.InputFilePath = inputFile;

            Assert.ThrowsException<FileNotFoundException>(() => text = ecc.InputText);
        }

        /// <summary>
        /// In the current implementation, <see cref="EncodingConverterCore.InputText"/> is supposed to return null if the
        /// <see cref="EncodingConverterCore.InputEncoding"/> is null.
        /// </summary>
        [TestMethod()]
        public void ECCInputTextNullInputEncodingTest()
        {
            EncodingConverterCore ecc = new EncodingConverterCore();

            string inputFile = "testinputfile";
            Encoding ie = null;
            int length = 20;

            string text = ECCTestHelper.CreateFile(inputFile, Encoding.UTF7, length);


            ecc.InputEncoding = ie;
            ecc.InputFilePath = inputFile;

            Assert.AreEqual(null, ecc.InputText);
        }

        /// <summary>
        /// In the current implementation, <see cref="EncodingConverterCore.InputText"/> is supposed to return null if the
        /// <see cref="EncodingConverterCore.InputFilePath"/> is null, empty, or white space.
        /// </summary>
        [TestMethod()]
        public void ECCInputTextEmptyInputFilePathTest()
        {
            EncodingConverterCore ecc = new EncodingConverterCore();

            string inputFile = "testinputfile";
            Encoding ie = Encoding.UTF8;
            int length = 20;

            string text = ECCTestHelper.CreateFile(inputFile, ie, length);

            ecc.InputEncoding = ie;
            ecc.InputFilePath = inputFile;
            Assert.AreEqual(text, ecc.InputText);

            ecc.InputEncoding = ie;
            ecc.InputFilePath = "";
            Assert.AreEqual(null, ecc.InputText);

            ecc.InputEncoding = ie;
            ecc.InputFilePath = inputFile;
            Assert.AreEqual(text, ecc.InputText);

            ecc.InputEncoding = ie;
            ecc.InputFilePath = "    ";
            Assert.AreEqual(null, ecc.InputText);

            ecc.InputFilePath = inputFile;
            Assert.AreEqual(text, ecc.InputText);

            ecc.InputEncoding = ie;
            ecc.InputFilePath = null;
            Assert.AreEqual(null, ecc.InputText);
        }

    }
}
#endif