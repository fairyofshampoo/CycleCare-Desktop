using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;

namespace CycleCare.Views.ContentModule
{
    public partial class RegisterInformativeContent : Page
    {

        private string base64String = string.Empty;

        public InformativeContentJSONResponse informativeContent { get; set; }

        public RegisterInformativeContent(bool isEdition, InformativeContentJSONResponse content)
        {
            InitializeComponent();
            this.informativeContent = content;
            ShowDetails(isEdition);
        }

        private void ShowDetails(bool isEdition) 
        {
            if (isEdition)
            {
                ShowEditionDetails();
            }
            else
            {
                ShowCreationDetails();
            }
        }

        private void ShowEditionDetails()
        {
            if(informativeContent != null)
            {
                txtTittle.Text = informativeContent.title;
                txtDescription.Text = informativeContent.description;
                btnEditContent.Visibility = Visibility.Visible;
            }
        }

        private void ShowCreationDetails() 
        { 
            btnPublishContent.Visibility = Visibility.Visible;
            txtDescription.Text = "";
            txtTittle.Text = "";
            btnEditContent.Visibility = Visibility.Collapsed;
        }


        private async void BtnPublishContent_Click(object sender, RoutedEventArgs e)
        {
            List<int> errors = validateFields();
            clearErrors();
            if (errors.Any())
            {
                showErrors(errors);
            }else
            {
               Article newArticle =  CreateArticle();
               Response response = await ContentService.PublishArticle(newArticle);
               switch (response.Code)
               {
                    case (int)HttpStatusCode.Created:
                        DialogManager.ShowSuccessMessageBox("Se ha publicado el artículo correctamente");
                        break;
                    case (int)HttpStatusCode.BadRequest:
                        DialogManager.ShowWarningMessageBox("No se ha podido registrar el artículo.");
                        break;
                    case (int)HttpStatusCode.InternalServerError:
                        DialogManager.ShowErrorMessageBox("Error interno del servidor. Inténtalo más tarde.");
                        break;
                }
            }
        }

        private async void BtnEditContent_Click(object sender, RoutedEventArgs e)
        {
            List<int> errors = validateFields();
            clearErrors();
            if (errors.Any())
            {
                showErrors(errors);
            }
            else
            {
                ArticleUpdated articleUpdated = CreateArticleUpdated();
                Response response = await ContentService.EditArticle(articleUpdated);
                switch (response.Code)
                {
                    case (int)HttpStatusCode.OK:
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

        private Article CreateArticle()
        {
            Article article = new Article();
            article.title = txtTittle.Text;
            article.description = txtDescription.Text;
            article.image = base64String;
            article.creationDate = getCreationDate();
            return article;
        }

        private String getCreationDate() 
        {
            DateTime currentDate = DateTime.Now;
            return currentDate.ToString("yyyy-MM-dd");
        }

        private ArticleUpdated CreateArticleUpdated()
        {
            ArticleUpdated articleUpdated = new ArticleUpdated();
            articleUpdated.contentId = informativeContent.contentId;
            articleUpdated.imageName = informativeContent.media;
            articleUpdated.title = txtTittle.Text;
            articleUpdated.description = txtDescription.Text;
            articleUpdated.newImage = base64String;
            return articleUpdated;
        }

        private void clearErrors()
        {
            lblTitleError.Visibility = Visibility.Collapsed;
            lblDescriptionError.Visibility = Visibility.Collapsed;
            lblImageError.Visibility = Visibility.Collapsed;   
        }

        private void showErrors(List<int> errors)
        {
            foreach(var error in errors)
            {
                switch (error)
                {
                    case 0:
                        lblTitleError.Visibility = Visibility.Visible;
                        break;
                    case 1:
                        lblDescriptionError.Visibility = Visibility.Visible;
                        break;
                    case 2:
                        lblImageError.Visibility = Visibility.Visible;
                        break;
                }
            }
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            //Mandar a la pantalla anterior
        }

        private List<int> validateFields()
        {
            List<int> errors = new List<int>();

            if (!isTitleValid())
            {
                errors.Add(0);
            }

            if (!isDescriptionValid())
            {
                errors.Add(1);
            }

            if (!isImageSelected())
            {
                errors.Add(2);
            }

            return errors;
        }

        private bool isTitleValid()
        {
            return ValidationManager.IsTitleValid(txtTittle.Text);
        }

        private bool isDescriptionValid()
        {
           return ValidationManager.isDescriptionValid(txtDescription.Text);
        }

        private bool isImageSelected()
        {
            bool isValid = false;
            if (!string.IsNullOrEmpty(base64String))
            {
                isValid = true;
            }
            return isValid;
        }

        private void BtnUploadImage_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg;*.bmp)|*.png;*.jpeg;*.jpg;*.bmp",
                Title = "Selecciona una imagen de producto"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string imagePath = openFileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(imagePath);
                base64String = Convert.ToBase64String(imageBytes);

            }
        }

    }
    
}
