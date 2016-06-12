using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed partial class VlcManager
    {
        public string GetEventTypeName(EventTypes eventType)
        {
            return GetInteropDelegate<GetEventTypeName>().Invoke(eventType).ToStringAnsi();
        }
    }
}
