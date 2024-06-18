using CycleCare.Views.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CycleCare.Utilities
{
    public static class DialogManager
    {
        public static void ShowErrorMessageBox(string errorMessage)
        {
            DialogWindow dialogWindow = new DialogWindow();
            dialogWindow.SetDialogWindowData("Error", errorMessage, DialogWindow.DialogType.OK, DialogWindow.IconType.Error);
            dialogWindow.ShowDialog();
        }

        public static void ShowWarningMessageBox(string warningMessage)
        {
            DialogWindow dialogWindow = new DialogWindow();
            dialogWindow.SetDialogWindowData("Advertencia", warningMessage, DialogWindow.DialogType.OK, DialogWindow.IconType.Warning);
            dialogWindow.ShowDialog();
        }

        public static void ShowSuccessMessageBox(string successMessage)
        {
            DialogWindow dialogWindow = new DialogWindow();
            dialogWindow.SetDialogWindowData("Operación exitosa", successMessage, DialogWindow.DialogType.OK, DialogWindow.IconType.Information);
            dialogWindow.ShowDialog();
        }
    }
}
