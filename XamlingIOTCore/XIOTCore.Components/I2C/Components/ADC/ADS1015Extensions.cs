using System;
using UnitsNet;
using UnitsNet.Units;
using XIOTCore.Contract.Enum;

namespace XIOTCore.Components.I2C.Components.ADC
{
    public static class ADS1015Extensions
    {
        public static ushort ForADS1015(this SamplesPerSecond samples)
        {
            ushort[] samplePerSecondMap = { 0x0000, 0x0020, 0x0040, 0x0060, 0x0080, 0x00A0, 0x00C0 };
            return samplePerSecondMap[(int)samples];
        }

        public static ushort ForADS1015(this Gain gain)
        {
            ushort[] programmableGainMap = { 0x0000, 0x0200, 0x0400, 0x0600, 0x0800, 0x0A00 };
            return programmableGainMap[(int) gain];
        }

        public static ElectricPotential ToElectricPotenital(this Gain gain)
        {
            switch (gain)
            {
                case Gain.Volt5:
                    return ElectricPotential.From(5, ElectricPotentialUnit.Volt);
                case Gain.Volt33:
                    return ElectricPotential.From(3.3, ElectricPotentialUnit.Volt);
                default:
                    throw new NotImplementedException($"No ElectricPotential mapping for {gain}");
            }
        }

        public static ushort ForADS1015Scale(this ushort gain)
        {
            ushort[] programmableGain_Scaler = { 6144, 4096, 2048, 1024, 512, 256 };
            return programmableGain_Scaler[(int)gain];
        }

        
    }
}
