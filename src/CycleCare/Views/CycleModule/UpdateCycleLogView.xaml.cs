using CycleCare.Models;
using CycleCare.Service;
using CycleCare.Utilities;
using CycleCare.Views.CycleModule;
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
    /// <summary>
    /// Interaction logic for UpdateCycleLogView.xaml
    /// </summary>
    public partial class UpdateCycleLogView : Page
    {
        private List<StackPanel> _moodsSelected = new List<StackPanel>();
        private StackPanel _selectedMenstrualFlowPanel = null;
        private StackPanel _selectedVaginalFlowPanel = null;
        private string _currentDate;

        public UpdateCycleLogView(CycleLog cycleLog)
        {
            InitializeComponent();
            _currentDate = cycleLog.CreationDate.ToString("dd/MM/yyyy");
            SetCycleLogData(cycleLog);
            tvDate.Text = _currentDate;
        }

        private void SetCycleLogData(CycleLog cycleLog)
        {
            FillHoursComboBox(cycleLog.SleepHours);

            txtNote.Text = cycleLog.Note;

            SetSelectedMenstrualFlow(cycleLog.MenstrualFlow.MenstrualFlowId);

            SetSelectedVaginalFlow(cycleLog.VaginalFlow.VaginalFlowId);

            SetSelectedSymptoms(cycleLog.Symptoms);

            SetSelectedMoods(cycleLog.Moods);

            SetSelectedMedications(cycleLog.Medications);

            SetSelectedPills(cycleLog.Pills);

            SetSelectedBirthControls(cycleLog.BirthControl);
        }

        private void FillHoursComboBox(int sleepHours)
        {
            for (int i = 1; i <= 24; i++)
            {
                sleepHoursComboBox.Items.Add(i);
            }

            sleepHoursComboBox.SelectedItem = sleepHours;
        }

        private void SetSelectedMenstrualFlow(int menstrualFlowId)
        {
            List<StackPanel> menstrualFlowPanels = new List<StackPanel> { lightFlowPanel, mediumFlowPanel, heavyFlowPanel };
            StackPanel selectedPanel = GetMenstrualFlowPanelById(menstrualFlowId);

            if (selectedPanel != null)
            {
                foreach (var panel in menstrualFlowPanels)
                {
                    panel.Background = (panel == selectedPanel) ? Brushes.LightGray : Brushes.White;
                }
            }
        }

        private StackPanel GetMenstrualFlowPanelById(int menstrualFlowId)
        {
            List<StackPanel> menstrualFlowPanels = new List<StackPanel> { lightFlowPanel, mediumFlowPanel, heavyFlowPanel };
            return menstrualFlowPanels.FirstOrDefault(panel => panel.Tag.ToString() == menstrualFlowId.ToString());
        }


        private void SetSelectedVaginalFlow(int vaginalFlowId)
        {
            List<StackPanel> vaginalFlowPanels = new List<StackPanel> { dryFlowStackPanel, stickyFlowStackPanel, creamyFlowStackPanel, aqueousFlowStackPanel, elasticFlowStackPanel };
            StackPanel selectedPanel = GetVaginalFlowPanelById(vaginalFlowId);

            if (selectedPanel != null)
            {
                foreach (var panel in vaginalFlowPanels)
                {
                    panel.Background = (panel == selectedPanel) ? Brushes.LightGray : Brushes.White;
                }
            }
        }
        private StackPanel GetVaginalFlowPanelById(int vaginalFlowId)
        {
            List<StackPanel> vaginalFlowPanels = new List<StackPanel> { dryFlowStackPanel, stickyFlowStackPanel, creamyFlowStackPanel, aqueousFlowStackPanel, elasticFlowStackPanel };
            return vaginalFlowPanels.FirstOrDefault(panel => panel.Tag.ToString() == vaginalFlowId.ToString());
        }


        private void SetSelectedSymptoms(List<Symptom> symptoms)
        {
            foreach (var symptom in symptoms)
            {
                switch (symptom.SymptomId)
                {
                    case 1:
                        abdominalPainCheckBox.IsChecked = true;
                        break;
                    case 2:
                        breastTendernessCheckBox.IsChecked = true;
                        break;
                    case 3:
                        acneCheckBox.IsChecked = true;
                        break;
                    case 4:
                        bloatingCheckBox.IsChecked = true;
                        break;
                    case 5:
                        fatigueCheckBox.IsChecked = true;
                        break;
                    case 6:
                        headacheCheckBox.IsChecked = true;
                        break;
                    case 7:
                        nauseaCheckBox.IsChecked = true;
                        break;
                    case 8:
                        dizzinessCheckBox.IsChecked = true;
                        break;
                    case 9:
                        cravingsCheckBox.IsChecked = true;
                        break;
                    case 10:
                        constipationCheckBox.IsChecked = true;
                        break;
                    case 11:
                        diarrheaCheckBox.IsChecked = true;
                        break;
                    case 12:
                        vaginalItchingCheckBox.IsChecked = true;
                        break;
                    case 13:
                        vulvaPainCheckBox.IsChecked = true;
                        break;
                }
            }
        }

        private void SetSelectedMoods(List<Mood> moods)
        {
            foreach (var mood in moods)
            {
                StackPanel moodPanel = GetMoodPanelById(mood.MoodId);
                _moodsSelected.Add(moodPanel);
                moodPanel.Background = new SolidColorBrush(Colors.LightGray);
            }
        }

        private StackPanel GetMoodPanelById(int moodId)
        {
            List<StackPanel> moodPanels = new List<StackPanel>{
                happyMoodStackPanel,
                sadMoodStackPanel,
                anxiousMoodStackPanel,
                angryMoodStackPanel,
                tiredMoodStackPanel,
                energeticMoodStackPanel,
                relaxedMoodStackPanel,
                focusedMoodStackPanel,
                stressedMoodStackPanel,
                irritatedMoodStackPanel
            };

            return moodPanels.FirstOrDefault(panel => panel.Tag.ToString() == moodId.ToString());
        }


        private void SetSelectedMedications(List<Medication> medications)
        {
            foreach (var medication in medications)
            {
                switch (medication.MedicationId)
                {
                    case 1:
                        hormoneTherapyCheckBox.IsChecked = true;
                        break;
                    case 2:
                        emergencyPillCheckBox.IsChecked = true;
                        break;
                    case 3:
                        painkillersCheckBox.IsChecked = true;
                        break;
                    case 4:
                        antidepressantsCheckBox.IsChecked = true;
                        break;
                    case 5:
                        antibioticsCheckBox.IsChecked = true;
                        break;
                    case 6:
                        antihistaminesCheckBox.IsChecked = true;
                        break;
                }
            }
        }

        private void SetSelectedPills(List<Pill> pills)
        {
            foreach (var pill in pills)
            {
                switch (pill.PillId)
                {
                    case 1:
                        pillTakenCheckBox.IsChecked = true;
                        break;
                    case 2:
                        pillForgottenCheckBox.IsChecked = true;
                        break;
                    case 3:
                        doubleDoseCheckBox.IsChecked = true;
                        break;
                    case 4:
                        noDoseCheckBox.IsChecked = true;
                        break;
                    case 5:
                        lateDoseCheckBox.IsChecked = true;
                        break;
                }
            }
        }

        private void SetSelectedBirthControls(List<BirthControl> birthControls)
        {
            foreach (var birthControl in birthControls)
            {
                switch (birthControl.BirthControlId)
                {
                    case 1:
                        insertedRadioButton.IsChecked = true;
                        break;
                    case 2:
                        removedRadioButton.IsChecked = true;
                        break;
                }
            }
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
            newCycleLog.CreationDate = _currentDate.ToString();
            if (sleepHoursComboBox.SelectedItem != null)
            {
                newCycleLog.SleepHours = int.Parse(sleepHoursComboBox.SelectedItem.ToString());
            }

            newCycleLog.Note = txtNote.Text;

            if (_selectedMenstrualFlowPanel != null)
            {
                newCycleLog.MenstrualFlowId = GetMenstrualFlowId();
            }

            if (_selectedVaginalFlowPanel != null)
            {
                newCycleLog.VaginalFlowId = GetVaginalFlowId();
            }

            newCycleLog.Symptoms = GetSelectedSymptoms();

            newCycleLog.Moods = GetSelectedMoods();

            newCycleLog.Medications = GetSelectedMedications();

            newCycleLog.Pills = GetSelectedPills();

            newCycleLog.BirthControls = GetSelectedBirthControl();

            UpdateCycleLog(newCycleLog);
        }

        private int GetVaginalFlowId()
        {
            string selectedVaginalFlowId = _selectedVaginalFlowPanel.Tag.ToString();
            return int.Parse(selectedVaginalFlowId);
        }

        private int GetMenstrualFlowId()
        {
            string selectedMenstrualFlowId = _selectedMenstrualFlowPanel.Tag.ToString();
            return int.Parse(selectedMenstrualFlowId);
        }

        private List<Symptom> GetSelectedSymptoms()
        {
            List<Symptom> selectedSymptoms = new List<Symptom>();

            if (abdominalPainCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 1, Name = "Calambres" });
            }
            if (breastTendernessCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 2, Name = "Sensibilidad en los senos" });
            }
            if (acneCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 3, Name = "Acné" });
            }
            if (bloatingCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 4, Name = "Hinchazón" });
            }
            if (fatigueCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 5, Name = "Fatiga" });
            }
            if (headacheCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 6, Name = "Dolor de cabeza" });
            }
            if (nauseaCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 7, Name = "Náuseas" });
            }
            if (dizzinessCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 8, Name = "Mareos" });
            }
            if (cravingsCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 9, Name = "Antojos" });
            }
            if (constipationCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 10, Name = "Estreñimiento" });
            }
            if (diarrheaCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 11, Name = "Diarrea" });
            }
            if (vaginalItchingCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 12, Name = "Picazón vaginal" });
            }
            if (vulvaPainCheckBox.IsChecked == true)
            {
                selectedSymptoms.Add(new Symptom { SymptomId = 13, Name = "Dolor en vulva" });
            }

            return selectedSymptoms;
        }

        private List<Mood> GetSelectedMoods()
        {
            List<Mood> selectedMoods = new List<Mood>();

            foreach (StackPanel moodPanel in _moodsSelected)
            {
                selectedMoods.Add(new Mood
                {
                    MoodId = int.Parse(moodPanel.Tag.ToString())
                });
            }

            return selectedMoods;
        }

        private List<Medication> GetSelectedMedications()
        {
            List<Medication> selectedMedications = new List<Medication>();

            if (hormoneTherapyCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication { MedicationId = 1, Name = "Terapia hormonal" });
            }
            if (emergencyPillCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication { MedicationId = 2, Name = "Pastilla de emergencia" });
            }
            if (painkillersCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication { MedicationId = 3, Name = "Analgésicos" });
            }
            if (antidepressantsCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication { MedicationId = 4, Name = "Antidepresivos" });
            }
            if (antibioticsCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication { MedicationId = 5, Name = "Antibióticos" });
            }
            if (antihistaminesCheckBox.IsChecked == true)
            {
                selectedMedications.Add(new Medication { MedicationId = 6, Name = "Antihistamínicos" });
            }

            return selectedMedications;
        }

        private List<Pill> GetSelectedPills()
        {
            List<Pill> selectedPills = new List<Pill>();

            if (pillTakenCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill { PillId = 1, Status = "Tomada" });
            }
            if (pillForgottenCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill { PillId = 2, Status = "Olvidada" });
            }
            if (doubleDoseCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill { PillId = 3, Status = "Dosis Doble" });
            }
            if (noDoseCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill { PillId = 4, Status = "Sin Dosis" });
            }
            if (lateDoseCheckBox.IsChecked == true)
            {
                selectedPills.Add(new Pill { PillId = 5, Status = "Tarde" });
            }

            return selectedPills;
        }

        private List<BirthControl> GetSelectedBirthControl()
        {
            List<BirthControl> selectedBirthControls = new List<BirthControl>();

            if (insertedRadioButton.IsChecked == true)
            {
                selectedBirthControls.Add(new BirthControl { BirthControlId = 1, Name = "Insertado" });
            }
            if (removedRadioButton.IsChecked == true)
            {
                selectedBirthControls.Add(new BirthControl { BirthControlId = 2, Name = "Removido" });
            }

            return selectedBirthControls;
        }

        private async void UpdateCycleLog(NewCycleLog newCycleLog)
        {
            Response response = await CycleService.UpdateCycleLog(newCycleLog);
            if (response.Code == (int)HttpStatusCode.Created)
            {
                DialogManager.ShowSuccessMessageBox("Recordatorio actualizado exitosamente");
            }
            else
            {
                DialogManager.ShowErrorMessageBox("Error al actualizar el recordatorio. Por favor, intente nuevamente.");
            }
        }

        private void Mood_Click(object sender, MouseButtonEventArgs e)
        {
            StackPanel selectedPanel = sender as StackPanel;
            UpdateMoodSelection(selectedPanel);
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
    }
}