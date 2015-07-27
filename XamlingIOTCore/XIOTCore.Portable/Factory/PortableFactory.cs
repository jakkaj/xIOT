using Autofac;
using XIOTCore.Contract;
using XIOTCore.Contract.Interface;
using XIOTCore.FTDI.Modules;
using XIOTCore.Portable.Modules;

namespace XIOTCore.Portable.Factory
{
    public class XIOTCoreFactory : IXIOTCoreFactory
    {
        protected readonly Platforms _platforms;

        public static IXIOTCoreFactory Create(Platforms platforms)
        {
            return new XIOTCoreFactory(platforms);
        }

        public XIOTCoreFactory(Platforms platforms)
        {
            _platforms = platforms;
            Builder = new ContainerBuilder();
        }

        public virtual void Init()
        {
            Builder.RegisterModule<PortableModule>();

            if (_platforms.HasFlag(Platforms.FTDI_USB))
            {
                Builder.RegisterModule<FTDI_Module>();
            }

            Container = Builder.Build();
        }

        public T GetComponent<T>()
        {
            return Container.Resolve<T>();
        }


        public IContainer Container { get; private set; }

        public ContainerBuilder Builder { get; }
    }
}
