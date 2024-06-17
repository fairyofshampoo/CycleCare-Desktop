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

namespace CycleCare.Views
{
    public partial class ContentsView : Page
    {
        public ContentsView()
        {
            InitializeComponent();
            SetInformativeContentInPage();
        }

        private async void SetInformativeContentInPage()
        {
            Response response = await ContentService.GetCurrentInformativeContent();
            switch (response.Code)
            {
                case(int) HttpStatusCode.OK:
                    ShowInformativeContent(response.contents);
                    break;
                case (int)HttpStatusCode.NotFound:
                    DialogManager.ShowWarningMessageBox("No hay contenido disponible.");
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    DialogManager.ShowErrorMessageBox("Error al recuperar el contenido informativo. Intente más tarde.");
                    break;
            }
        }

        public void ShowInformativeContent(List<InformativeContentJSONResponse> content)
        {
            lstInformativeContent.Items.Clear();
            foreach (var informativeContent in content)
            {
                AddInformativeContentToList(informativeContent);
            }
        }

        public void AddInformativeContentToList(InformativeContentJSONResponse content) 
        {
            InformativeContentUC informativeContentUC = new InformativeContentUC();
            informativeContentUC.ContentsView = this;
            informativeContentUC.SetInformativeContentData(content);
            lstInformativeContent.Items.Add(informativeContentUC);
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnArticles_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnVideos_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
