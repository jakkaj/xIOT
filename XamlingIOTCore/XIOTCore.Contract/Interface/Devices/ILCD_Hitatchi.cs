using System.Threading.Tasks;
using XIOTCore.Contract.Enum;

namespace XIOTCore.Contract.Interface.Devices
{
    public interface ILCD_Hitatchi
    {
        Task Begin(int cols, int rows, int charSize = LCDConstants.LCD_5x8DOTS);
        void Clear();
        void Home();
        void Display();
        void NoDisplay();
        void Blink();
        void NoBlink();
        void NoCursor();
        void Cursor();
        void ScollDisplayLeft();
        void ScrollDisplayRight();
        void LeftToRight();
        void RightToLeft();
        void MoveCursorLeft();
        void MoveCursorRight();
        void AutoScroll();
        void NoAutoScroll();
        void CreateChar(int location, int[] charMap);
        void SetCursor(int col, int row);
        void BackLight();
        void NoBacklight();
        void On();
        void Off();
        void SetBacklightPin(int value, BacklightPolarity pol);
        void SetBacklight(int value);
        int Write(string value);
        int Write(int value);
    }
}