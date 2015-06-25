using System.Threading.Tasks;
using XCore.RaspberryPI.ExplorerHATPro.Enum;
using XCore.RaspberryPI.Interface;
using XIOTCore.Components.I2C.ADC;
using XIOTCore.Contract.Enum;

namespace XCore.RaspberryPI.ExplorerHATPro.Components
{
    public class ExplorerHat_ADS1015 : ADS1015, IExplorerHat_ADS1015
    {
        public ExplorerHat_ADS1015() : base(0x48, "I2C1")
        {
        }

        public async Task<double> Measure(ExplorerHat_ADS1015_Channel channel, Gain gain = Gain.Volt5,
            SamplesPerSecond samples = SamplesPerSecond.SPS1600)
        {
            return await GetMillivolts((ushort)channel, gain.ForADS1015(), samples.ForADS1015());
        }

        public async Task<double> MeasurePercentage(ExplorerHat_ADS1015_Channel channel, Gain gain = Gain.Volt5,
            SamplesPerSecond samples = SamplesPerSecond.SPS1600)
        {
            var mv = await GetMillivolts((ushort)channel, gain.ForADS1015(), samples.ForADS1015());

            var volts = gain.ToElectricPotenital();

            return (volts.Millivolts - mv) / volts.Millivolts * 100d;
        }
    }
}
