using CycleCare.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace CycleCare.Views.ContentModule
{

    public partial class InformativeContentUC : UserControl
    {

        private InformativeContentJSONResponse data;

        public ContentsView ContentsView { get; set; }

        public InformativeContentUC()
        {
            InitializeComponent();
        }


        public void SetInformativeContentData(InformativeContentJSONResponse informativeContent)
        {
            data = informativeContent;
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("http://192.168.1.77:8085/images/" + informativeContent.media);
            bi.EndInit();
            informativeContentImage.Source = bi;
            string newDate = FormateToDate(informativeContent.creationDate);
            txtDate.Text = newDate;
            txtTitle.Text = informativeContent.title;

        }

        private string FormateToDate(string date)
        {
            DateTime dateTime = DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
            return dateTime.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        private void Content_Clicked(object sender, MouseButtonEventArgs e)
        {
            ContentDetailView contentDetail = new ContentDetailView();
            contentDetail.SetInformativeContent(data);
            ContentsView.NavigationService.Navigate(contentDetail);
        }

    }
}
