using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBaseGenerator.Test.Core;
using DataBaseGenerator.Test.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataBaseGenerator.Test
{
    [TestClass]
    [DoNotParallelize]
    public class AutoTestSuite
    {
        private readonly NLog.ILogger _logger = NLog.LogManager.GetCurrentClassLogger();
        private ITestClient _testClient;

        private string _pathToTestClient = "D:\\C#\\DataBaseGenerator\\DataBaseGenerator\\bin\\Debug\\net8.0-windows\\DataBaseGenerator.UI.Wpf.exe";

        public AutoTestSuite()
        {
            _testClient = new DataBaseTestClient(_pathToTestClient);
        }


        [TestMethod]
        public async Task TestStartTestClient()
        {
            try
            {
                _logger.Info("Entering in Test Start test client");

                var menuState = await _testClient.StartAsync(TimeSpan.FromSeconds(30));
                Assert.IsNotNull(menuState);

                var openWindow = await menuState.GoToStateAsync("MainWindowState", TimeSpan.FromSeconds(30));

                var window = openWindow.GetMainWindow();

                if (openWindow is MainWindowState mainWindowState)
                {
                    mainWindowState.ClickConnectButton();

                    bool openDialogWindow = mainWindowState.CheckDialogWindowOpen();

                    Assert.IsTrue(openDialogWindow);

                    mainWindowState.CloseDialogWindow();

                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Test Go To Main Window State Failed");
                throw;
            }
            finally
            {
                _testClient.Kill();
            }
        }



        [TestMethod]
        public async Task TestAddPatientButton()
        {
            try
            {
                _logger.Info("Entering in Test Add Patient Button");

                var menuState = await _testClient.StartAsync(TimeSpan.FromSeconds(30));
                Assert.IsNotNull(menuState);

                var openWindow = await menuState.GoToStateAsync("MainWindowState", TimeSpan.FromSeconds(30));

                var window = openWindow.GetMainWindow();

                if (openWindow is MainWindowState mainWindowState)
                {

                    mainWindowState.ClickDeleteAllPatientButton();
                    mainWindowState.InputPatientCountTextBox();
                    mainWindowState.ClickAddPatientButton();

                    int patientCount = mainWindowState.GetViewAllPatientTableRowCount();

                    Assert.AreEqual(7, patientCount);

                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Test Add Patient Button Failed");
                throw;
            }
            finally
            {
                _testClient.Kill();
            }

        }

        [TestMethod]
        public async Task TestDeleteAllButton()
        {
            try
            {
                _logger.Info("Entering in Test Delete All Button");

                var menuState = await _testClient.StartAsync(TimeSpan.FromSeconds(30));
                Assert.IsNotNull(menuState);

                var openWindow = await menuState.GoToStateAsync("MainWindowState", TimeSpan.FromSeconds(30));

                var window = openWindow.GetMainWindow();

                if (openWindow is MainWindowState mainWindowState)
                {


                    bool isTableEmpty = mainWindowState.IsViewAllPatientTableEmpty();

                    if (isTableEmpty)
                    {
                        mainWindowState.InputPatientCountTextBox();
                        mainWindowState.ClickAddPatientButton();
                        mainWindowState.ClickDeleteAllPatientButton();

                    }

                    else
                    {
                        mainWindowState.ClickDeleteAllPatientButton();
                    }

                    Assert.AreEqual(0, mainWindowState.GetViewAllPatientTableRowCount());

                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Test Delete All Button Failed");
                throw;
            }
            finally
            {
                _testClient.Kill();
            }

        }


        [TestMethod]
        public async Task TestAddWorklistButton()
        {
            try
            {
                _logger.Info("Entering in Test Add Worklist Button");

                var menuState = await _testClient.StartAsync(TimeSpan.FromSeconds(30));
                Assert.IsNotNull(menuState);

                var openWindow = await menuState.GoToStateAsync("MainWindowState", TimeSpan.FromSeconds(30));

                var window = openWindow.GetMainWindow();

                if (openWindow is MainWindowState mainWindowState)
                {

                    mainWindowState.ClickDeleteAllWorkListButton();
                    mainWindowState.InputAeTitleBoxTextBox();
                    mainWindowState.InputWorkListCountTextBox();
                    mainWindowState.ClickAddWorklistButton();

                    var worklistCount = mainWindowState.GetViewAllWorkListTableRowCount();


                    Assert.AreEqual(5, worklistCount);

                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Test Add Worklist Button Failed");
                throw;
            }
            finally
            {
                _testClient.Kill();
            }

        }

        [TestMethod]
        public async Task TestDeleteAllWlButton()
        {
            try
            {
                _logger.Info("Entering in Test Delete All Wl Button");

                var menuState = await _testClient.StartAsync(TimeSpan.FromSeconds(30));
                Assert.IsNotNull(menuState);

                var openWindow = await menuState.GoToStateAsync("MainWindowState", TimeSpan.FromSeconds(30));

                var window = openWindow.GetMainWindow();

                if (openWindow is MainWindowState mainWindowState)
                {


                    bool isTableEmpty = mainWindowState.IsViewAllWorkListTableEmpty();

                    if (isTableEmpty)
                    {
                        mainWindowState.InputAeTitleBoxTextBox();
                        mainWindowState.InputWorkListCountTextBox();
                        mainWindowState.ClickAddWorklistButton();
                        mainWindowState.ClickDeleteAllWorkListButton();

                    }

                    else
                    {
                        mainWindowState.ClickDeleteAllWorkListButton();
                    }

                    Assert.AreEqual(0, mainWindowState.GetViewAllWorkListTableRowCount());

                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Test Delete All Wl Button Failed");
                throw;
            }
            finally
            {
                _testClient.Kill();
            }

        }

        [TestMethod]
        public async Task TestDeleteAllTablesButton()
        {
            try
            {
                _logger.Info("Entering in Test Delete All Wl Button");

                var menuState = await _testClient.StartAsync(TimeSpan.FromSeconds(30));
                Assert.IsNotNull(menuState);

                var openWindow = await menuState.GoToStateAsync("MainWindowState", TimeSpan.FromSeconds(30));

                var window = openWindow.GetMainWindow();

                if (openWindow is MainWindowState mainWindowState)
                {

                    bool isPatientTableEmpty = mainWindowState.IsViewAllPatientTableEmpty();
                    bool isWorkListTableEmpty = mainWindowState.IsViewAllWorkListTableEmpty();


                    if (isPatientTableEmpty)
                    {
                        mainWindowState.InputPatientCountTextBox();
                        mainWindowState.ClickAddPatientButton();

                    }
                    if (isWorkListTableEmpty)
                    {
                        mainWindowState.InputAeTitleBoxTextBox();
                        mainWindowState.InputWorkListCountTextBox();
                        mainWindowState.ClickAddWorklistButton();
                    }


                    mainWindowState.ClickDeleteAllTablesButton();

                    int rowPatientCount = mainWindowState.GetViewAllPatientTableRowCount();
                    int rowWorklistCount = mainWindowState.GetViewAllWorkListTableRowCount();
                   

                    Assert.AreEqual(0, rowPatientCount + rowWorklistCount);


                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Test Delete All Wl Button Failed");
                throw;
            }
            finally
            {
                _testClient.Kill();
            }

        }

    }

}
