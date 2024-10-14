using FlaUI.Core.AutomationElements;
using FlaUI.Core.Conditions;

namespace DataBaseGenerator.Test.Locators
{
    public class MainWindowStateLocators
    {

        static Window _window;
        static ConditionFactory _conditionFactory;

        public MainWindowStateLocators(Window window, ConditionFactory conditionFactory)
        {
            _window = window;
            _conditionFactory = conditionFactory;
        }

        public AutomationElement ConnectButton => _window.FindFirstDescendant(_conditionFactory.ByName("ConnectButton")).AsButton();
        public AutomationElement DialogWindow => _window.FindFirstDescendant(_conditionFactory.ByLocalizedControlType("диалоговое окно"));

        public AutomationElement AddIdPatieentTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("AddIdPatieentTextBox")).AsTextBox();
        public AutomationElement AddFamilyTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("AddFamilyTextBox")).AsTextBox();
        public AutomationElement AddNameTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("AddNameTextBox")).AsTextBox();
        public AutomationElement AddMiddleNameTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("AddMiddleNameTextBox")).AsTextBox();
        public AutomationElement AddAdressTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("AddAdressTextBox")).AsTextBox();
        public AutomationElement SelecedGenderComboBox => _window.FindFirstDescendant(_conditionFactory.ByName("SelecedGenderComboBox")).AsComboBox();
        public AutomationElement AddWorkPlaseTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("AddWorkPlaseTextBox")).AsTextBox();
        public AutomationElement AddInfoTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("AddInfoTextBox")).AsTextBox();
        public AutomationElement AddOnePatientButton => _window.FindFirstDescendant(_conditionFactory.ByName("AddOnePatientButton")).AsButton();
        public AutomationElement CancelAddPatientButton => _window.FindFirstDescendant(_conditionFactory.ByName("CancelAddPatientButton")).AsButton();
        public AutomationElement PatientCountTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("PatientCountTextBox")).AsTextBox();
        public AutomationElement AddPatientButton => _window.FindFirstDescendant(_conditionFactory.ByName("AddPatientButton")).AsButton();
        public AutomationElement RefreshPatientsButton => _window.FindFirstDescendant(_conditionFactory.ByName("RefreshPatientsButton")).AsButton();
        public AutomationElement DeleteFirstPatientButton => _window.FindFirstDescendant(_conditionFactory.ByName("DeleteFirstPatientButton")).AsButton();
        public AutomationElement DeleteAllPatientButton => _window.FindFirstDescendant(_conditionFactory.ByName("DeleteAllPatientButton")).AsButton();
        public AutomationElement DeleteAllTablesButton => _window.FindFirstDescendant(_conditionFactory.ByName("DeleteAllTablesButton")).AsButton();
        public AutomationElement WorkListCountTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("WorkListCountTextBox")).AsTextBox();
        public AutomationElement ModalityComboBox => _window.FindFirstDescendant(_conditionFactory.ByName("ModalityComboBox")).AsComboBox();
        public AutomationElement AeTitleBoxTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("AeTitleBoxTextBox")).AsTextBox();
        public AutomationElement AddWorkListButton => _window.FindFirstDescendant(_conditionFactory.ByName("AddWorkListButton")).AsButton();
        public AutomationElement RefreshWorkListButton => _window.FindFirstDescendant(_conditionFactory.ByName("RefreshWorkListButton")).AsButton();
        public AutomationElement DeleteFirstWorkListButton => _window.FindFirstDescendant(_conditionFactory.ByName("DeleteFirstWorkListButton")).AsButton();
        public AutomationElement DeleteAllWorkListButton => _window.FindFirstDescendant(_conditionFactory.ByName("DeleteAllWorkListButton")).AsButton();
        public AutomationElement UpdateTextTextBox => _window.FindFirstDescendant(_conditionFactory.ByName("UpdateTextTextBox")).AsTextBox();

    }
}
