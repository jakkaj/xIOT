using System;
using XIOTCore.FTDI.Types;

namespace XIOTCore.FTDI.Exceptions
{
    public class I2CChannelNotConnectedException : Exception
    {
        public FtResult Reason { get; private set; }

        public I2CChannelNotConnectedException(FtResult res)
        {
            Reason = res;
        }


    }
}
