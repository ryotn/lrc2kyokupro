using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;

namespace lrc2kyokupro
{
    /// <summary>
    /// App.xaml の相互作用ロジック
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length <= 0) return;

            string arg = e.Args[0];
            string extension = System.IO.Path.GetExtension(arg);
            if (extension.Equals(".txt") || extension.Equals(".lrc"))
            {
                lrc2kyokupro.Properties.Settings.Default.ext_flg = true;
                lrc2kyokupro.Properties.Settings.Default.filePath = arg;
                string outPath = Regex.Replace(arg, ".txt$", ".prj");
                outPath = Regex.Replace(outPath, ".lrc$", ".prj");
                lrc2kyokupro.Properties.Settings.Default.outPath = outPath;
                lrc2kyokupro.Properties.Settings.Default.Save();
            }
            else
            {
                MessageBox.Show("非対応ファイルです。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown();
            }
        }
    }
}
