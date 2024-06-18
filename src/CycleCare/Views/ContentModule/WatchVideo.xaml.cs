using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CycleCare.Views.ContentModule
{
    public partial class WatchVideo : Page
    {
        private string _fileName;

        public WatchVideo(string fileName)
        {
            InitializeComponent();
            _fileName = fileName;
        }

        private void OnStreamStarted(string tempFilePath)
        {
            Dispatcher.Invoke(() =>
            {
                videoPlayer.Source = new Uri(tempFilePath);
                videoPlayer.Play();
            });
        }

        private void BtnGoBack_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ContentsView contentsView = new ContentsView();
            NavigationService.Navigate(contentsView);
        }
    }
}