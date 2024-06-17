using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using CycleCare.GrpcClients;
using System.Collections.Generic;
using CycleCare.Utilities;
using System.Linq;
using CycleCare.Models;
using CycleCare.Service;
using System.Net;

namespace CycleCare.Views.ContentModule
{
    public partial class RegisterInformativeContentWithVideo : Page
    {
        private GrpcVideoClient _grpcVideoClient;

        private bool isVideoSelected = false;

        public RegisterInformativeContentWithVideo()
        {
            InitializeComponent();
            _grpcVideoClient = new GrpcVideoClient();
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
                isVideoSelected = true;
                UploadVideoAsync(fileName);
            }
        }

        private async void UploadVideoAsync(string fileName)
        {
            try
            {
                byte[] fileData = await ReadFileAsync(fileName);
                await _grpcVideoClient.UploadVideo(fileName, fileData);
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

        private async void BtnPublishContent_Click(object sender, RoutedEventArgs e)
        {
            ClearErrors();
            List<int> errors = ValidationData();
            if (errors.Any())
            {
                ShowErrors(errors);
            }else
            {
                Video video =  CreateVideo();
                Response response = await ContentService.PublishVideo(video);
                switch (response.Code)
                {
                    case (int)HttpStatusCode.Created:
                        DialogManager.ShowSuccessMessageBox("Se ha editado el artículo correctamente");
                        break;
                    case (int)HttpStatusCode.BadRequest:
                        DialogManager.ShowWarningMessageBox("No se ha podido editar el artículo.");
                        break;
                    case (int)HttpStatusCode.InternalServerError:
                        DialogManager.ShowErrorMessageBox("Error interno del servidor. Inténtalo más tarde.");
                        break;
                }
            }
        }

        private Video CreateVideo()
        {
            Video video = new Video();
            video.title = txtTitle.Text;
            video.creationDate = getCreationDate();
            return video;
        }

        private String getCreationDate()
        {
            DateTime currentDate = DateTime.Now;
            return currentDate.ToString("yyyy-MM-dd");
        }

        private void ShowErrors(List<int> errors)
        {
            foreach (var error in errors)
            {
                switch (error)
                {
                    case 0:
                        lblTitleError.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        lblVideoError.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void ClearErrors()
        {
            lblTitleError.Visibility = Visibility.Collapsed;
            lblVideoError.Visibility = Visibility.Collapsed;
        }

        private List<int> ValidationData()
        {
            List<int> errors = new List<int>();
            if (!isTitleValid())
            {
                errors.Add(0);
            }

            if(!isVideoSelected)
            {
                errors.Add(1);
            }

            return errors;
        }

        private bool isTitleValid()
        {
            return ValidationManager.IsTitleValid(txtTitle.Text);
        }
    }
}