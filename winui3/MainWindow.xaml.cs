using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Windows.Storage.Pickers;
using Microsoft.Win32;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace winui3
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private static readonly string Original_Office_Path = @"C:\Program Files\Microsoft Office";

        public MainWindow()
        {
            InitializeComponent();
            AppWindow.Resize(new Windows.Graphics.SizeInt32(600, 400));
            var presenter = (OverlappedPresenter)AppWindow.Presenter;
            presenter.IsResizable = false;
            presenter.IsMaximizable = false;
        }

        private async Task<ContentDialogResult> ShowMessageBox(string content, string title = "提示")
        {
            ContentDialog dialog = new ContentDialog
            {
                Title = title,
                Content = content,
                //PrimaryButtonText = "",
                CloseButtonText = "确定",
                XamlRoot = this.Content.XamlRoot
            };
            return await dialog.ShowAsync();
        }

        private async void BlockOfficePLUS(object sender, RoutedEventArgs e)
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
                await ShowMessageBox("完成设置");
            }
            catch (Exception error)
            {
                await ShowMessageBox(error.ToString(), "错误");
            }
        }

        private async void btn(object sender, RoutedEventArgs e)
        {
            if (Directory.Exists(Original_Office_Path))
            {
                await ShowMessageBox("发现已经默认安装位置已存在文件夹！");
                return;
            }

            var folderPicker = new FolderPicker(this.AppWindow.Id)
            {
                SuggestedStartLocation = PickerLocationId.DocumentsLibrary,
                CommitButtonText = "选择Office安装的文件夹",
                ViewMode = PickerViewMode.List,
            };

            var result = await folderPicker.PickSingleFolderAsync();

            if (result is not null)
            {
                var path = Path.Combine(result.Path, "Microsoft Office");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                try
                {
                    Directory.CreateSymbolicLink(Original_Office_Path, path);
                    await ShowMessageBox("完成安装位置设置");
                }
                catch (Exception error)
                {
                    await ShowMessageBox(error.ToString(), "错误");
                }
            }
            else
            {
                await ShowMessageBox("未知错误", "错误");
            }
        }
    }
}