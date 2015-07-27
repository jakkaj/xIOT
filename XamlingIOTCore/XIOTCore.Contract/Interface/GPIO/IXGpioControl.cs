using XIOTCore.Contract.Enum;

namespace XIOTCore.Contract.Interface.GPIO
{
    public interface IXGpioControl
    {
        void On();
        void Off();
        bool State { get; set; }
        void SetDirection(XGpioDirection direction);
    }
}