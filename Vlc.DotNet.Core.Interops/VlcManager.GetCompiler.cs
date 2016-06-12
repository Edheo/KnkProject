using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed partial class VlcManager
    {
        public string GetCompiler()
        {
            return GetInteropDelegate<GetCompiler>().Invoke().ToStringAnsi();
        }
    }
}
