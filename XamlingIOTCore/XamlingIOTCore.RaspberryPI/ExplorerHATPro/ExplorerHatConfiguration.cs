using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;

namespace XCore.RaspberryPI.ExplorerHATPro
{
    public class ExplorerHatConfiguration : IPlatformConfiguration
    {
        public XOutputMode OutputMode => XOutputMode.Gnd;
    }
}
