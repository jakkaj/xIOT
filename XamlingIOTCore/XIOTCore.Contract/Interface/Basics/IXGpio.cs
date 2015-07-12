using XIOTCore.Contract.Dto;

namespace XIOTCore.Contract.Interface
{
    public interface IXGpio
    {
        void SetLow();
        void SetHigh();
        XPinValue GetValue();
    }
}