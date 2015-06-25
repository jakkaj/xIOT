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
        private readonly Platforms _platforms;


        public static IXIOTCoreFactory Create(Platforms platforms)
        {
            return new XIOTCoreFactory(platforms);
        }

        public XIOTCoreFactory(Platforms platforms)
        {
            _platforms = platforms;
            Builder = new ContainerBuilder();
        }

        public void Init()
        {
            if (_platforms.HasFlag(Platforms.RaspberryPi2ModelB))
            {
                Builder.RegisterModule<RaspberryPi_2_ModelB_Module>();
            }

            if (_platforms.HasFlag(Platforms.RaspberryPi2ExporerHatPro))
            {
                //Register the pi2 anyway, doesn't matter if it's already registered above
                Builder.RegisterModule<RaspberryPi_2_ModelB_Module>();
                Builder.RegisterModule<ExplorerHat_Pro_Module>();
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
