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
    public partial class MyContentByMedic : Page
    {
        public MyContentByMedic()
        {
            InitializeComponent();
            SetInformativeContent();
        }

        private void SetInformativeContent()
        {

        }

        public void AddInformativeContentToList(InformativeContentJSONResponse informativeContent)
        {
            InformativeContentForMedicUC informativeContentForMedicUC = new InformativeContentForMedicUC();
            informativeContentForMedicUC.myContent = this;

        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnUploadVideo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnRegisterNewContent_Click(object sender, RoutedEventArgs e)
        {
            //Debe enviar a la pantalla de crear contenido
        }
    }
}
