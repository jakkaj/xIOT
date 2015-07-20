using System;

namespace XIOTCore.Contract
{
    [Flags]
    public enum Platforms
    {
        RaspberryPi2ModelB = 1,
        RaspberryPi2ExporerHatPro = 2,
        FTDI_USB = 4
    }
}
