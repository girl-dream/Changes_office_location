using System;
using System.Threading;
using System.Windows.Forms;

namespace winform3._5
{
    internal static class Program
    {
        private static Mutex mutex = new Mutex(true, Application.ProductName);

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(true);
                Application.Run(new Form1());
                mutex.ReleaseMutex();
            }
            else
            {
                MessageBox.Show("程序已经在运行中！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(Application.ProductName);
            }
        }
    }
}
