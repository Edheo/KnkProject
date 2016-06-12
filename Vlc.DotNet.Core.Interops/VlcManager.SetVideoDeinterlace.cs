using System;
using Vlc.DotNet.Core.Interops.Signatures;

namespace Vlc.DotNet.Core.Interops
{
    public sealed partial class VlcManager
    {
        public void SetVideoDeinterlace(VlcMediaPlayerInstance mediaPlayerInstance, string deinterlaceMode)
        {
            if (mediaPlayerInstance == IntPtr.Zero)
                throw new ArgumentException("Media player instance is not initialized.");
            GetInteropDelegate<SetVideoDeinterlace>().Invoke(mediaPlayerInstance, deinterlaceMode.ToHGlobalAnsi());
        }
    }
}
