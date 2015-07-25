using System.Runtime.InteropServices;
using System.Threading;

namespace XIOTCore.FTDI.LibMPSSE
{
    public class LibMpsse
    {
        private static int _initializations = 0;

        public const string DllName = "libMPSSE.dll";

        public static void Init()
        {
            if(Interlocked.Increment(ref _initializations) == 1)
                Init_libMPSSE();

        }

        public static void Cleanup()
        {
            if(Interlocked.Decrement(ref _initializations) == 0)
                Cleanup_libMPSSE();
        }

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private extern static void Init_libMPSSE();

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        private extern static void Cleanup_libMPSSE();
    }
}
