using Autofac;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.FTDI.I2C;

namespace XIOTCore.FTDI.Modules
{
    public class FTDI_Module : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FTDII2CDevice>().As<IXI2CDevice>();
            base.Load(builder);
        }
    }
}
