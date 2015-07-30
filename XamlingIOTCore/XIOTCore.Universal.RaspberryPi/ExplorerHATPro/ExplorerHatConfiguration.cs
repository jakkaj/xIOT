using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface.Configs;

namespace XIOTCore.Universal.RaspberryPi.ExplorerHATPro
{
    public class ExplorerHatConfiguration : IPlatformConfiguration
    {
        public XOutputMode OutputMode => XOutputMode.Gnd;
    }
}
