using Autofac;

namespace XIOTCore.Contract.Interface
{
    public interface IXIOTCoreFactory
    {
        T GetComponent<T>();
        void Init();
        IContainer Container { get; }
        ContainerBuilder Builder { get; }
    }
}