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

            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Test Go To Patient Selecting State Failed");

                throw;
            }
            finally
            {
                _testClient.Kill();
            }
        }


    }
}
