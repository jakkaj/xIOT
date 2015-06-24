using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Bluetooth.Advertisement;
using Autofac;
using XCore.RaspberryPI.Modules;
using XIOTCore.Contract;
using XIOTCore.Contract.Interface;

namespace XIOTCore.Factory
{
    public class XIOTCoreFactory : IXIOTCoreFactory
    {
        public static IXIOTCoreFactory Create(Platforms platforms)
        {
            return new XIOTCoreFactory(platforms);
        }

        private IContainer _container;

        public XIOTCoreFactory(Platforms platforms)
        {
            _init(platforms);
        }

        void _init(Platforms platforms)
        {
            var builder = new ContainerBuilder();

            if (platforms.HasFlag(Platforms.RaspberryPi2ModelB))
            {
                builder.RegisterModule<RaspberryPi2ModelBModule>();
            }

            if (platforms.HasFlag(Platforms.RaspberryPi2ExporerHatPro))
            {
                //Register the pi2 anyway, doesn't matter if it's already registered above
                builder.RegisterModule<RaspberryPi2ModelBModule>();
                builder.RegisterModule<ExplorerHatProModule>();
            }

            _container = builder.Build();
        }

        public T GetComponent<T>()
        {
            return _container.Resolve<T>();
        }
    }
}
