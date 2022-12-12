using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Data;
using System.Media;
using System.Windows;
using System.Windows.Input;
using DataBaseGenerator.Core;
using DataBaseGenerator.Core.Data;
using DataBaseGenerator.Core.GeneratorRules.Patient;
using DataBaseGenerator.Core.GeneratorRules.WorkList;
using MahApps.Metro.Actions;
using MySqlConnector;
using Prism.Commands;
using Prism.Mvvm;

namespace DataBaseGenerator.UI.Wpf
{
    public class MainViewModel : BindableBase
    {

        public MainViewModel()
        {
            var defaultAeTitle = new RandomModalityRule("DX");
            ModalityRules = new ObservableCollection<RandomModalityRule>
            {
                defaultAeTitle,
                new RandomModalityRule("MG")
            };

            SelectModality = defaultAeTitle;

        }


        private PatientGeneratorParameters _patientGeneratorParameters;
        private readonly RandomModalityRule _modalityRule;
        private MySqlConnection _myConnection;
        private MySqlCommand _mySqlCommand;
        private MySqlDataReader _dataReader;
        private MySqlDataAdapter _adapter;
        private DataTable _tablet;
        private string _connect = "Server=localhost;DataBase=medxregistry;Uid=root;pwd=root;";
        private string _updateText;
        private int _patientCount;
        private int _workListCount;
        private RandomModalityRule _modality;
        private string _aeTitle;
        DialogMessageWindow _dialogMessage = new DialogMessageWindow();





        public string UpdateText
        {
            get
            {
                return _updateText;
            }
            set
            {
                SetProperty(ref _updateText, value);
            }
        }


        public int SetPatientCount
        {
            get
            {
                return _patientCount;
            }

            set
            {
                SetProperty(ref _patientCount, value);
            }
        }


        public int SetWorkListCount
        {
            get
            {
                return _workListCount;
            }

            set
            {
                SetProperty(ref _workListCount, value);
            }
        }


        public ObservableCollection<RandomModalityRule> ModalityRules { get; }

        public RandomModalityRule SelectModality
        {
            get
            {
                return _modality;
            }

            set
            {
                SetProperty(ref _modality, value);
            }
        }


        public string SetAeTitle
        {
            get
            {
                return _aeTitle;
            }

            set
            {
                SetProperty(ref _aeTitle, value);
            }
        }



        private DelegateCommand _connectDB;
        public ICommand ConnectDB => _connectDB = new DelegateCommand(PerformConnectDB);

        private void PerformConnectDB()
        {
            MessageBox.Show("Ну да конечно, хахахах !!!");
            MessageBox.Show("Попробуй еще раз !!!");
            MessageBox.Show("Не останавливайся, ты уже так далеко зашел !!!");
            MessageBox.Show("У тебя все получится ;) !!!");
        }



        private List<Patient> _allPatients = DataBaseCommand.GetAllPatients();

        public List<Patient> AllPatients
        {
            get
            {
                return _allPatients;
            }
            set
            {
                SetProperty(ref _allPatients, value);
            }
        }


        private List<WorkList> _allWorkLists = DataBaseCommand.GetAllWorkLists();

        public List<WorkList> AllWorkLists
        {
            get
            {
                return _allWorkLists;
            }
            set
            {
                SetProperty(ref _allWorkLists, value);
            }
        }



        private DelegateCommand refreshPatients;
        public ICommand RefreshPatients => refreshPatients ??= new DelegateCommand(PerformRefreshPatients);

        private void PerformRefreshPatients()
        {
            AllPatients = DataBaseCommand.GetAllPatients();
            MainWindow.AllPatientView.ItemsSource = null;
            MainWindow.AllPatientView.Items.Clear();
            MainWindow.AllPatientView.ItemsSource = AllPatients;
            MainWindow.AllPatientView.Items.Refresh();
            UpdateText = "Patient table is update";
        }



        private DelegateCommand refreshWorkList;
        public ICommand RefreshWorkList => refreshWorkList ??= new DelegateCommand(PerformRefreshWorkList);

        private void PerformRefreshWorkList()
        {
            AllWorkLists = DataBaseCommand.GetAllWorkLists();
            MainWindow.AllWorkListView.ItemsSource = null;
            MainWindow.AllWorkListView.Items.Clear();
            MainWindow.AllWorkListView.ItemsSource = AllWorkLists;
            MainWindow.AllWorkListView.Items.Refresh();
            UpdateText = "WorkList table is update";
        }



