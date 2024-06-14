using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using CycleCare.Views.CycleModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;

namespace CycleCare.Views
{
    /// <summary>
    /// Interaction logic for NewCycleLog.xaml
    /// </summary>
    public partial class NewCycleLog : Page
    {
        private List<StackPanel> _moodsSelected = new List<StackPanel>();
        private StackPanel _selectedMenstrualFlowPanel = null;
        private StackPanel _selectedVaginalFlowPanel = null;
        public NewCycleLog()
        {
            InitializeComponent();
        }

        private void BtnGoBack_Click(object sender, RoutedEventArgs e)
        {
            CycleCalendarView cycleCalendarView = new CycleCalendarView();
            NavigationService.Navigate(cycleCalendarView);
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateData())
            {
                SaveCycleLog();
            } else
            {
                DialogManager.ShowWarningMessageBox("No dejé ningun campo vacío o sin seleccionar");
            }
        }

        private void SaveCycleLog()
        {
            // Crear un nuevo objeto CycleLog con los datos validados
            CycleLog newCycleLog = new CycleLog
            {
                Username = "nombre de usuario", // Asigna el nombre de usuario adecuado
                CreationDate = DateTime.Now, // Asigna la fecha actual
                SleepHours = GetSleepHours(), // Obtiene las horas de sueño del ComboBox
                Note = txtNote.Text.Trim(), // Obtiene la nota del TextBox

                // Asigna los valores seleccionados de los paneles de flujo menstrual y vaginal
                MenstrualFlow = GetMenstrualFlow(),
                VaginalFlow = GetVaginalFlow(),

                // Asigna los síntomas seleccionados de los CheckBoxes
                Symptoms = GetSelectedSymptoms(),

                // Asigna los estados de ánimo seleccionados de los StackPanels
                Moods = GetSelectedMoods(),

                // Asigna las medicaciones seleccionadas de los CheckBoxes
                Medications = GetSelectedMedications(),

                // Asigna la píldora anticonceptiva seleccionada de los CheckBoxes
                Pills = GetSelectedPills(),

                // Asigna el método de control de nacimiento seleccionado de los RadioButtons
                BirthControl = GetSelectedBirthControl()
            };

            // Llama al método para crear un nuevo registro de ciclo
            CreateNewCycleLog(newCycleLog);
        }

        // Métodos de obtención de datos de la interfaz de usuario (ejemplos)
        private int GetSleepHours()
        {
            // Implementa la lógica para obtener las horas de sueño del ComboBox
            // Ejemplo: return int.Parse(sleepHoursComboBox.SelectedItem.ToString());
            return 0;
        }

        private MenstrualFlow GetMenstrualFlow()
        {
            // Implementa la lógica para obtener el flujo menstrual seleccionado
            // Ejemplo: return MenstrualFlow.Light;
            MenstrualFlow menstrualFlow = new MenstrualFlow();
            return menstrualFlow;
        }

        private VaginalFlow GetVaginalFlow()
        {
            // Implementa la lógica para obtener el flujo vaginal seleccionado
            // Ejemplo: return VaginalFlow.Normal;
            VaginalFlow vaginalFlow = new VaginalFlow();
            return vaginalFlow;
        }

        private List<Symptom> GetSelectedSymptoms()
        {
            // Implementa la lógica para obtener los síntomas seleccionados
            // Ejemplo: return new List<Symptom> { Symptom.Fatigue, Symptom.Nausea };
            return new List<Symptom>();
        }

        private List<Mood> GetSelectedMoods()
        {
            // Implementa la lógica para obtener los estados de ánimo seleccionados
            // Ejemplo: return new List<Mood> { Mood.Happy, Mood.Sad };
            return new List<Mood>();
        }

        private List<Medication> GetSelectedMedications()
        {
            List<Medication> selectedMedications = new List<Medication>();

            // Verificar si alguna medicación está seleccionada y agregarla a la lista
            if (hormoneTherapyCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication
                {
                    MedicationId = 1, // ID de la medicación "Terapia hormonal" (ajustar según tu lógica)
                    Name = "Terapia hormonal" // Nombre de la medicación "Terapia hormonal" (ajustar según tu lógica)
                });
            }

