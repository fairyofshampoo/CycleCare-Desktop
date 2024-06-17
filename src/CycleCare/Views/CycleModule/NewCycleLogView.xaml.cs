using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using CycleCare.Views.CycleModule;
using Grpc.Core;
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
    public partial class NewCycleLogView : Page
    {
        private List<StackPanel> _moodsSelected = new List<StackPanel>();
        private StackPanel _selectedMenstrualFlowPanel = null;
        private StackPanel _selectedVaginalFlowPanel = null;
        public NewCycleLogView()
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
            SaveCycleLog();
        }

        private void SaveCycleLog()
        {
            NewCycleLog newCycleLog = new NewCycleLog();
            newCycleLog.CreationDate = DateTime.Now;

            if (sleepHoursComboBox.SelectedItem != null)
            {
                newCycleLog.SleepHours = int.Parse(sleepHoursComboBox.SelectedItem.ToString());
            }

            newCycleLog.Note = txtNote.Text;

            if(_selectedMenstrualFlowPanel != null)
            {
                newCycleLog.MenstrualFlowId = GetMenstrualFlowId();
            }

            if(_selectedVaginalFlowPanel != null)
            {
                newCycleLog.VaginalFlowId = GetVaginalFlowId();
            }

            newCycleLog.Symptoms = GetSelectedSymptoms();

            newCycleLog.Moods = GetSelectedMoods();

            newCycleLog.Medications = GetSelectedMedications();

            newCycleLog.Pills = GetSelectedPills();

            newCycleLog.BirthControls = GetSelectedBirthControl();

            CreateNewCycleLog(newCycleLog);
        }

        private int GetVaginalFlowId()
        {
            return _selectedVaginalFlowPanel != null ? (int)_selectedVaginalFlowPanel.Tag : 0;
        }

        private int GetMenstrualFlowId()
        {
            return _selectedMenstrualFlowPanel != null ? (int)_selectedMenstrualFlowPanel.Tag : 0;
        }

        private List<Symptom> GetSelectedSymptoms()
        {
            List<Symptom> selectedSymptoms = new List<Symptom>();

            if (abdominalPainCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 1,
                    Name = "Calambres"
                });
            }

            if (breastTendernessCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 2,
                    Name = "Sensibilidad en los senos"
                });
            }

            if (acneCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 3,
                    Name = "Acné"
                });
            }

            if (bloatingCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 4,
                    Name = "Hinchazón"
                });
            }

            if (fatigueCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 5,
                    Name = "Fatiga"
                });
            }

            if(headacheCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 6,
                    Name = "Dolor de cabeza"
                });
            }

            if(nauseaCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 7,
                    Name = "Náuseas"
                });
            }

            if(dizzinessCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 8,
                    Name = "Mareos"
                });
            }

            if (cravingsCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 9,
                    Name = "Antojos"
                });
            }

            if (constipationCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 10,
                    Name = "Estreñimiento"
                });
            }

            if(diarrheaCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 11,
                    Name = "Diarrea"
                });
            }

            if(vaginalItchingCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 12,
                    Name = "Picazón vaginal"
                });
            }

            if(vulvaPainCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom
                {
                    SymptomId = 13,
                    Name = "Dolor en vulva"
                });
            }

            return selectedSymptoms;
        }

        private List<Mood> GetSelectedMoods()
        {
            List<Mood> selectedMoods = new List<Mood>();

            foreach (var moodPanel in _moodsSelected)
            {
                if (moodPanel.Tag is Mood mood)
                {
                    selectedMoods.Add(mood);
                }
            }

            return selectedMoods;
        }

        private List<Medication> GetSelectedMedications()
        {
            List<Medication> selectedMedications = new List<Medication>();

            if (hormoneTherapyCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication
                {
                    MedicationId = 1,
                    Name = "Terapia hormonal"
                });
            }

            if (emergencyPillCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication
                {
                    MedicationId = 2,
                    Name = "Pastilla de emergencia"
                });
            }

            if(painkillersCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication
                {
                    MedicationId = 3,
                    Name = "Analgésicos"
                });
            }

            if(antidepressantsCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication
                {
                    MedicationId = 4,
                    Name = "Antidepresivos"
                });
            }

            if(antibioticsCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication
                {
                    MedicationId = 5,
                    Name = "Antibióticos"
                });
            }

            if(antihistaminesCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication
                {
                    MedicationId = 6,
                    Name = "Antihistamínicos"
                });
            }

            return selectedMedications;
        }

        private List<Pill> GetSelectedPills()
        {
            List<Pill> selectedPills = new List<Pill>();

            if (pillTakenCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill
                {
                    PillId = 1,
                    Status = "Tomada"
                });
            }

            if (pillForgottenCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill
                {
                    PillId = 2,
                    Status = "Olvidada"

                });
            }

            if (doubleDoseCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill
                {
                    PillId = 3,
                    Status = "Dosis doble"
                });
            }

            if(noDoseCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill
                {
                    PillId = 4,
                    Status = "Sin dosis"
                });
            }

            if(lateDoseCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill
                {
                    PillId = 5,
                    Status = "Tarde"
                });
            }

            return selectedPills;
        }

        private List<BirthControl> GetSelectedBirthControl()
        {
            List<BirthControl> selectedBirthControl = new List<BirthControl>();

            if (insertedRadioButton.IsChecked == true)
            {
                selectedBirthControl.Add( new BirthControl
                {
                    BirthControlId = 1,
                    Name = "Insertado"
                });
            }

            if (removedRadioButton.IsChecked == true)
            {
                selectedBirthControl.Add(new BirthControl
                {
                    BirthControlId = 2,
                    Name = "Removido"
                });
            }

            return selectedBirthControl;
        }

        private async void CreateNewCycleLog(NewCycleLog newCycleLog)
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
