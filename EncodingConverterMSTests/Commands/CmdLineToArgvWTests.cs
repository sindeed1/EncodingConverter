using Microsoft.VisualStudio.TestTools.UnitTesting;
using EncodingConverter.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace EncodingConverter.Commands.Tests
{
    [TestClass()]
    public class CmdLineToArgvWTests
    {
        [TestMethod()]
        public void SplitArgsManagedTest_BasicFunctionality()
        {
            string line = "a b c d";
            TestSplitArgsManaged(line);
        }
        [TestMethod()]
        public void SplitArgsManagedTest_Test2()
        {
            string line = "a \"b c\" d";
            TestSplitArgsManaged(line);
        }
        [TestMethod()]
        public void SplitArgsManagedTest_Test3()
        {
            string line = "a \"b\" c\" d";
            TestSplitArgsManaged(line);
        }
        [TestMethod()]
        public void SplitArgsManagedTest_Test4()
        {
            string line = "\"a \"b\" c\" d";
            TestSplitArgsManaged(line);
        }
        [TestMethod()]
        public void SplitArgsManagedTest_Test5()
        {
            string line = "a b c d\"";
            TestSplitArgsManaged(line);
        }
        [TestMethod()]
        public void SplitArgsManagedTest_Test6()
        {
            string line = "\"a b c d";
            TestSplitArgsManaged(line);
        }
        [TestMethod()]
        public void SplitArgsManagedTest_Test7()
        {
            string line = "\"a\\ b c d";
            TestSplitArgsManaged(line);
        }
        [TestMethod()]
        public void SplitArgsManagedTest_Test8()
        {
            string line = "\"a b c d\\";
            TestSplitArgsManaged(line);
        }

        void TestSplitArgsManaged(string line)
        {
            var unmanaged = CmdLineToArgvW.SplitArgs(line);
            var managed = CmdLineToArgvW.SplitArgsManaged(line);

            Trace.WriteLine(line);
            TraceArray(unmanaged, nameof(unmanaged));
            TraceArray(managed, nameof(managed));

            Assert.AreEqual(unmanaged.Length, managed.Length);
            for (int i = 0; i < unmanaged.Length; i++)
            {
                Assert.AreEqual(unmanaged[i], managed[i]);
            }
        }
        void TraceArray<T>(T[] items, string text)
        {
            Trace.WriteLine("");
            Trace.Write(text + "={");
            TraceArray(items);
            Trace.Write("}");
        }

        void TraceArray<T>(T[] items)
        {
            if (items == null)
            {
                Trace.Write($"NULL!");
                return;
            }
            Trace.Write($"Count='{ items.Length}'");
            for (int i = 0; i < items.Length; i++)
            {
                Trace.Write(", '" + items[i]+"'");
            }
        }
    }
}