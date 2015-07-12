namespace XIOTCore.Contract.Interface.Basics
{
    public interface IXGpioControl
    {
        void On();
        void Off();
        bool State { get; set; }
    }
}