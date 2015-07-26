using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIOTCore.FTDI.I2C
{
    public enum I2CModes
    {
        I2C_CLOCK_STANDARD_MODE = 100000,
        I2C_CLOCK_FAST_MODE = 400000,
        I2C_CLOCK_FAST_MODE_PLUS = 1000000,
        I2C_CLOCK_HIGH_SPEED_MODE = 3400000
    }
}
