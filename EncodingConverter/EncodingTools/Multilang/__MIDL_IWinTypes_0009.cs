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

    [StructLayout(LayoutKind.Explicit, Pack=4)]
    public struct __MIDL_IWinTypes_0009
    {
        [FieldOffset(0)]
        public int hInproc;
        [FieldOffset(0)]
        public int hRemote;
    }
}
