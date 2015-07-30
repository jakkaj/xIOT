using System.Threading.Tasks;
using XIOTCore.Contract.Interface.Basics;
using XIOTCore.Universal.RaspberryPi.ExplorerHATPro.Components;
using XIOTCore.Universal.RaspberryPi.ExplorerHATPro.Enum;
using XIOTCore.Universal.RaspberryPi.Interface;

namespace XIOTCore.Universal.RaspberryPi.ExplorerHATPro.Plugs
{
    public class ExplorerHat_AnaloguePlug : IExplorerHat_AnaloguePlug
    {
        private IExplorerHat_ADS1015 _ads;
        protected ExplorerHat_AnaloguePlug(IXI2CDevice i2CDevice, ExplorerHat_ADS1015_Channel channel)
        {
            _ads = new ExplorerHat_ADS1015(i2CDevice, channel);
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
        public ExplorerHat_AnaloguePlug1(IXI2CDevice i2CDevice) : base(i2CDevice, ExplorerHat_ADS1015_Channel.A1)
        {
        }
    }
    public class ExplorerHat_AnaloguePlug2 : ExplorerHat_AnaloguePlug, IExplorerHat_AnaloguePlug2
    {
        public ExplorerHat_AnaloguePlug2(IXI2CDevice i2CDevice) : base(i2CDevice, ExplorerHat_ADS1015_Channel.A2)
        {
        }
    }
    public class ExplorerHat_AnaloguePlug3 : ExplorerHat_AnaloguePlug, IExplorerHat_AnaloguePlug3
    {
        public ExplorerHat_AnaloguePlug3(IXI2CDevice i2CDevice) : base(i2CDevice, ExplorerHat_ADS1015_Channel.A3)
        {
        }
    }
    public class ExplorerHat_AnaloguePlug4 : ExplorerHat_AnaloguePlug, IExplorerHat_AnaloguePlug4
    {
        public ExplorerHat_AnaloguePlug4(IXI2CDevice i2CDevice) : base(i2CDevice, ExplorerHat_ADS1015_Channel.A4)
        {
        }
    }
}
