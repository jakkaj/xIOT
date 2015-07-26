namespace XIOTCore.Contract.Enum
{
    public static class LCDConstants
    {
        public const int OUTPUT = 1;
        public const int INPUT = 0;

        public const int HomeClearExec = 2000;
        public const int ClearDisplay = 0x01;
        public const int ReturnHome = 0x02;
        public const int EntryModeSet = 0x04;
        public const int DisplayControl = 0x08;
        public const int CursorShift = 0x10;
        public const int FunctionSet = 0x20;
        public const int SetCGRAMAddr = 0x40;
        public const int SetDDRAMAddr = 0x80;

        public const int EntryRight = 0x00;
        public const int EntryLeft = 0x02;
        public const int ShiftIncrement = 0x01;
        public const int ShiftDecrement = 0x00;

        public const int DisplayOn = 0x04;
        public const int DisplayOff = 0x00;
        public const int CursorOn = 0x02;
        public const int CursorOff = 0x00;
        public const int BlinkOn = 0x01;
        public const int BlinkOff = 0x00;

       public const int DisplayMove = 0x08;
       public const int CursorMove = 0x00;
       public const int MoveRight = 0x04;
        public const int MoveLeft = 0x00;

       public const int LCD_8BITMODE = 0x10;
        public const int LCD_4BITMODE = 0x00;
        public const int LCD_2LINE = 0x08;
        public const int LCD_1LINE = 0x00;
       public const int LCD_5x10DOTS = 0x04;
        public const int LCD_5x8DOTS = 0x00;

       public const int COMMAND = 0;
       public const int DATA = 1;
       public const int FOUR_BITS = 2;

       public const int BackLightOff = 0;
        public const int BackLightOn = 255;
    }


   
    public enum BacklightPolarity
    {
        Positive,
        Negative
    }

}
