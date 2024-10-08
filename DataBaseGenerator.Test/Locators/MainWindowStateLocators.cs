using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
