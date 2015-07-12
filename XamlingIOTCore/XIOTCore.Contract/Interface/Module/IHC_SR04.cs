using System.Threading.Tasks;

namespace XIOTCore.Components.Modules.Range
{
    public interface IHC_SR04
    {
        Task Init();
        Task<decimal> Measure(bool averageOutResult = false);
    }
}