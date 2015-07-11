using System.Threading.Tasks;
using XCore.RaspberryPI.ExplorerHATPro.Enum;
using XCore.RaspberryPI.Interface;
using XIOTCore.Components.I2C.ADC;
using XIOTCore.Contract.Enum;

namespace XCore.RaspberryPI.ExplorerHATPro.Components
{
    public class ExplorerHat_ADS1015 : ADS1015, IExplorerHat_ADS1015
    {
        private readonly ExplorerHat_ADS1015_Channel _channel;
        private readonly XGain _xGain;
        private readonly XSamplesPerSecond _xSamples;

        public ExplorerHat_ADS1015(ExplorerHat_ADS1015_Channel channel, XGain xGain = XGain.Volt5,
            XSamplesPerSecond xSamples = XSamplesPerSecond.SPS1600) : base(0x48, "I2C1")
        {
            _channel = channel;
            _xGain = xGain;
            _xSamples = xSamples;
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
