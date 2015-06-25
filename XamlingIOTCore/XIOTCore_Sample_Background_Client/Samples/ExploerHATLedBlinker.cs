using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract;
using XIOTCore.Contract.Interface;
using XIOTCore.Factory;

namespace XIOTCore_Sample_Background_Client.Samples
{
    public class ExploerHATLedBlinker
    {
        private IXIOTCoreFactory _factory =
          XIOTCoreFactory.Create(Platforms.RaspberryPi2ModelB | Platforms.RaspberryPi2ExporerHatPro);

        private IXLed _redLed;
        private IXLed _greenLed;

        public async void DoSomeBlinking()
        {
            var state = true;

            while (true)
            {
                _redLed.State = state;
                _greenLed.State = !state;
                state = !state;
                await Task.Delay(500);
            }
        }
    }
}
