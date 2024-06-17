using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
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

namespace CycleCare.Views.ContentModule
{
    public partial class MyContentByMedic : Page
    {
        public MyContentByMedic()
        {
            InitializeComponent();
            SetInformativeContent();
        }

        private async void SetInformativeContent()
        {
            Response response = await ContentService.GetCurrentInformativeContent();
            switch (response.Code)
            {
                case (int)HttpStatusCode.OK:
                    ShowInformativeContent(response.Contents);
                    break;
                case (int)HttpStatusCode.NotFound:
                    DialogManager.ShowWarningMessageBox("No tiene contenido registrado.");
                    break;
                case (int)HttpStatusCode.InternalServerError:
                    DialogManager.ShowErrorMessageBox("Error al recuperar tu contenido informativo. Inténtelo más tarde.");
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
            InformativeContentForMedicUC informativeContentForMedicUC = new InformativeContentForMedicUC();
            informativeContentForMedicUC.myContent = this;
            informativeContentForMedicUC.SetInformativeContentData(content);
            lstInformativeContent.Items.Add(informativeContentForMedicUC);

        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUploadVideo_Click(object sender, RoutedEventArgs e)
        {
            //Tal vez quitemos esto
        }

        private void BtnRegisterNewContent_Click(object sender, RoutedEventArgs e)
        {
            RegisterInformativeContent registerInformativeContent = new RegisterInformativeContent();
            NavigationService.Navigate(registerInformativeContent);
        }
    }
}
