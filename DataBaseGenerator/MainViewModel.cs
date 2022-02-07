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

        public MySqlConnection myConnection;
        public MySqlCommand mySqlCommand;
        public MySqlDataReader dataReader;
        private MySqlDataAdapter adapter;
        private DataTable tablet;
        public string connect = "Server=localhost;DataBase=medxregistry;Uid=root;pwd=root;";
        private string _updateText;
        private int ID_Patient;


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


        private DelegateCommand connectDB;
        public ICommand ConnectDB => connectDB = new DelegateCommand(PerformConnectDB);

        private void PerformConnectDB()
        {
            try
            {
                myConnection = new MySqlConnection(connect);

                myConnection.Open();

                UpdateText = "DB connect";

            }
            catch (Exception e)
            {
                UpdateText = "DB not connect";
            }
        }


        private DelegateCommand selectDb;
        public ICommand SelectDb => selectDb = new DelegateCommand(PerformRequest);

        private void PerformRequest()
        {
            try
            {
                myConnection = new MySqlConnection(connect);

                myConnection.Open();

                adapter = new MySqlDataAdapter();

                mySqlCommand = new MySqlCommand($"INSERT INTO patient (ID_Patient, LastName, FirstName, MiddleName, PatientID, BirthDate, Sex, Address, AddInfo, Occupation) VALUES ('10', '3', '4', '6', 'MXR-000005', '1984-01-02', 'm', 'm', 'm', 'm');", myConnection);

                dataReader = mySqlCommand.ExecuteReader();

                UpdateText = "Patients registered";

                while (dataReader.Read())
                {
                    UpdateText = dataReader[0].ToString();
                }

                dataReader.Close();

                myConnection.Close();
            }
            catch (Exception e)
            {
                UpdateText = "Patient in Base";
            }
        }



        private List<Patient> allPatients = DataBaseCommand.GetAllPatients();

        public List<Patient> AllPatients
        {
            get
            {
                return allPatients;
            }
            set
            {
                SetProperty(ref allPatients, value);
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


        //НАДО ПРОВЕРИТЬ - ВАРИАНТ , ЛИБО ТАК ЛИБО ТАК!!!
        private RandomLastNameRule _lastName;

        private DelegateCommand addPatient;
        public ICommand AddPatient => addPatient ??= new DelegateCommand(PerformAddPatient);

        private void PerformAddPatient()
        {
            try
            {
                //НАДО ПРОВЕРИТЬ - ВАРИАНТ , ЛИБО ТАК ЛИБО ТАК!!!

                var patient = new PatientGenerator();

                var newPatient = new PatientGeneratorParameters(
                    new OrderIdPatientRule(),
                    new RandomLastNameRule(),
                    new RandomFirstNameRule(),
                    new RandomMiddleNameRule(),
                    new RandomBirthDateRule(new DateTime()),
                    new RandomSexRule(),
                    new RandomAddressRule(),
                    new RandomAddInfoRule(),
                    new RandomOccupationRule());

                

                //var addPatient = DataBaseCommand.CreatePatient(1, "Vasia", "Pupkin", "Olegich", "MXR-0004",
                //    new DateTime(1985, 01, 01), "M", "Minsk", "No comments", "Engineer");

                var addPatient = DataBaseCommand.CreatePatient(newPatient.ID_Patient.Generate(000019), newPatient.LastName.Generate(), 
                    newPatient.FirstName.Generate(), newPatient.MiddleName.Generate(), newPatient.BirthDate.Generate(), newPatient.Sex.Generate(),
                    newPatient.Address.Generate(),newPatient.AddInfo.Generate(),newPatient.Occupation.Generate());

                UpdateText = "Patient added";

            }

            catch (Exception e)
            {
                UpdateText = "Patient not added";
            }

        }

        private DelegateCommand deletePatient;
        public ICommand DeletePatient => deletePatient ??= new DelegateCommand(PerformDeletePatient);

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
