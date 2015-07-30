using Autofac;
using XIOTCore.Contract;
using XIOTCore.Contract.Interface;
using XIOTCore.Portable.Factory;
using XIOTCore.Universal.RaspberryPi.Modules;
using XIOTCore.Universal.Windows.Modules;

namespace XIOTCore.Universal.Factory
{
    public class XIOTCoreWindowsFactory : XIOTCoreFactory
    {

        public static new IXIOTCoreFactory Create(Platforms platforms)
        {
            return new XIOTCoreWindowsFactory(platforms);
        }

        public XIOTCoreWindowsFactory(Platforms platforms)
            :base(platforms)
        {
            
        }

        public override void Init()
        {
            Builder.RegisterModule<WindowsModule>();

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

            base.Init();
        }
    }
}
