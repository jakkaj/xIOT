using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCore.RaspberryPI.ExplorerHATPro.Components;
using XCore.RaspberryPI.ExplorerHATPro.Enum;
using XCore.RaspberryPI.Interface;
using XIOTCore.Contract.Enum;

namespace XCore.RaspberryPI.ExplorerHATPro.Plugs
{
    public class ExplorerHat_AnaloguePlug : IExplorerHat_AnaloguePlug
    {
        private IExplorerHat_ADS1015 _ads;
        protected ExplorerHat_AnaloguePlug(ExplorerHat_ADS1015_Channel channel)
        {
            _ads = new ExplorerHat_ADS1015(channel);
        }

        public async Task<bool> Init()
        {
            return await _ads.Init();
        }

        public async Task<double> Measure()
        {
            return await _ads.Measure();
        }

        public async Task<double> MeasurePercentage()
        {
            return await _ads.MeasurePercentage();
        }
    }

    public class ExplorerHat_AnaloguePlug1 : ExplorerHat_AnaloguePlug, IExplorerHat_AnaloguePlug1
    {
        public ExplorerHat_AnaloguePlug1() : base(ExplorerHat_ADS1015_Channel.A1)
        {
        }
    }
    public class ExplorerHat_AnaloguePlug2 : ExplorerHat_AnaloguePlug, IExplorerHat_AnaloguePlug2
    {
        public ExplorerHat_AnaloguePlug2() : base(ExplorerHat_ADS1015_Channel.A2)
        {
        }
    }
    public class ExplorerHat_AnaloguePlug3 : ExplorerHat_AnaloguePlug, IExplorerHat_AnaloguePlug3
    {
        public ExplorerHat_AnaloguePlug3() : base(ExplorerHat_ADS1015_Channel.A3)
        {
        }
    }
    public class ExplorerHat_AnaloguePlug4 : ExplorerHat_AnaloguePlug, IExplorerHat_AnaloguePlug4
    {
        public ExplorerHat_AnaloguePlug4() : base(ExplorerHat_ADS1015_Channel.A4)
        {
        }
    }
}
