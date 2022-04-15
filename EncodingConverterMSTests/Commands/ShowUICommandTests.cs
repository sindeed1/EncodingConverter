using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncodingConverter.UnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace EncodingConverterMSTests.Commands
{
    [TestClass()]
    public class ShowUICommandTests
    {
        /// <summary>
        /// Tests if showui command shows the default main form 4.
        /// </summary>
        [TestMethod("Args: NULL")]
        public void ShowUICommandTestTest()
        {
            string[] args = new string[] { ShowUICommandTest.SwitchCmdName };

            ShowUICommandTest cmd = InvokeNewShowUICommand(args);
            AssertMainFormIsOpen("mainform4");

            Application.Exit();
        }

        [TestMethod("Args: inputFile")]
        public void ShowUICommandTestSwitches()
        {
            string inputFilePathArg = "testinputfile";
            string[] args = new string[] { ShowUICommandTest.SwitchCmdName
                                        , inputFilePathArg };

            ShowUICommandTest cmd = InvokeNewShowUICommand(args);
            AssertMainFormIsOpen("mainform4");

            cmd.ECC.AssertECCInputFile(inputFilePathArg);

            Application.Exit();
        }

        [TestMethod("Args: inputFile, oe:outputEncoding")]
        public void ShowUICommandTestSwitchesOutputEncoding()
        {
            string inputFilePathArg = "testinputfile";
            string encodingArg = "utf-8";
            string[] args = new string[] { ShowUICommandTest.SwitchCmdName
                                    , inputFilePathArg
                                    , ShowUICommandTest.SwitchOutputEncoding + ShowUICommandTest.DataSeparationChar + encodingArg };

            ShowUICommandTest cmd = InvokeNewShowUICommand(args);
            AssertMainFormIsOpen("mainform4");

            cmd.ECC.AssertECCInputFile(inputFilePathArg);
            cmd.ECC.AssertECCOutputEncoding(encodingArg);

            Application.Exit();
        }
        [TestMethod("Args: inputFile, oe, outputEncoding")]
        public void ShowUICommandTestSwitchesOutputEncoding2()
        {
            string inputFilePathArg = "testinputfile";
            string encodingArg = "utf-8";
            string[] args = new string[] { ShowUICommandTest.SwitchCmdName
                                    , inputFilePathArg
                                    , ShowUICommandTest.SwitchOutputEncoding
                                    , encodingArg };

            ShowUICommandTest cmd = InvokeNewShowUICommand(args);
            AssertMainFormIsOpen("mainform4");

            cmd.ECC.AssertECCInputFile(inputFilePathArg);
            cmd.ECC.AssertECCOutputEncoding(encodingArg);

            Application.Exit();
        }

        [TestMethod("Args: inputFile, ie:inputEncoding")]
        public void ShowUICommandTestSwitchesInputEncoding()
        {
            string inputFilePathArg = "testinputfile";
            string encodingArg = "utf-8";
            string[] args = new string[] { ShowUICommandTest.SwitchCmdName
                                        , inputFilePathArg
                                        , ShowUICommandTest.SwitchInputEncoding + ShowUICommandTest.DataSeparationChar + encodingArg };

            ShowUICommandTest cmd = InvokeNewShowUICommand(args);
            AssertMainFormIsOpen("mainform4");

            cmd.ECC.AssertECCInputFile(inputFilePathArg);
            cmd.ECC.AssertECCInputEncoding(encodingArg);

            Application.Exit();
        }

        [TestMethod("Args: inputFile, outputFile")]
        public void ShowUICommandTestSwitchesOutputpath()
        {
            string inputFilePathArg = "testinputfile";
            string outputFilePathArg = "testoutputfile";
            string[] args = new string[] { ShowUICommandTest.SwitchCmdName
                                        , inputFilePathArg
                                        //, ShowUICommandTest.SwitchInputEncoding + ":utf8" 
                                        , outputFilePathArg};

            ShowUICommandTest cmd = InvokeNewShowUICommand(args);
            AssertMainFormIsOpen("mainform4");

            cmd.ECC.AssertECCInputFile(inputFilePathArg);
            cmd.ECC.AssertECCOutputFile(outputFilePathArg);

            //Trace.WriteLine("");
            //Trace.Write($"ECC.InputEncoding = 'utf8' ?");
            //Assert.IsTrue(cmd.ECC.InputEncoding != null
            //        && (cmd.ECC.InputEncoding.BodyName.ToLower().Contains("utf8")
            //            || cmd.ECC.InputEncoding.EncodingName.ToLower().Contains("utf8")));
            //Trace.Write("[OK]");

            //Trace.WriteLine("");
            //Trace.Write($"ECC.OutputFilePath.Contains('{outputFilePathArg}') ?");
            //Assert.IsTrue(!string.IsNullOrWhiteSpace(cmd.ECC.OutputFilePath)
            //        && cmd.ECC.OutputFilePath.ToLower().Contains(outputFilePathArg));
            //Trace.Write("[OK]");


            Application.Exit();
        }
        [TestMethod("Args: inputFile, ie, inputEncoding, outputFile")]
        public void ShowUICommandTestSwitchesOutputpath2()
        {
            string inputFilePathArg = "testinputfile";
            string outputFilePathArg = "testoutputfile";
            string encodingArg = "utf-8";
            string[] args = new string[] { ShowUICommandTest.SwitchCmdName
                                        , inputFilePathArg
                                        , ShowUICommandTest.SwitchInputEncoding 
                                        , encodingArg 
                                        , outputFilePathArg};

            ShowUICommandTest cmd = InvokeNewShowUICommand(args);
            AssertMainFormIsOpen("mainform4");

            cmd.ECC.AssertECCInputFile(inputFilePathArg);
            cmd.ECC.AssertECCOutputFile(outputFilePathArg);

            //Trace.WriteLine("");
            //Trace.Write($"ECC.InputEncoding = 'utf8' ?");
            //Assert.IsTrue(cmd.ECC.InputEncoding != null
            //        && (cmd.ECC.InputEncoding.BodyName.ToLower().Contains("utf8")
            //            || cmd.ECC.InputEncoding.EncodingName.ToLower().Contains("utf8")));
            //Trace.Write("[OK]");

            //Trace.WriteLine("");
            //Trace.Write($"ECC.OutputFilePath.Contains('{outputFilePathArg}') ?");
            //Assert.IsTrue(!string.IsNullOrWhiteSpace(cmd.ECC.OutputFilePath)
            //        && cmd.ECC.OutputFilePath.ToLower().Contains(outputFilePathArg));
            //Trace.Write("[OK]");


            Application.Exit();
        }


        static ShowUICommandTest InvokeNewShowUICommand(string[] args)
        {
            ShowUICommandTest cmd = new ShowUICommandTest();

            Thread th = new Thread(() => cmd.Execute(args, 0));
            th.SetApartmentState(ApartmentState.STA);

            th.Start();

            Thread.Sleep(1000);

            return cmd;
        }

        static void AssertMainFormIsOpen(string formName)
        {
            var forms = Application.OpenForms;
            Trace.WriteLine("");
            Trace.Write("Count of open forms = 1 ?");
            Assert.AreEqual(1, forms.Count);
            Trace.Write("[OK]");

            Trace.WriteLine("");
            Trace.Write($"Open form = '{formName}' ?");
            var form = forms[0];
            Assert.IsTrue(string.Compare(form.Name.Trim(), formName, StringComparison.OrdinalIgnoreCase) == 0, $"Failure! Name of form[0]={form.Name}");
            Trace.Write("[OK]");
        }
    }
}