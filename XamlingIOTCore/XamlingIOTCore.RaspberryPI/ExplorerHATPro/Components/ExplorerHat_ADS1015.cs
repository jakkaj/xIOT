using System.Threading.Tasks;
using XCore.RaspberryPI.ExplorerHATPro.Enum;
using XCore.RaspberryPI.Interface;
using XIOTCore.Components.I2C.ADC;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;

namespace XCore.RaspberryPI.ExplorerHATPro.Components
{
    public class ExplorerHat_ADS1015 : ADS1015, IExplorerHat_ADS1015
    {
        private readonly ExplorerHat_ADS1015_Channel _channel;
        private readonly Gain _gain;
        private readonly SamplesPerSecond _samples;

        public ExplorerHat_ADS1015(IXI2CDevice i2cDevice, ExplorerHat_ADS1015_Channel channel, Gain gain = Gain.Volt5,
            SamplesPerSecond samples = SamplesPerSecond.SPS1600) : base(i2cDevice, 0x48, "I2C1")
        {
            _channel = channel;
            _gain = gain;
            _samples = samples;
        }
        public async Task<double> Measure()
        {
            return await GetMillivolts((ushort)_channel, _gain.ForADS1015(), _samples.ForADS1015());
        }

        public async Task<double> MeasurePercentage()
        {
            var mv = await GetMillivolts((ushort)_channel, _gain.ForADS1015(), _samples.ForADS1015());

            var volts = _gain.ToElectricPotenital();

            return (volts.Millivolts - mv) / volts.Millivolts * 100d;
        }
    }
}
