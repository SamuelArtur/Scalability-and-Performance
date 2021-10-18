using System;
using System.Diagnostics;
using static System.Console;
using static System.Diagnostics.Process;

namespace MonitoringLib
{
    public class Recorder
    {
        static Stopwatch timer = new Stopwatch();

        static long bytesPhysicalBefore = 0;

        static long bytesVirtualBefore = 0;

        public static void Start()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            bytesPhysicalBefore = GetCurrentProcess().WorkingSet64;
            bytesVirtualBefore = GetCurrentProcess().VirtualMemorySize64;
            timer.Restart();
        }

        public static void Stop()
        {
            timer.Stop();
            long bytesPhysicalAfter = GetCurrentProcess().WorkingSet64;
            long bytesVirtualAfter = GetCurrentProcess().VirtualMemorySize64;

            WriteLine("{0:N0} bytes de espaço físico usado.", bytesPhysicalAfter - bytesPhysicalBefore);
            WriteLine("{0:N0} bytes de espaço virtual usado.", bytesVirtualAfter - bytesVirtualBefore);

            WriteLine("{0} de tempo se passaram", timer.Elapsed);
            WriteLine("{0:N0} de milissegundos se passaram", timer.ElapsedMilliseconds);
        }
    }
}