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

    [ComImport, Guid("275C23E1-3747-11D0-9FEA-00AA003F8646"), CoClass(typeof(CMultiLanguageClass))]
    public interface CMultiLanguage : IMultiLanguage
    {
    }
}
