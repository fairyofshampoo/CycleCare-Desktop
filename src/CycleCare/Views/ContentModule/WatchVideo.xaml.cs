using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Controls;
using CycleCare.GrpcClients;

namespace CycleCare.Views.ContentModule
{
    public partial class WatchVideo : Page
    {
        private GrpcVideoClient _grpcVideoClient;
        private string _fileName;

        public WatchVideo(string fileName)
        {
            InitializeComponent();
            _grpcVideoClient = new GrpcVideoClient();
            _fileName = fileName;
        }

        private async void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            string fileName = _fileName;
            await _grpcVideoClient.DownloadVideo(fileName, OnStreamStarted);
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