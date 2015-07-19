using System;
using XIOTCore.Components.FTDI.Types;

namespace XIOTCore.Components.FTDI.Exceptions
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
