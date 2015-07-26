using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XIOTCore.Contract.Interface.Basics
{
    public interface ISimpleWriter
    {

        bool Write(byte[] buffer);

        bool Write(byte value);

        bool Write(byte value1, byte value2);
    }
}
