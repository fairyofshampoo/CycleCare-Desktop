using CycleCare.Models;
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

namespace CycleCare.Views.ContentModule
{
    public partial class ContentDetailsForMedic : Page
    {

        public InformativeContentJSONResponse informativeContentData { get; set; }

        public ContentDetailsForMedic()
        {
            InitializeComponent();
            ShowInformativeContentDetails();
        }

        private void ShowInformativeContentDetails()
        {
            txtTitle.Text = informativeContentData.title;
            txtDescription.Text = informativeContentData.description;
            var bi = new BitmapImage();
            bi.BeginInit();
            bi.UriSource = new Uri("" + informativeContentData.media); //AQUÍ FALTA PONER CORRECTAMENTE LA URL BASE
            bi.EndInit();
            img_InformativeContent.Source = bi;
        }

        private void BtnEditContent_Click(object sender, RoutedEventArgs e)
        {
            //Deberá llevarlo a la pantalla de registrar un artículo
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
