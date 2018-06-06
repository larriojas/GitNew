using Alemana.Nucleo.Common.Utility.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Alemana.Nucleo.Common.Utility
{
    public static class InstallerHelper
    {
        private static IntPtr HWND_BROADCAST = new IntPtr(0xffff);
        private static IntPtr HWND_TOP = new IntPtr(0);
        private static IntPtr HWND_BOTTOM = new IntPtr(1);
        private static IntPtr HWND_TOPMOST = new IntPtr(-1);
        private static IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        private static IntPtr HWND_MESSAGE = new IntPtr(-3);

        [DllImport("gdi32.dll")]
        static extern int AddFontResource(string lpFilename);

        [DllImport("gdi32.dll")]
        static extern bool RemoveFontResource(string lpFileName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, WindowsMessage wMsg, IntPtr wParam, IntPtr lParam);


        public static void ExecuteInstallerRoutines()
        {
            InstallFonts();
        }


        private static void InstallFonts()
        {
            //Code39HalfInch
            AddFontResource(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Code39HalfInch.ttf"));

            long result = SendMessage(HWND_BROADCAST, WindowsMessage.FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
