/*
Taken from:
http://www.codeproject.com/KB/recipes/DetectEncoding.aspx

This library is created and authored by Carsten Zeumer
Copyright � Carsten Zeumer 2009 

Licensed under A Public Domain dedication
http://creativecommons.org/licenses/publicdomain/
 */
namespace MultiLanguage
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;

    [ComImport, Guid("3DC39D1D-C030-11D0-B81B-00C04FC9B31F"), InterfaceType((short) 1)]
    public interface IEnumRfc1766
    {
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
        void Clone([MarshalAs(UnmanagedType.Interface)] out IEnumRfc1766 ppEnum);
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
        void Next([In] uint celt, out tagRFC1766INFO rgelt, out uint pceltFetched);
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
        void Reset();
        [MethodImpl(MethodImplOptions.InternalCall, MethodCodeType=MethodCodeType.Runtime)]
        void Skip([In] uint celt);
    }
}
