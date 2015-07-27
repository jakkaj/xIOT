using System.Threading.Tasks;
using XIOTCore.Contract.Enum;

namespace XIOTCore.Contract.Interface.Devices
{
    public interface ILCD_Hitatchi_I2C : ILCD_Hitatchi
    {
        Task<bool> Init(int address, int cols, int rows, int charSize = LCDConstants.LCD_5x8DOTS,
            int en = 6, int rw = 5, 
            int rs = 4, int d4 = 0, 
            int d5 = 1, int d6 = 2, 
            int d7 = 3,
            int? backlightPin = null, BacklightPolarity? polarity = null);
    }
}