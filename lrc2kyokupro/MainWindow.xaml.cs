using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace lrc2kyokupro
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    

    public partial class MainWindow : Window
    {
        private string[] filePath = new string[2];
        private bool flg_ext = false;
        public MainWindow()
        {
            InitializeComponent();

            flg_ext = lrc2kyokupro.Properties.Settings.Default.ext_flg;
            if (flg_ext)
            {
                filePath[0] = lrc2kyokupro.Properties.Settings.Default.filePath;
                filePath[1] = lrc2kyokupro.Properties.Settings.Default.outPath;
                lbl_fileName.Text = filePath[0];
                lbl_outputName.Text = filePath[1];

                btn_save.IsEnabled = true;
                btn_convert.IsEnabled = true;

                btn_convert_Click(btn_convert,new RoutedEventArgs());
            }

            btn_save.IsEnabled = flg_ext;
            btn_convert.IsEnabled = flg_ext;
            
            grid.PreviewDragOver += grid_PreviewDragOver;
            grid.Drop += grid_Drop;
            grid.AllowDrop = true;
            
        }

        private void grid_PreviewDragOver(object sender, System.Windows.DragEventArgs e)
        {
            var dropFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop) as string[];
            if (dropFiles == null) return;

            string extension = System.IO.Path.GetExtension(dropFiles[0]);
            bool flg = (extension.Equals(".txt") || extension.Equals(".lrc"));

            if (e.Data.GetDataPresent(System.Windows.DataFormats.FileDrop, true) && flg)
            {
                e.Effects = System.Windows.DragDropEffects.Copy;
            }
            else
            {
                e.Effects = System.Windows.DragDropEffects.None;
            }
            e.Handled = true;
        }
        private void grid_Drop(object sender, System.Windows.DragEventArgs e)
        {
            var dropFiles = e.Data.GetData(System.Windows.DataFormats.FileDrop) as string[];
            if (dropFiles == null) return;

            string extension = System.IO.Path.GetExtension(dropFiles[0]);
            if (extension.Equals(".txt") || extension.Equals(".lrc"))
            {
                filePath[0] = dropFiles[0];
                filePath[1] = Regex.Replace(filePath[0], ".txt$", ".prj");
                filePath[1] = Regex.Replace(filePath[1], ".lrc$", ".prj");
                lbl_fileName.Text = filePath[0];
                lbl_outputName.Text = filePath[1];

                btn_save.IsEnabled = true;
                btn_convert.IsEnabled = true;
            }
            
        }

        private void btn_select_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            
            dialog.Filter = "lrcファイル (*.lrc)|*.lrc|テキストファイル (*.txt)|*.txt";
            
            if (dialog.ShowDialog() == true)
            {
                filePath[0] = dialog.FileName;
                filePath[1] = Regex.Replace(filePath[0], ".txt$", ".prj");
                filePath[1] = Regex.Replace(filePath[1], ".lrc$", ".prj");
                lbl_fileName.Text = filePath[0];
                lbl_outputName.Text = filePath[1];
                
                btn_save.IsEnabled = true;
                btn_convert.IsEnabled = true;
            }
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            
            dialog.Filter = "キョクプロファイル (*.prj)|*.prj";
            dialog.InitialDirectory = System.IO.Path.GetDirectoryName(filePath[1]);
            dialog.FileName = System.IO.Path.GetFileName(filePath[1]);
            
            if (dialog.ShowDialog() == true)
            {
                filePath[1] = dialog.FileName;
                lbl_outputName.Text = filePath[1];
            }
        }

        private void btn_convert_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                ArrayList pageTmp = new ArrayList();
                ArrayList list = new ArrayList();

                using (StreamReader sr = new StreamReader(filePath[0], Encoding.GetEncoding("Shift_JIS")))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line.Length > 0 && Regex.IsMatch(line, @"^\[([0-9]+):([0-9]+):([0-9]+)\].*\[([0-9]+):([0-9]+):([0-9]+)\]$") )
                        {
                            pageTmp.Add(line);
                        }
                        else
                        {
                            if (pageTmp.Count > 0) list.Add(pageTmp);
                            pageTmp = new ArrayList();
                        }
                    }

                    if (pageTmp.Count > 0) list.Add(pageTmp);
                    pageTmp = new ArrayList();
                }

                string xml = lrc2kyokupro.Properties.Resources.xml_header;


                ArrayList arrPageStTimes = new ArrayList();
                ArrayList arrPageEdTimes = new ArrayList();
                foreach (ArrayList page in list)
                {
                    Match stM = Regex.Match((string)page[0], "([0-9]+):([0-9]+):([0-9]+)");
                    string stStr = Regex.Replace(stM.Value, ":", ",");
                    string[] stSplit = stStr.Split(',');
                    int stTime = (Int32.Parse(stSplit[0]) * 60 * 1000) + (Int32.Parse(stSplit[1]) * 1000) + (Int32.Parse(stSplit[2]) * 10);


                    Match edM = Regex.Match((string)page[page.Count - 1], "([0-9]+):([0-9]+):([0-9]+)]$");
                    string edStr = Regex.Replace(edM.Value.Trim(']'), ":", ",");
                    string[] edSplit = edStr.Split(',');
                    int edTime = (Int32.Parse(edSplit[0]) * 60 * 1000) + (Int32.Parse(edSplit[1]) * 1000) + (Int32.Parse(edSplit[2]) * 10);

                    arrPageStTimes.Add(stTime);
                    arrPageEdTimes.Add(edTime);

                }

                int prevPageEndTime = 0;
                int cnt = 0;

                foreach (ArrayList page in list)
                {
                    int stTime = (int)arrPageStTimes[cnt];
                    stTime -= 2000;
                    if (stTime < 0) stTime = 0;
                    if (stTime < prevPageEndTime) stTime = prevPageEndTime;

                    if(stTime - prevPageEndTime > 0 && prevPageEndTime != 0)
                    {
                        xml += $"\n    <page>\n      <show_time>{prevPageEndTime}</show_time>";
                        xml += $"\n      <hide_time>{stTime}</hide_time>\n      <paint_timing>0</paint_timing>\n      <layout>xing_0</layout>";
                        xml += $"\n    </page>";
                    }
                    
                    int edTime = (int)arrPageEdTimes[cnt];
                    edTime += 2000;
                    if (cnt + 1 < arrPageStTimes.Count)
                    {
                        int nextStTme = (int)arrPageStTimes[cnt + 1];
                        if (nextStTme < edTime) edTime = nextStTme;
                    }
                    
                    prevPageEndTime = edTime;

                    cnt++;

                    xml += $"\n    <page>\n      <show_time>{stTime}</show_time>";
                    xml += $"\n      <hide_time>{edTime}</hide_time>\n      <paint_timing>0</paint_timing>\n      <layout>xing_0</layout>";

                    foreach (string line in page)
                    {
                        xml += $"\n      <line>";
                        string word = Regex.Replace(line, ",", "，");
                        word = Regex.Replace(line, @"\[([0-9]+):([0-9]+):([0-9]+)\]", ",");
                        word = Regex.Replace(word, "^,", "");
                        string[] wordList = Regex.Replace(word, ",$", "").Split(',');

                        ArrayList timeTag = new ArrayList();
                        MatchCollection mcTime = Regex.Matches(line, @"\[([0-9]+):([0-9]+):([0-9]+)\]");
                        foreach (Match mTime in mcTime)
                        {
                            string[] tagList = Regex.Replace(mTime.Value, ":", ",").Trim(']').Trim('[').Split(',');
                            timeTag.Add((Int32.Parse(tagList[0]) * 60 * 1000) + (Int32.Parse(tagList[1]) * 1000) + (Int32.Parse(tagList[2]) * 10));
                        }
                        int wordLength = 0;
                        for (int i = 0; i < wordList.Length; i++)
                        {
                            xml += $"\n        <word>";
                            xml += $"\n          <text>{wordList[i]}</text>";
                            xml += $"\n          <start_time>{timeTag[i]}</start_time>";
                            xml += $"\n          <end_time>{timeTag[i + 1]}</end_time>";
                            xml += $"\n        </word>";

                            wordLength += wordList[i].Length;
                        }

                        xml += $"\n        <color>\n          <before_text_color>16777215</before_text_color>\n          <after_text_color>180</after_text_color>\n          <before_shadow_color>526344</before_shadow_color>";
                        xml += $"\n          <after_shadow_color>16777215</after_shadow_color>\n          <before_text_no_fill_color>16777215</before_text_no_fill_color>\n          <after_text_no_fill_color>180</after_text_no_fill_color>";
                        xml += $"\n          <before_shadow_no_fill_color>526344</before_shadow_no_fill_color>\n          <after_shadow_no_fill_color>16777215</after_shadow_no_fill_color>\n          <start_pos>0</start_pos>";
                        xml += $"\n          <end_pos>{wordLength - 1}</end_pos>";
                        xml += $"\n          <no_fill>0</no_fill>\n        </color>\n        <duet>\n          <mark>0</mark>\n          <start_pos>0</start_pos>";
                        xml += $"\n          <end_pos>{wordLength - 1}</end_pos>";
                        xml += $"\n        </duet>";
                        xml += $"\n      </line>";
                    }

                    xml += $"\n    </page>";

                }

                xml += lrc2kyokupro.Properties.Resources.xml_footer;
                File.WriteAllText(filePath[1], xml);

                MessageBox.Show("変換が完了しました。");
            }
            catch (Exception)
            {
                MessageBox.Show("入力ファイルを確認してください。", "エラー", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (flg_ext)
            {
                lrc2kyokupro.Properties.Settings.Default.ext_flg = false;
                lrc2kyokupro.Properties.Settings.Default.filePath = "";
                lrc2kyokupro.Properties.Settings.Default.outPath = "";
                lrc2kyokupro.Properties.Settings.Default.Save();

                Application.Current.Shutdown();
            }

        }
    }
}
