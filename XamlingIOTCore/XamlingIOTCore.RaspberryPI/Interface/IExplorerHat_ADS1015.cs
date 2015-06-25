using System.Threading.Tasks;
using XCore.RaspberryPI.ExplorerHATPro.Enum;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;

namespace XCore.RaspberryPI.Interface
{
    public interface IExplorerHat_ADS1015 : IADS1015
    {
        Task<double> Measure(ExplorerHat_ADS1015_Channel channel, Gain gain = Gain.Volt5,
            SamplesPerSecond samples = SamplesPerSecond.SPS1600);

        Task<double> MeasurePercentage(ExplorerHat_ADS1015_Channel channel, Gain gain = Gain.Volt5,
            SamplesPerSecond samples = SamplesPerSecond.SPS1600);
    }
}