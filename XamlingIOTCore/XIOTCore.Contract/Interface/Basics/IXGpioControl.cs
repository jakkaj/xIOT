using System.Diagnostics.Contracts;
using XIOTCore.Contract.Enum;

namespace XIOTCore.Contract.Interface.Basics
{
    public interface IXGpioControl
    {
        void On();
        void Off();
        bool State { get; set; }
        void SetDirection(XGpioDirection direction);
    }
}