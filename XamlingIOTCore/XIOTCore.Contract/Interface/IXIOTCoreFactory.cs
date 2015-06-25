namespace XIOTCore.Contract.Interface
{
    public interface IXIOTCoreFactory
    {
        T GetComponent<T>();
        void Init();
    }
}