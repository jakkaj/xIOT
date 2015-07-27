using XIOTCore.Contract.Enum;

namespace XIOTCore.Contract.Interface.GPIO
{
    public interface IXGpio
    {
        void SetDirection(XGpioDirection direction);
        void SetLow();
        void SetHigh();
        XPinValue GetValue();
    }
}