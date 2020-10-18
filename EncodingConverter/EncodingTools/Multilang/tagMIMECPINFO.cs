/*
Taken from:
http://www.codeproject.com/KB/recipes/DetectEncoding.aspx

This library is created and authored by Carsten Zeumer
Copyright © Carsten Zeumer 2009 

Licensed under A Public Domain dedication
http://creativecommons.org/licenses/publicdomain/
 */
namespace MultiLanguage
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [StructLayout(LayoutKind.Sequential, Pack=4)]
    public struct tagMIMECPINFO
    {
        public uint dwFlags;
        public uint uiCodePage;
        public uint uiFamilyCodePage;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x40)]
        public ushort[] wszDescription;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=50)]
        public ushort[] wszWebCharset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=50)]
        public ushort[] wszHeaderCharset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=50)]
        public ushort[] wszBodyCharset;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x20)]
        public ushort[] wszFixedWidthFont;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=0x20)]
        public ushort[] wszProportionalFont;
        public byte bGDICharset;
    }
}
