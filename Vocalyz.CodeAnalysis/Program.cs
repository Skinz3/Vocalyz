using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vocalyz.DesignPattern;

namespace Vocalyz.CodeAnalysis
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "Vocalyz.CodeAnalysis";

            string result = DevelopmentManager.Analyse(Assembly.GetAssembly(typeof(Physics)));

            OpenNotepad(result);
            Console.WriteLine("Analysis done.");
            Console.Read();
        }
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool PostMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        const uint WM_PASTE = 0x302;

        static void OpenNotepad(string text)
        {
            Clipboard.SetText(text);
            Process p = Process.Start("notepad.exe");
            p.WaitForInputIdle();
            IntPtr EditHandle = FindWindowEx(p.MainWindowHandle, IntPtr.Zero, "edit", null);
            PostMessage(new HandleRef(p, EditHandle), WM_PASTE, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
