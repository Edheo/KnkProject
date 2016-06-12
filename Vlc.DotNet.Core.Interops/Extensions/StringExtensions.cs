using System;
using System.Runtime.InteropServices;

namespace Vlc.DotNet.Core.Interops
{
    internal static class StringExtensions
    {
        public static IntPtr ToHGlobalAnsi(this string source)
        {
            if (source == null)
                return IntPtr.Zero;
            return Marshal.StringToHGlobalAnsi(source);
        }
    }
}
