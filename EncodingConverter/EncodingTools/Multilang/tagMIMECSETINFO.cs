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
    public struct tagMIMECSETINFO
    {
        public uint uiCodePage;
        public uint uiInternetEncoding;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=50)]
        public ushort[] wszCharset;
    }
}
