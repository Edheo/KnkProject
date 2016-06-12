using System;
using System.Runtime.InteropServices;

namespace Vlc.DotNet.Core.Interops
{
    internal static class IntPtrExtensions
    {
        public static string ToStringAnsi(this IntPtr ptr)
        {
            return ptr != IntPtr.Zero ? Marshal.PtrToStringAnsi(ptr) : null;
        }
    }
}
