using System.Threading.Tasks;
using XCore.RaspberryPI.ExplorerHATPro.Enum;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.I2C;

namespace XCore.RaspberryPI.Interface
{
    public interface IExplorerHat_ADS1015 : IADS1015
    {
        Task<double> Measure();

        Task<double> MeasurePercentage();
    }
}