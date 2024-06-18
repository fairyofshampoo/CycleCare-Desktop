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
        public InformativeContentJSONResponse informativeContent { get; set; }

        public ContentDetailsForMedic()
        {
            InitializeComponent();
            ShowInformativeContentDetails();
        }

        public void SetInformativeContent(InformativeContentJSONResponse content)
        {
            this.informativeContent = content;
            ShowInformativeContentDetails();
        }

        private void ShowInformativeContentDetails()
        {
            if(informativeContent != null)
            {
                txtTitle.Text = informativeContent.title;
                txtDescription.Text = informativeContent.description;
                var bi = new BitmapImage();
                bi.BeginInit();
                bi.UriSource = new Uri("http://localhost:8085/images/" + informativeContent.media);
                bi.EndInit();
                img_InformativeContent.Source = bi;
            }
        }

        private void BtnEditContent_Click(object sender, RoutedEventArgs e)
        {
            RegisterInformativeContent registerInformativeContent = new RegisterInformativeContent(true, informativeContent);
            NavigationService.Navigate(registerInformativeContent);
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            MyContentByMedic myContentByMedic = new MyContentByMedic();
            NavigationService.Navigate(myContentByMedic);
        }
    }
}
