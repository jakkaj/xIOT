using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using XIOTCore.Contract.Interface.Devices;
using XIOTCore.Portable.Components.OLED.SSD1306;

namespace XIOTCore.Portable.Modules
{
    public class PortableModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OLDED_SSD1306_I2C>().As<IOLED_SSD1306_I2C>();

            base.Load(builder);
        }
    }
}
