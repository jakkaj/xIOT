using XIOTCore.Contract.Enum;

namespace XIOTCore.Contract.Interface.Basics
{
    public interface IXGpio
    {
        void SetLow();
        void SetHigh();
        XPinValue GetValue();
    }
}