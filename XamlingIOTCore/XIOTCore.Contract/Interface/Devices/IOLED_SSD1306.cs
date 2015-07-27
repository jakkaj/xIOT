using System.Threading.Tasks;
using XIOTCore.Contract.Enum;

namespace XIOTCore.Contract.Interface.Devices
{
    public interface IOLED_SSD1306
    {
        void Init(OLEDDisplaySize displaySize);
        void DrawPixel(int x, int y, int color);
        void Display();
        void Command(byte c);
        void Data(byte c);
    }

    public interface IOLED_SSD1306_I2C : IOLED_SSD1306
    {
        Task<IOLED_SSD1306_I2C> Init(int address, OLEDDisplaySize displaySize);
    }
}