            if (emergencyPillCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication
                {
                    MedicationId = 2, // ID de la medicación "Pastilla de emergencia" (ajustar según tu lógica)
                    Name = "Pastilla de emergencia" // Nombre de la medicación "Pastilla de emergencia" (ajustar según tu lógica)
                });
            }

            // Agregar más condiciones según sea necesario para otras medicaciones

            return selectedMedications;
        }

        private List<Pill> GetSelectedPills()
        {
            List<Pill> selectedPills = new List<Pill>();

            // Verificar si alguna píldora está seleccionada y agregarla a la lista
            if (pillTakenCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill
                {
                    PillId = 1, // ID de la píldora "Tomada" (ajustar según tu lógica)
                    Status = "Tomada" // Estado de la píldora "Tomada" (ajustar según tu lógica)
                });
            }

            if (pillForgottenCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill
                {
                    PillId = 2, // ID de la píldora "Olvidada" (ajustar según tu lógica)
                    Status = "Olvidada" // Estado de la píldora "Olvidada" (ajustar según tu lógica)
                });
            }

            return selectedPills;
        }

        private BirthControl GetSelectedBirthControl()
        {
            // Verificar si el RadioButton de "Insertado" está seleccionado
            if (insertedRadioButton.IsChecked == true)
            {
                // Crear un objeto BirthControl con el método "Insertado"
                return new BirthControl
                {
                    BirthControlId = 1, // ID del método "Insertado" (ajustar según tu lógica)
                    Name = "Insertado",
                    Status = "Activo" // Estado del método "Insertado" (ajustar según tu lógica)
                };
            }
            else
            {
                // Crear un objeto BirthControl con el método "Removido"
                return new BirthControl
                {
                    BirthControlId = 2, // ID del método "Removido" (ajustar según tu lógica)
                    Name = "Removido",
                    Status = "Inactivo" // Estado del método "Removido" (ajustar según tu lógica)
                };
            }
        }

        private async void CreateNewCycleLog(CycleLog newCycleLog)
        {
            Response response = await CycleService.CreateCycleLog(newCycleLog);
            if (response.Code == (int)HttpStatusCode.Created)
            {
                DialogManager.ShowSuccessMessageBox("Recordatorio creado exitosamente");
            }
            else
            {
                DialogManager.ShowErrorMessageBox("Error al crear el recordatorio. Por favor, intente nuevamente.");
            }
        }

        private bool ValidateData()
        {
            // Validar nota
            if (!ValidateNoteText())
            {
                return false;
            }

            // Validar al menos un CheckBox seleccionado en el WrapPanel de síntomas
            if (!ValidateCheckBoxesSelected(symptomsWrapPanel))
            {
                return false;
            }

            // Validar al menos un RadioButton seleccionado en el StackPanel de actividad sexual
            if (!ValidateRadioButtonSelected(sexualActivityStackPanel))
            {
                return false;
            }

            // Validar al menos un CheckBox seleccionado en el WrapPanel de medicación
            if (!ValidateCheckBoxesSelected(medicationWrapPanel))
            {
                return false;
            }

            // Validar que la hora de sueño no esté vacía
            if (!ValidateSleepTime())
            {
                return false;
            }

            // Validar que solo un RadioButton esté seleccionado en el WrapPanel de control de nacimiento
            if (!ValidateSingleRadioButtonSelected(birthControlWrapPanel))
            {
                return false;
            }

            if (_moodsSelected == null || _moodsSelected.Count == 0)
            {
                return false;
            }
            if (_selectedMenstrualFlowPanel == null)
            {
                return false;
            }

            // Validar que el panel de flujo vaginal seleccionado no sea nulo
            if (_selectedVaginalFlowPanel == null)
            {
                return false;
            }

            return true;
        }

        private bool ValidateCheckBoxesSelected(WrapPanel wrapPanel)
        {
            foreach (var child in wrapPanel.Children)
            {
                if (child is CheckBox checkBox && checkBox.IsChecked == true)
                {
                    return true;
                }
            }
            return false;
        }


        private bool ValidateNoteText()
        {
            // Obtener el texto del TextBox de la nota
            string noteText = txtNote.Text.Trim();

            // Validar que el campo de texto de la nota no esté vacío
            if (string.IsNullOrWhiteSpace(noteText))
            {
                txtNote.BorderBrush = new SolidColorBrush(Colors.Red);
                return false;
            }

            // Validar que el texto de la nota cumpla con el regex
            Regex regex = new Regex(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$");
            if (!regex.IsMatch(noteText))
            {
                DialogManager.ShowWarningMessageBox("La nota solo puede contener caracteres en español y espacios.");
                return false;
            }

            return true;
        }


        private bool ValidateSingleRadioButtonSelected(WrapPanel wrapPanel)
        {
            int radioButtonCheckedCount = 0;
            foreach (var child in wrapPanel.Children)
            {
                if (child is RadioButton radioButton && radioButton.IsChecked == true)
                {
                    radioButtonCheckedCount++;
                }
            }
            if (radioButtonCheckedCount != 1)
            {
                return false;
            }
            return true;
        }

        private bool ValidateRadioButtonSelected(StackPanel stackPanel)
        {
            foreach (var child in stackPanel.Children)
            {
                if (child is RadioButton radioButton && radioButton.IsChecked == true)
                {
                    return true;
                }
            }
            return false;
        }


        private bool ValidateSleepTime()
        {
            if (sleepHoursComboBox.SelectedItem == null || sleepMinutesComboBox.SelectedItem == null)
            {
                MessageBox.Show("Por favor, selecciona una hora de sueño.");
                return false;
            }
            return true;
        }


        private void UpdateMenstrualFlowSelection(StackPanel selectedPanel)
        {
            if (_selectedMenstrualFlowPanel != null)
            {
                _selectedMenstrualFlowPanel.Background = new SolidColorBrush(Colors.White);
            }

            _selectedMenstrualFlowPanel = selectedPanel;
            _selectedMenstrualFlowPanel.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void MenstrualFlow_Click(object sender, MouseButtonEventArgs e)
        {
            StackPanel selectedPanel = sender as StackPanel;
            UpdateMenstrualFlowSelection(selectedPanel);
        }

        private void UpdateVaginalFlowSelection(StackPanel selectedPanel)
        {
            if (_selectedVaginalFlowPanel != null)
            {
                _selectedVaginalFlowPanel.Background = new SolidColorBrush(Colors.White);
            }

            _selectedVaginalFlowPanel = selectedPanel;
            _selectedVaginalFlowPanel.Background = new SolidColorBrush(Colors.LightGray);
        }

        private void VaginalFlow_Click(object sender, MouseButtonEventArgs e)
        {
            StackPanel selectedPanel = sender as StackPanel;
            UpdateVaginalFlowSelection(selectedPanel);
        }

        private void NoteTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length + e.Text.Length > 49)
            {
                e.Handled = true;
            }
        }

        private void NoteTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox.Text.Length > 49)
            {
                textBox.Text = textBox.Text.Substring(0, 49);
                textBox.CaretIndex = textBox.Text.Length;
            }
        }

        private void UpdateMoodSelection(StackPanel selectedPanel)
        {
            if (_moodsSelected.Contains(selectedPanel))
            {
                _moodsSelected.Remove(selectedPanel);
                selectedPanel.Background = new SolidColorBrush(Colors.White);
            }
            else
            {
                _moodsSelected.Add(selectedPanel);
                selectedPanel.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void Mood_Click(object sender, MouseButtonEventArgs e)
        {
            StackPanel selectedPanel = sender as StackPanel;
            UpdateMoodSelection(selectedPanel);
        }

    }
}
