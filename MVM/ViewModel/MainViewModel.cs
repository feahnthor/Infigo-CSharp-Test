using System;
using Infigo_api_sucks_solution.Core;

namespace Infigo_api_sucks_solution.MVM.ViewModel
{
    // Binds Usercontrol form to the main window
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewcommand { get; set; } // one that lets us switch between views

        public RelayCommand DiscoveryViewCommand { get; set; }
        public HomeViewModel HomeVM { get; set; }

        public DiscoveryViewModel DiscoveryVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { 
                _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            DiscoveryVM = new DiscoveryViewModel();

            CurrentView = HomeVM;

            HomeViewcommand = new RelayCommand(o => // Sets which View to show
            {
                CurrentView = HomeVM;
            });

            DiscoveryViewCommand = new RelayCommand(o =>
            {
                CurrentView = DiscoveryVM;
            });
        }
    }
}
