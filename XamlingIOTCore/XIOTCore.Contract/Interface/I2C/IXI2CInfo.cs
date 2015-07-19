using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface.I2C
{
    public interface IXI2CInfo
    {
        Task GetAllDevices();
    }
}