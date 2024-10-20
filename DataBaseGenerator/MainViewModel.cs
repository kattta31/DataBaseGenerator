﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using DataBaseGenerator.Core;
using DataBaseGenerator.Core.Data;
using DataBaseGenerator.Core.GeneratorRules.Patient;
using DataBaseGenerator.Core.GeneratorRules.WorkList;
using MySqlConnector;
using Prism.Commands;

namespace DataBaseGenerator.UI.Wpf
{
    public partial class MainViewModel : ObservableObject
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

            Gender = new List<string> { "Man", "Female", "Other" };
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
        private string _gender;
        private string _addIdPatieent;
        private string _addFamily;
        private string _addName;
        private string _addMiddleName;
        private string _addAdress;
        private string _addWorkPlase;
        private string _addInfo;


        [ObservableProperty]
        private string _pathToResourceForDialogMessage = "D:\\Develop\\DataBaseGenerator\\DataBaseGenerator.Core\\Resources\\333.jpg";
        //private string _pathToResourceForDialogMessage = "C:\\Program Files (x86)\\DBGeneratorBroken\\Resources\\333.jpg";

        [ObservableProperty]
        private string _pathToResourceForSpecificationWindow = "D:\\Develop\\DataBaseGenerator\\DataBaseGenerator.Core\\Resources\\Specification.jpg";
        //private string _pathToResourceForSpecificationWindow = "C:\\Program Files (x86)\\DBGeneratorBroken\\Resources\\Specification.jpg";

        [ObservableProperty]
        private string _pathToIcon = ".\\Sources\\DBGenerator.ico";
        //private string _pathToIcon = "C:\\Program Files (x86)\\DBGeneratorBroken\\Resources\\DBGenerator.ico";





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


        public string AddIdPatieent
        {
            get => _addIdPatieent;
            set
            {
                SetProperty(ref _addIdPatieent, value);
            }
        }

        public string AddFamily
        {
            get => _addFamily;
            set
            {
                SetProperty(ref _addFamily, value);
            }
        }

        public string AddName
        {
            get => _addName;
            set
            {
                SetProperty(ref _addName, value);
            }
        }

        public string AddMiddleName
        {
            get => _addMiddleName;
            set
            {
                SetProperty(ref _addMiddleName, value);
            }
        }


        public List<string> Gender { get; }

        public string SelecedGender
        {
            get => _gender;

            set
            {
                if (value == Gender[0])
                {
                    value = "M";
                }
                else if (value == Gender[1])
                {
                    value = "F";
                }
                else if (value == Gender[2])
                {
                    value = "O";
                }
                SetProperty(ref _gender, value);
            }
        }

        public string AddAdress
        {
            get => _addAdress;
            set
            {
                SetProperty(ref _addAdress, value);
            }
        }

        public string AddWorkPlase
        {
            get => _addWorkPlase;
            set
            {
                SetProperty(ref _addWorkPlase, value);
            }
        }

        public string AddInfo
        {
            get => _addInfo;
            set
            {
                SetProperty(ref _addInfo, value);
            }
        }

        [ObservableProperty]
        public DateTime _patientBirthDate = DateTime.Today.AddYears(-100);


        private DelegateCommand _connectDB;
        public ICommand ConnectDB => _connectDB = new DelegateCommand(PerformConnectDB);

        private void PerformConnectDB()
        {
            MessageBox.Show("Ну да конечно, хахахах !!!");
            MessageBox.Show("Попробуй еще раз !!!");
            MessageBox.Show("Не останавливайся, ты уже так далеко зашел !!!");
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
                UpdateText = "Patient not added";
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


        private DelegateCommand _cancelAddPatient;
        public ICommand CancelAddPatient => _cancelAddPatient ??= new DelegateCommand(PerformCancelAddPatient);

        private void PerformCancelAddPatient()
        {
            AddIdPatieent = string.Empty;

            AddFamily = string.Empty;

            AddName = string.Empty;

            AddMiddleName = string.Empty;

            AddAdress = string.Empty;

            AddWorkPlase = string.Empty;

            AddInfo = string.Empty;

            SelecedGender = null;

            UpdateText = "Как скажете, отменяю !";
        }

        private DelegateCommand _addOnePatient;
        public ICommand AddOnePatient => _addOnePatient ??= new DelegateCommand(PerformAddOnePatient);

        private void PerformAddOnePatient()
        {
            try
            {
                var newPatient = new PatientInputParameters(
                    1,
                    AddFamily,
                    AddName,
                    AddMiddleName,
                    AddIdPatieent,
                    PatientBirthDate,
                    SelecedGender,
                    AddAdress,
                    AddInfo,
                    AddWorkPlase)
                {
                    PatientCount = _patientCount
                };

                var addPatient = DataBaseCommand.AddPatientInDateBase(newPatient);

                PerformRefreshPatients();

                CleareFields();

                UpdateText = "Patient added";
            }
            catch (Exception e)
            {
                UpdateText = "Пациент не добавлен";
            }
        }

        private void CleareFields()
        {
            AddIdPatieent = string.Empty;
            AddFamily = string.Empty;
            AddName = string.Empty;
            AddMiddleName = string.Empty;
            SelecedGender = null;
            AddAdress = string.Empty;
            AddWorkPlase = string.Empty;
            AddInfo = string.Empty;
        }
    }
}
