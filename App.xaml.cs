using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;


namespace Infigo_api_sucks_solution
{
    /// <summary>
    /// Interaction logic for App.xaml, functions created here are direct references to APP.xaml
    /// </summary>
    public partial class App : Application // Method sthat affect the ENTIRE applicatoin, such as computer shutdown events, and application closing
    {
        // 
        private void App_SessionEnding(object sender, SessionEndingCancelEventArgs e) // References "App_SessionEnding in the App.xaml, prompts user on system restart, or shutdownw https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.sessionending?view=windowsdesktop-6.0
        {
            // Ask the user if they want to allow the session to end
            string msg = $"{e.ReasonSessionEnding}. End session?";
            MessageBoxResult result = MessageBox.Show(msg, "Session Ending", MessageBoxButton.YesNo);

            // End session, if specified
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }
    }
}
