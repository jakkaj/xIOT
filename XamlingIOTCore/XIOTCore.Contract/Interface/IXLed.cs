namespace XIOTCore.Contract.Interface
{
    public interface IXLed
    {
        void On();
        void Off();
        bool State { get; set; }
    }
}