using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed partial class VlcManager
    {
        public string GetLastErrorMessage()
        {
            return GetInteropDelegate<GetLastErrorMessage>().Invoke().ToStringAnsi();
        }
    }
}
