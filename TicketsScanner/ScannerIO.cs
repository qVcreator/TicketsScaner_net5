using RGiesecke.DllExport;
using System;
using System.Runtime.InteropServices;
using TicketsScanner;

namespace TicketsScaner
{
    public class ScannerIO
    {

        static Scanner scanner;

        [DllExport("Initialize", CallingConvention = CallingConvention.Cdecl)]
        public static void Initialize([MarshalAs(UnmanagedType.LPStr)] String comPort)
        {
            scanner = new Scanner(comPort);
        }

        [DllExport("Start", CallingConvention = CallingConvention.Cdecl)]
        public static bool Start()
        {
            if (scanner == null)
            {
                return false;
            }

            return scanner.Start();
        }

        [DllExport("Stop", CallingConvention = CallingConvention.Cdecl)]
        public static void Stop()
        {
            if (scanner != null)
            {
                scanner.Stop();
            }
        }

        [DllExport("GetNewTicket", CallingConvention = CallingConvention.Cdecl)]
        public static string GetNewTicket()
        {
            if (scanner != null)
            {
                return scanner.GetNewTicket();
            }

            return String.Empty;
        }
    }
}
