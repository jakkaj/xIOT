﻿using System;

namespace XIOTCore.FTDI.Types
{
    [Flags]
    public enum FtSpiTransferOptions : int
    {
        SizeInBytes = 0x00000000,
        SizeInBits = 0x00000001,


        ToogleChipSelect = 0x6,
        ChipselectEnable = 0x00000002,
        ChipselectDisable = 0x00000004,
    }
}
