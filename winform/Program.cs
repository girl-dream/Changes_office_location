using System;
using System.Threading;
using System.Windows.Forms;

namespace Office_location
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, "office_tool");
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {   
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("程序已经在运行中！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
