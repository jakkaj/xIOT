using System;
using XIOTCore.FTDI.Types;

namespace XIOTCore.FTDI.Exceptions
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
