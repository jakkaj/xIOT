using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface.I2C
{
    public interface IADS1015
    {
        Task<bool> Init();
    }
}