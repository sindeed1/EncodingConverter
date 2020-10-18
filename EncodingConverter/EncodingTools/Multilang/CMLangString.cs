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
    using System.Runtime.InteropServices;

    [ComImport, CoClass(typeof(CMLangStringClass)), Guid("C04D65CE-B70D-11D0-B188-00AA0038C969")]
    public interface CMLangString : IMLangString
    {
    }
}
