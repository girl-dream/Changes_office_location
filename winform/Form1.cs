using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Office_location
{
    public partial class Form1 : Form
    {
        private static readonly string Original_Office_Path = @"C:\Program Files\Microsoft Office";

        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CreateSymbolicLink(string lpSymlinkFileName, string lpTargetFileName, SymbolicLinkFlags dwFlags);

        private enum SymbolicLinkFlags
        {
            File = 0,
            Directory = 1
        }

        private void Block_OfficePlus_Click(object sender, System.EventArgs e)
        {
            try
            {
                RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\MSOfficePLUS", writable: true);

                if (key == null)
                {
                    key = Registry.LocalMachine.CreateSubKey(
                        @"SOFTWARE\Microsoft\MSOfficePLUS");
                }

                key.SetValue("InstallSuccess", "");
                key.Close();
                MessageBox.Show("设置完成", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception error)
            {
                MessageBox.Show(error.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void chang_location_Click(object sender, System.EventArgs e)
        {
            if (Directory.Exists(Original_Office_Path))
            {
                MessageBox.Show("发现默认安装位置已存在文件夹！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (FolderBrowserDialog folderDialog = new   ())
            {
                folderDialog.Description = "请选择Office安装更改后的文件夹";
                folderDialog.ShowNewFolderButton = true;
                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    string path = Path.Combine(folderDialog.SelectedPath, "Microsoft Office");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    try
                    {
                        // .NET Framework 不支持以下方法
                        //Directory.CreateSymbolicLink(Original_Office_Path, path);
                        bool temp = CreateSymbolicLink(Original_Office_Path, path, SymbolicLinkFlags.Directory);
                        if (temp)
                        {
                            MessageBox.Show("完成安装位置设置", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("创建符号链接失败！请确保以管理员身份运行此程序。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception error)
                    {
                        MessageBox.Show(error.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                }
                else
                {
                    MessageBox.Show("取消操作!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
