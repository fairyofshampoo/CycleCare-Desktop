using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;

namespace CycleCare.Views.ContentModule
{
    public partial class RegisterInformativeContentWithVideo : Page
    {

        public RegisterInformativeContentWithVideo()
        {
            InitializeComponent();
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            MyContentByMedic myContentByMedic = new MyContentByMedic();
            NavigationService.Navigate(myContentByMedic);
        }

        private void UploadContent_Clicked(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Video Files (*.mp4)|*.mp4"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                videoGrid.Visibility = Visibility.Collapsed;
                lblVideoUploaded.Visibility = Visibility.Visible;
                lblVideoUploaded.Content = fileName;
                UploadVideoAsync(fileName);
            }
        }

        private async void UploadVideoAsync(string fileName)
        {
            try
            {
                byte[] fileData = await ReadFileAsync(fileName);
                MessageBox.Show("Video subido exitosamente.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al subir el video: {ex.Message}");
            }
        }

        private async Task<byte[]> ReadFileAsync(string filePath)
        {
            byte[] buffer;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                buffer = new byte[fs.Length];
                await fs.ReadAsync(buffer, 0, (int)fs.Length);
            }
            return buffer;
        }

        private void BtnPublishContent_Click(object sender, RoutedEventArgs e)
        {
            // Si el título cumple lo de ValidationManager.IsTitleValid(txtTittle.Text) y hay un videoSeleccionado
        }
    }
}