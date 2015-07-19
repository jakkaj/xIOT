using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIOTCore.Contract.Enum
{
    /// <summary>
    /// Some boards have outputs that don't send VCC, they sink ground instead
    /// This allows you to trigger output based on that "reversal"
    /// An example of something that triggers by opening to ground is the 
    /// Explorer HAT pro on the Raspberry Pi 2. 
    /// </summary>
    public enum XOutputMode
    {
        Vcc,
        Gnd
    }
}
