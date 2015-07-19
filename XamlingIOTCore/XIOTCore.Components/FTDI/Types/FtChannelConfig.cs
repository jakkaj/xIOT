using System.Runtime.InteropServices;

namespace XIOTCore.Components.FTDI.Types
{
    [StructLayout(LayoutKind.Sequential)]
    public struct FtChannelConfig
    {
        public int ClockRate;
        public byte LatencyTimer;
        public FtConfigOptions configOptions;
        public int Pin;
        public short reserved;
    }
}
