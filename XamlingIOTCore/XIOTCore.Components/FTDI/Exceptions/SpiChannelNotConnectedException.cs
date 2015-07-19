using System;
using XIOTCore.Components.FTDI.Types;

namespace XIOTCore.Components.FTDI.Exceptions
{
    public class SpiChannelNotConnectedException : Exception
    {
        public FtResult Reason { get; private set; }

        public SpiChannelNotConnectedException(FtResult res)
        {
            Reason = res;
        }


    }
}
