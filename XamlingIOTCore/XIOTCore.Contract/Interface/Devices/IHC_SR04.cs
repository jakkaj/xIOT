using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface.Devices
{
    public interface IHC_SR04
    {
        Task Init();
        Task<decimal> Measure(bool averageOutResult = false);
    }
}