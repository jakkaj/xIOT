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
        private XOutputMode _mode = XOutputMode.Vcc;
        public HC_SR04(IXGpioControl triggerOutput)
        {
            _triggerOutput = triggerOutput;
        }

        public void Init()
        {
            
        }

        /// <summary>
        /// Some boards have outputs that don't send VCC, they sink ground instead
        /// This allows you to trigger output based on that "reversal"
        /// An example of something that triggers by opening to ground is the 
        /// Explorer HAT pro on the Raspberry Pi 2. 
        /// </summary>
        /// <param name="triggerMode"></param>
        public void SetOutputTriggerMode(XOutputMode mode)
        {
            _mode = mode;
        }
    }
}
