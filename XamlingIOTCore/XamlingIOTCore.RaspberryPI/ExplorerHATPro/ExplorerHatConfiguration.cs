using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;
using XIOTCore.Contract.Interface.Configs;

namespace XCore.RaspberryPI.ExplorerHATPro
{
    public class ExplorerHatConfiguration : IPlatformConfiguration
    {
        public XOutputMode OutputMode => XOutputMode.Gnd;
    }
}
