using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using XIOTCore_Sample_Background_Client;

// The Background Application template is documented at http://go.microsoft.com/fwlink/?LinkID=533884&clcid=0x409

namespace XIOTCore_Sample_Background
{
    public sealed class StartupTask : IBackgroundTask
    {
        readonly CoreIOTRunner _runner = new CoreIOTRunner();

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            Task.Run(() => _runner.Run()).Wait();
        }
    }
}
