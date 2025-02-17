using System.Windows;
using WpfApp1.Views;
using WpfApp1.Services;



namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var databaseService = new DatabaseService();
            var LoginWindow = new LoginWindow(databaseService);
            LoginWindow.Show();

        }

    }
}
