using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XIOTCore.Contract.Dto;
using XIOTCore.Contract.Enum;
using XIOTCore.Contract.Interface;

namespace XIOTCore.Components.Modules.Range
{
    public class HC_SR04
    {
        private readonly IXGpioControl _triggerOutput;
        
        public HC_SR04(IXGpioControl triggerOutput)
        {
            
            _triggerOutput = triggerOutput;
        }

        public void Init()
        {
            _stopTrigger();
        }

        void _stopTrigger()
        {
            _triggerOutput.Off();
        }

        void _startTrigger()
        {
            _triggerOutput.On();
        }
    }
}
