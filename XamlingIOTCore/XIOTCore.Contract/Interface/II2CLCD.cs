using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface
{
    public interface II2CLCD : ILCD
    {
        Task<bool> Init();
    }
}