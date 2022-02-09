using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using DataBaseGenerator.Core;
using DataBaseGenerator.Core.Data;
using DataBaseGenerator.Core.GeneratorRules.Patient;
using MySqlConnector;
using Prism.Commands;
using Prism.Mvvm;

namespace DataBaseGenerator.UI.Wpf
{
    public class MainViewModel : BindableBase
    {

        public MainViewModel()
        {

        }


        private PatientGeneratorParameters _patientGeneratorParameters;

        private MySqlConnection _myConnection;
        private MySqlCommand _mySqlCommand;
        private MySqlDataReader _dataReader;
        private MySqlDataAdapter _adapter;
        private DataTable _tablet;
        private string _connect = "Server=localhost;DataBase=medxregistry;Uid=root;pwd=root;";
        private string _updateText;




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


        private DelegateCommand _connectDB;
        public ICommand ConnectDB => _connectDB = new DelegateCommand(PerformConnectDB);

        private void PerformConnectDB()
        {
            try
            {
                _myConnection = new MySqlConnection(_connect);

                _myConnection.Open();

                UpdateText = "DB connect";

            }
            catch (Exception e)
            {
                UpdateText = "DB not connect";
            }
        }


        private DelegateCommand _selectDb;
        public ICommand SelectDb => _selectDb = new DelegateCommand(PerformRequest);

        private void PerformRequest()
        {
            try
            {
                _myConnection = new MySqlConnection(_connect);

                _myConnection.Open();

                _adapter = new MySqlDataAdapter();

                _mySqlCommand = new MySqlCommand($"INSERT INTO patient (ID_Patient, LastName, FirstName, MiddleName, PatientID, BirthDate, Sex, Address, AddInfo, Occupation) VALUES ('10', '3', '4', '6', 'MXR-000005', '1984-01-02', 'm', 'm', 'm', 'm');", _myConnection);

                _dataReader = _mySqlCommand.ExecuteReader();

                UpdateText = "Patients registered";

                while (_dataReader.Read())
                {
                    UpdateText = _dataReader[0].ToString();
                }

                _dataReader.Close();

                _myConnection.Close();
            }
            catch (Exception e)
            {
                UpdateText = "Patient in Base";
            }
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



        private DelegateCommand refreshPatients;
        public ICommand RefreshPatients => refreshPatients ??= new DelegateCommand(PerformRefreshPatients);

        private void PerformRefreshPatients()
        {
            AllPatients = DataBaseCommand.GetAllPatients();
            MainWindow.AllPatientView.ItemsSource = null;
            MainWindow.AllPatientView.Items.Clear();
            MainWindow.AllPatientView.ItemsSource = AllPatients;
            MainWindow.AllPatientView.Items.Refresh();
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
                    PatientCount = 150
                };

                var addPatient = DataBaseCommand.GenerateDateBase(newPatient);

                UpdateText = "Patient added";

            }

            catch (Exception e)
            {
                UpdateText = "Patient not added";
            }

            PerformRefreshPatients();

        }


        private DelegateCommand _deletePatient;
        public ICommand DeletePatient => _deletePatient ??= new DelegateCommand(PerformDeletePatient);

        private void PerformDeletePatient()
        {
            try
            {
                //Patient patient = new Patient();
                //patient.Id = 1;
                //patient.LastName = "";
                //patient.Name = "";

                //var deletePatient = DataBaseCommand.DeletePatient(patient);

                UpdateText = "Patient delete";
            }
            catch (Exception e)
            {
                UpdateText = "Patient not Deleted";
            }
        }


    }
}
