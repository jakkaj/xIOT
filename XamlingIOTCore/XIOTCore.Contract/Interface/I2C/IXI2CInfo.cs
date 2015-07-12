using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface
{
    public interface IXI2CInfo
    {
        Task GetAllDevices();
    }
}