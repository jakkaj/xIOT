using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Portable.Components.I2C.ADC;
using XIOTCore.Universal.RaspberryPi.ExplorerHATPro.Enum;
using XIOTCore.Universal.RaspberryPi.Interface;

namespace XIOTCore.Universal.RaspberryPi.ExplorerHATPro.Components
{
    public class ExplorerHat_ADS1015 : ADS1015, IExplorerHat_ADS1015
    {
        private readonly ExplorerHat_ADS1015_Channel _channel;
        private readonly XGain _xGain;
        private readonly XSamplesPerSecond _xSamples;

        public ExplorerHat_ADS1015(IXI2CDevice i2cDevice, ExplorerHat_ADS1015_Channel channel, XGain gain = XGain.Volt5,
            XSamplesPerSecond samples = XSamplesPerSecond.SPS1600) : base(i2cDevice, 0x48)
        {
            _channel = channel;
            _xGain = gain;
            _xSamples = samples;
        }
        public async Task<double> Measure()
        {
            return await GetMillivolts((ushort)_channel, _xGain.ForADS1015(), _xSamples.ForADS1015());
        }

        public async Task<double> MeasurePercentage()
        {
            var mv = await GetMillivolts((ushort)_channel, _xGain.ForADS1015(), _xSamples.ForADS1015());

            var volts = _xGain.ToElectricPotenital();

            return (volts.Millivolts - mv) / volts.Millivolts * 100d;
        }
    }
}
