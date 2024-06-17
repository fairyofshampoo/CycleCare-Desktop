using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using CycleCare.Views.ContentModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using static System.Net.Mime.MediaTypeNames;

namespace CycleCare.Views
{
    public partial class ContentDetailView : Page
    {
        public  InformativeContentJSONResponse informativeContent { get; set; }

        public ContentDetailView()
        {
            InitializeComponent();
        }

        public void SetInformativeContent(InformativeContentJSONResponse content)
        {
            informativeContent = content;
            ShowInformativeContentData();
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            ContentsView contentsView = new ContentsView();
            NavigationService.Navigate(contentsView);
        }

        private void ShowInformativeContentData()
        {
            if (informativeContent != null) 
            {
                txtTitle.Text = informativeContent.title;
                txtDescription.Text = informativeContent.description;
                txtAuthor.Text = "Por: " + informativeContent.username;
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri("http://localhost:8085/images/" + informativeContent.media);
                bi.EndInit();
                img_informativeContent.Source = bi;
            }
        }

        private void RateContentCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            ComboBoxItem selectedItem = comboBox.SelectedItem as ComboBoxItem;

            if (selectedItem != null) 
            { 
                String selectedContent = selectedItem.Content.ToString();
                RateContent(selectedContent);
            }
        }

        private async void RateContent(string rate)
        {
            int rating = int.Parse(rate);
            Response response = await ContentService.RateInformativeContent(informativeContent.contentId, rating);
            switch (response.Code)
            {
                case (int)HttpStatusCode.OK:
                    DialogManager.ShowSuccessMessageBox("Se ha calificado el contenido");
                    RateContentCB.IsEnabled = false;
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    DialogManager.ShowWarningMessageBox("Error al intentar calificar el contenido, inténtelo más tarde.");
                    break;
            }
        }
    }
}
