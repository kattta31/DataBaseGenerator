using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DataBaseGenerator.UI.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                base.OnStartup(e);

                var mainWindow = new MainWindow();
                mainWindow.DataContext = new MainViewModel();
                mainWindow.Show();
            }

            catch(Exception ex)
            {
                MessageBox.Show("Check your connection settings !!!");                
                base.Shutdown();
            }

        }

    }
}