        private DelegateCommand _addPatient;
        public ICommand AddPatient => _addPatient ??= new DelegateCommand(PerformAddPatient);

        private void PerformAddPatient()
        {
            try
            {
                var newPatient = new PatientGeneratorParameters(
                    new OrderIdPatientRule(),
                    new RandomLastNameRule(),
                    new RandomFirstNameRule(),
                    new RandomMiddleNameRule(),
                    new OrderPatientIdRule(),
                    new RandomBirthDateRule(new DateTime()),
                    new RandomSexRule(),
                    new RandomAddressRule(),
                    new RandomAddInfoRule(),
                    new RandomOccupationRule())
                {
                    PatientCount = _patientCount
                };

                var addPatient = DataBaseCommand.GeneratePatientDateBase(newPatient);

                PerformRefreshPatients();

                UpdateText = "Patient added";
            }

            catch (Exception e)
            {
                UpdateText = "Пациент не добавлен";
            }
        }


        private DelegateCommand _addWorkList;
        public ICommand AddWorkList => _addWorkList ??= new DelegateCommand(PerformAddWorkList);

        private void PerformAddWorkList()
        {
            try
            {
                var newWorkList = new WorkListGeneratorParameters(
                    new OrderIdWorklistRule(),
                    new RandomCreateDateRule(),
                    new RandomCreateTimeRule(),
                    new RandomCompleteDateRule(),
                    new RandomCompleteTimeRule(),
                    new OrderIdPatientWlRule(),
                    new RandomStateRule(),
                    new RandomSOPInstanceUIDRule(),
                    SelectModality,
                    new RandomStationAeTitleRule(_aeTitle),
                    new RandomProcedureStepStartDateTimeRule(),
                    new RandomPerformingPhysiciansNameRule(),
                    new RandomStudyDescriptionRule(),
                    new RandomReferringPhysiciansNameRule(),
                    new RandomRequestingPhysicianRule())
                {
                    WorkListCount = _workListCount
                };

                var addWorkList = DataBaseCommand.GenerateWorkListBase(newWorkList);

                PerformRefreshWorkList();

                UpdateText = "WorkList added";
            }

            catch (Exception e)
            {
                UpdateText = "WorkList not added";
            }
        }



        private DelegateCommand _deleteFirstPatient;
        public ICommand DeleteFirstPatient => _deleteFirstPatient ??= new DelegateCommand(PerformDeleteFirstPatient);

        private void PerformDeleteFirstPatient()
        {
            try
            {
                var patient = new PatientGeneratorParameters(
                   new OrderIdPatientRule(),
                   new RandomLastNameRule(),
                   new RandomFirstNameRule(),
                   new RandomMiddleNameRule(),
                   new OrderPatientIdRule(),
                   new RandomBirthDateRule(new DateTime()),
                   new RandomSexRule(),
                   new RandomAddressRule(),
                   new RandomAddInfoRule(),
                   new RandomOccupationRule())
                {
                    PatientCount = _patientCount
                };

                var deletePatient = DataBaseCommand.DeleteFirstPatient(patient);

                PerformRefreshPatients();

                UpdateText = "First Patient is Delete";
            }
            catch (Exception ex)
            {
                UpdateText = "Patient is not Deleted";
            }
        }


        private DelegateCommand _deleteAllPatient;
        public ICommand DeleteAllPatient => _deleteAllPatient ??= new DelegateCommand(PerformDeleteAllPatient);

        private void PerformDeleteAllPatient()
        {
            try
            {
                var patient = new PatientGeneratorParameters(
                   new OrderIdPatientRule(),
                   new RandomLastNameRule(),
                   new RandomFirstNameRule(),
                   new RandomMiddleNameRule(),
                   new OrderPatientIdRule(),
                   new RandomBirthDateRule(new DateTime()),
                   new RandomSexRule(),
                   new RandomAddressRule(),
                   new RandomAddInfoRule(),
                   new RandomOccupationRule())
                {
                    PatientCount = _patientCount
                };

                var deletePatient = DataBaseCommand.DeleteAllPatients(patient);

                PerformRefreshPatients();

                UpdateText = "Patient Table Deletion completed";
            }
            catch (Exception ex)
            {
                UpdateText = "Patient Table is not Deleted";
            }
        }


