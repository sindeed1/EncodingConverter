using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace EncodingConverter
{
    static internal class EncodingConverterSafeCore
    {
        public static string GetInputTextSafe(this EncodingConverterCore ecc, out Exception ex)
        {
            try
            {
                ex = null;
                return ecc.InputText;
            }
            catch (OutOfMemoryException ex2)
            {
                ex = ex2;

                Trace.TraceWarning($"A '{nameof(OutOfMemoryException)}' was encountered while reading input file '{ecc.InputFilePath}' using" +
                    $" input encoding '{ecc.InputEncoding}'. This is most likely because input file is too large.");
                ex.WriteToTrace();
                Console.WriteLine($"Warning! Reading text from input file '{ecc.InputFilePath}' failed with '{nameof(OutOfMemoryException)}'!");

                return null;
            }
            catch (Exception e)
            {
                ex = e;

                Trace.TraceWarning($"An unspecified exception was encountered while reading input file '{ecc.InputFilePath}' using" +
                    $" input encoding '{ecc.InputEncoding}'.");
                ex.WriteToTrace();
                Console.WriteLine($"Warning! Reading text from input file '{ecc.InputFilePath}' failed with an unspecified exception!");

                return null;
            }
        }
        public static Exception InputFilePathSafeSet(this EncodingConverterCore ecc, string inputFilePath)
        {
            try
            {
                ecc.InputFilePath = inputFilePath;
            }
            catch (FileNotFoundException ex)
            {
                Trace.TraceWarning($"A '{nameof(FileNotFoundException)}' was encountered while setting a new value " +
                    $"{nameof(EncodingConverterCore)}.{nameof(EncodingConverterCore.InputFilePath)}='{inputFilePath}'.");
                ex.WriteToTrace();
                Console.WriteLine($"Warning! File '{inputFilePath}' does not exist!");

                return ex;
            }
            catch (Exception ex)
            {
                Trace.TraceWarning($"An unspecified exception was encountered while setting a new value " +
                    $"{nameof(EncodingConverterCore)}.{nameof(EncodingConverterCore.InputFilePath)}='{inputFilePath}'.");
                ex.WriteToTrace();
                Console.WriteLine($"Warning! An error was encountered while setting the new input file '{inputFilePath}'!");
                return ex;
            }

            return null;
        }

        public static Exception DetectInputEncodingSafe(this EncodingConverterCore ecc)
        {
            try
            {
                ecc.DetectInputEncoding();
            }
            catch (FileNotFoundException ex)
            {
                Trace.TraceWarning($"A '{nameof(FileNotFoundException)}' was encountered while detecting input encoding of " +
                    $"{nameof(EncodingConverterCore)}.{nameof(EncodingConverterCore.InputFilePath)}='{ecc.InputFilePath}'.");
                ex.WriteToTrace();
                Console.WriteLine($"Warning! File '{ecc.InputFilePath}' does not exist!");

                return ex;
            }
            catch (Exception ex)
            {
                Trace.TraceWarning($"An unspecified exception was encountered while detecting input encoding of " +
                    $"{nameof(EncodingConverterCore)}.{nameof(EncodingConverterCore.InputFilePath)}='{ecc.InputFilePath}'.");
                ex.WriteToTrace();
                Console.WriteLine($"Warning! An error was encountered while detecting input encoding!");
                return ex;
            }
            return null;
        }

        public static Exception ConvertSafe(this EncodingConverterCore ecc)
        {
            try
            {
                ecc.Convert();
            }
            catch (System.Security.SecurityException ex)
            {
                Trace.TraceWarning($"A '{nameof(System.Security.SecurityException)}' was encountered while converting encoding of file '{ecc.InputFilePath}'" +
                    $" to file '{ecc.OutputFilePath}'. This is most likely because you don't have the permissions needed to read or write to either of the" +
                    $"above mentioned files.");
                ex.WriteToTrace();
                Console.WriteLine($"Warning! A '{nameof(System.Security.SecurityException)}' was encountered while converting the encoding!");
                return ex;
            }
            catch (Exception ex)
            {
                Trace.TraceWarning($"An unspecified exception was encountered while converting encoding of file '{ecc.InputFilePath}'" +
                    $" to file '{ecc.OutputFilePath}'.");
                ex.WriteToTrace();
                Console.WriteLine($"Warning! An error was encountered while converting the encoding!");
                return ex;
            }

            return null;
        }
    }
}
