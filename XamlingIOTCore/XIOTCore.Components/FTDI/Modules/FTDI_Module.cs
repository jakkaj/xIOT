using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using XIOTCore.Components.FTDI.I2C;
using XIOTCore.Contract.Interface;

namespace XIOTCore.Components.FTDI.Modules
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
