using System;
using Microsoft.VisualBasic.Devices;
using System.Diagnostics;

namespace SDRSharp.Rtl_433
{

    public static class WindowBudgetManager
    {
        private static readonly int MaxWindowsTheoretical;
        private static int _currentWindows = 0;

        static WindowBudgetManager()
        {
            MaxWindowsTheoretical = CalculateTheoreticalLimit();
        }

        public static void RegisterWindowOpened()
        {
            _currentWindows++;
        }

        public static void RegisterWindowClosed()
        {
            if (_currentWindows > 0)
                _currentWindows--;
        }

        public static bool CanOpenNewWindow()
        {
            if (_currentWindows >= MaxWindowsTheoretical)
                return false;

            if (IsCommitMemoryHigh())
                return false;

            return true;
        }

        private static int CalculateTheoreticalLimit()
        {
            var computerInfo = new ComputerInfo();
            double ramGB = computerInfo.TotalPhysicalMemory / (1024.0 * 1024.0 * 1024.0);

            double archFactor = Environment.Is64BitProcess ? 0.9 : 1.2;
            double graphicsFactor = GraphicsModeIsHeavy ? 0.8 : 1.0;

            int limit = (int)(15 * ramGB * archFactor * graphicsFactor);

            return Math.Max(limit, 20); // sécurité minimale
        }

        private static bool IsCommitMemoryHigh()
        {
            var committed = GetPerfCounter("Memory", "Committed Bytes");
            var limit = GetPerfCounter("Memory", "Commit Limit");

            if (limit == 0)
                return false;

            double ratio = committed / limit;

            return ratio >= 0.80;
        }

        private static double GetPerfCounter(string category, string counter)
        {
            using var pc = new PerformanceCounter(category, counter);
            return pc.NextValue();
        }

        // À ajuster selon ton application
        public static bool GraphicsModeIsHeavy { get; set; } = true;
    }
}
