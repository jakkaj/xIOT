using System;
using System.Threading.Tasks;
using XIOTCore_Sample_Background_Client.Samples;

namespace XIOTCore_Sample_Background_Client
{
    public class CoreIOTRunner
    {
        public async Task Run()
        {
            var explorerHatBlinker = new ExploerHATLedBlinker();
            explorerHatBlinker.DoSomeBlinking();

            await Task.Delay(TimeSpan.MaxValue);
        }
    }
}
