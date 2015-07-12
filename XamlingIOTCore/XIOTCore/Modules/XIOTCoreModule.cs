using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using XIOTCore.Components.I2C;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.I2C;

namespace XIOTCore.Modules
{
    public class XIOTCoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<XI2CInfo>().As<IXI2CInfo>();
            base.Load(builder);
        }
    }
}
