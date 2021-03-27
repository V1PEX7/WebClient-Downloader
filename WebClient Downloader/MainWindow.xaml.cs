using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
namespace WebClient_Downloader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string[] splitarr = LinkBox.Text.Split('.');
            string ext = splitarr[splitarr.Length - 1];
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = $"*.{ext}|*.{ext}";
            if (saveFileDialog.ShowDialog() == true)
            {
                using (WebClient w = new WebClient())
                {
                    w.DownloadProgressChanged += (s, e) =>
                    {
                        progressBar.Value = e.ProgressPercentage;
                        DLabel.Content = $"Downloaded: {((double)e.BytesReceived / 1048576).ToString("#.#")} MB/{((double)e.TotalBytesToReceive / 1048576).ToString("#.#")} MB";
                    };
                    w.DownloadFileAsync(new Uri(LinkBox.Text), saveFileDialog.FileName);
                }
            }
                
        }
    }
}
