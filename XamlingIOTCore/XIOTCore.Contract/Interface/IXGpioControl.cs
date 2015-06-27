namespace XIOTCore.Contract.Interface
{
    public interface IXGpioControl
    {
        void On();
        void Off();
        bool State { get; set; }
    }
}