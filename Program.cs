using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace PhotoFinder
{
    class Program
    {
        [DllImport("Kernel32.dll")]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int cmdShow);
        static void Main(string[] args)
        {


            if (Environment.GetCommandLineArgs().Length <= 1)
            {
                new ContextMenuEditor().EditRegistry();
                Console.ReadKey();
            }
            else
            {
                
                IntPtr hWnd = GetConsoleWindow();
                ShowWindow(hWnd, 0);
                Process.Start(BrowserHandler.GetBrowserFromRegister(), FindImage.GetUriImage(Environment.GetCommandLineArgs()[1]));
            }
        }
    }
}