        private DelegateCommand _deleteFirstWorkList;
        public ICommand DeleteFirstWorkList => _deleteFirstWorkList ??= new DelegateCommand(PerformDeleteFirstWorkList);

        private void PerformDeleteFirstWorkList()
        {
            try
            {
                var workList = new WorkListGeneratorParameters(
                    new OrderIdWorklistRule(),
                    new RandomCreateDateRule(),
                    new RandomCreateTimeRule(),
                    new RandomCompleteDateRule(),
                    new RandomCompleteTimeRule(),
                    new OrderIdPatientWlRule(),
                    new RandomStateRule(),
                    new RandomSOPInstanceUIDRule(),
                    SelectModality,
                    new RandomStationAeTitleRule(_aeTitle),
                    new RandomProcedureStepStartDateTimeRule(),
                    new RandomPerformingPhysiciansNameRule(),
                    new RandomStudyDescriptionRule(),
                    new RandomReferringPhysiciansNameRule(),
                    new RandomRequestingPhysicianRule())
                {
                    WorkListCount = _workListCount
                };

                var deleteWorkList = DataBaseCommand.DeleteFirstWorkList(workList);

                PerformRefreshWorkList();

                UpdateText = "First in WorkList Delete";
            }
            catch (Exception ex)
            {
                UpdateText = "WorkList is not Deleted";
            }
        }


        private DelegateCommand _deleteAllWorkList;
        public ICommand DeleteAllWorkList => _deleteAllWorkList ??= new DelegateCommand(PerformDeleteAllWorkList);

        private void PerformDeleteAllWorkList()
        {
            try
            {
                var workList = new WorkListGeneratorParameters(
                    new OrderIdWorklistRule(),
                    new RandomCreateDateRule(),
                    new RandomCreateTimeRule(),
                    new RandomCompleteDateRule(),
                    new RandomCompleteTimeRule(),
                    new OrderIdPatientWlRule(),
                    new RandomStateRule(),
                    new RandomSOPInstanceUIDRule(),
                    SelectModality,
                    new RandomStationAeTitleRule(_aeTitle),
                    new RandomProcedureStepStartDateTimeRule(),
                    new RandomPerformingPhysiciansNameRule(),
                    new RandomStudyDescriptionRule(),
                    new RandomReferringPhysiciansNameRule(),
                    new RandomRequestingPhysicianRule())
                {
                    WorkListCount = _workListCount
                };

                var deletePatient = DataBaseCommand.DeleteAllWorkList(workList);

                PerformRefreshWorkList();

                UpdateText = "WorkList Table Deletion completed";
            }
            catch (Exception ex)
            {
                UpdateText = "WorkList is not Deleted";
            }
        }


        private DelegateCommand _deleteAllTables;
        public ICommand DeleteAllTables => _deleteAllTables ??= new DelegateCommand(PerformDeleteAllTables);

        private void PerformDeleteAllTables()
        {
            try
            {
                PerformDeleteAllPatient();
                PerformRefreshPatients();

                PerformDeleteAllWorkList();
                PerformRefreshWorkList();

                UpdateText = "All Tables Deletion completed";
            }
            catch (Exception ex)
            {
                UpdateText = "Tables is not Deleted";
            }
        }


        /// <summary>
        /// Dialog Window Commands
        /// </summary>



        private DelegateCommand _aboutProgram;
        public ICommand AboutProgram => _aboutProgram = new DelegateCommand(InformationMessage);

        private void InformationMessage()
        {
            _dialogMessage.DataContext = this;
            _dialogMessage.ShowDialog();
        }



        private DelegateCommand _dialog;
        public ICommand ClosingDialogWindow => _dialog = new DelegateCommand(ClosingDialog);

        private void ClosingDialog()
        {
            //SoundPlayer soundPlayer = new SoundPlayer("C:\\Temp\\nizza.wav");
            //soundPlayer.Load();
            //soundPlayer.PlaySync();
            _dialogMessage.Close();
        }

        

        private DelegateCommand _hotkey;
        public ICommand HotkeyForDialogWindow => _hotkey = new DelegateCommand(HotkeyForDialog);

        private void HotkeyForDialog()
        {
            MessageBox.Show("Отличная попытка ДРУЖИЩЕ");
        }




    }
}
