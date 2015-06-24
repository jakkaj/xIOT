using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using XCore.RaspberryPI.Modules;

namespace XIOTCore.Factory
{
    public class XIOTCoreFactory
    {
        public void Initialise(Platforms platforms)
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


        }
    }
}
