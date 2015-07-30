using Autofac;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Universal.Windows.I2C;

namespace XIOTCore.Universal.Windows.Modules
{
    public class WindowsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WindowsI2CDevice>().As<IXI2CDevice>();
            base.Load(builder);
        }
    }
}
