using Autofac;
using XIOTCore.Components.I2C;
using XIOTCore.Components.I2C.Windows;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Basics;

namespace XCore.RaspberryPI.Modules
{
    public class RaspberryPi_2_ModelB_Module : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WindowsI2CDevice>().As<IXI2CDevice>();
            base.Load(builder);
        }
    }
}
