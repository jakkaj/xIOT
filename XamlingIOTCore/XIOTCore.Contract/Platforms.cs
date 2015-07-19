using System;

namespace XIOTCore.Contract
{
    [Flags]
    public enum Platforms
    {
        RaspberryPi2ModelB = 0,
        RaspberryPi2ExporerHatPro = 1,
        FTDI_USB = 2
    }
}
