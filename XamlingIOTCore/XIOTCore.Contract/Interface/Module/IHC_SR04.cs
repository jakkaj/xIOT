using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface.Module
{
    public interface IHC_SR04
    {
        Task Init();
        Task<decimal> Measure(bool averageOutResult = false);
    }
}