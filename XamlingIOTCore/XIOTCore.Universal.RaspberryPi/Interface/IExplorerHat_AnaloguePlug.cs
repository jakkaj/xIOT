using System.Threading.Tasks;

namespace XIOTCore.Universal.RaspberryPi.Interface
{
    public interface IExplorerHat_AnaloguePlug
    {
        Task<bool> Init();
        Task<double> Measure();
        Task<double> MeasurePercentage();
    }

    public interface IExplorerHat_AnaloguePlug1 : IExplorerHat_AnaloguePlug
    {
        
    }
    public interface IExplorerHat_AnaloguePlug2 : IExplorerHat_AnaloguePlug
    {

    }
    public interface IExplorerHat_AnaloguePlug3 : IExplorerHat_AnaloguePlug
    {

    }
    public interface IExplorerHat_AnaloguePlug4 : IExplorerHat_AnaloguePlug
    {

    }
}