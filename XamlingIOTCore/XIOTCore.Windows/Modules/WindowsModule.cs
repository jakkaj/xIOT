using Autofac;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Windows.I2C;

namespace XIOTCore.Windows.Modules
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
