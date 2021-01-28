using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ShortCuts.Desktop
{
    public class WindowHelper
    {
        #region constants
        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;
        #endregion

        #region external methods
        [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd, out Rectangle rect);

        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        #endregion

        private static WindowModel makeModel(Process process, Rectangle rect)
        {
            int x = -333;
            int y = -333;
            int width = -333;
            int height = -333;

            try
            {
                x = rect.X;
                y = rect.Y;
                width = rect.Width - rect.X;
                height = rect.Height - rect.Y;
            }
            catch
            {

            }

            WindowModel model = new WindowModel()
            {
                Id = process.Id,
                MainWindowTitle = process.MainWindowTitle,
                MainWindowHandle = process.MainWindowHandle,
                ProcessName = process.ProcessName,
                WindowRect = rect
            };

            return model;
        }

        public static List<WindowModel> ReadWindows()
        {
            List<WindowModel> windows = new List<WindowModel>();

            Process[] processlist = Process.GetProcesses();

            foreach (Process theprocess in processlist)
            {
                if(theprocess.MainWindowHandle != IntPtr.Zero)
                {
                    Rectangle rect = Rectangle.Empty;
                    GetWindowRect(theprocess.MainWindowHandle, out rect);

                    windows.Add(makeModel(theprocess, rect));
                }
            }

            windows.Sort();
            
            return windows;
        }
    
        public static WindowModel ResetWindow(IntPtr windowHandler, int id)
        {
            SetWindowPos(windowHandler, IntPtr.Zero, 0, 0, 0, 0, SWP_NOSIZE | SWP_NOZORDER);

            Process theprocess = Process.GetProcessById(id);
            Rectangle rect = Rectangle.Empty;
            GetWindowRect(theprocess.MainWindowHandle, out rect);
            
            return makeModel(theprocess, rect);
        }
    }
}
