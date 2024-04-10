using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
    public static partial class Extensions
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

        private const int WM_SETREDRAW = 0x000B;

        public static void SuspendDrawing(this Control control, Action action)
        {
            try
            {
                SendMessage(control.Handle, WM_SETREDRAW, false, 0);
                action();
            }
            finally
            {
                SendMessage(control.Handle, WM_SETREDRAW, true, 0);
                control.Refresh();
            }
        }
    }
}
