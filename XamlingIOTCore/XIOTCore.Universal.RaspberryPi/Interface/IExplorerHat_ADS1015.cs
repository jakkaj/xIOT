using System.Threading.Tasks;
using XIOTCore.Contract.Interface.I2C;

namespace XIOTCore.Universal.RaspberryPi.Interface
{
    public interface IExplorerHat_ADS1015 : IADS1015
    {
        Task<double> Measure();

        Task<double> MeasurePercentage();
    }
}