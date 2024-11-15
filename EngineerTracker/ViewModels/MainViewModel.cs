using CommunityToolkit.Mvvm.ComponentModel;
using EngineerTracker.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;
namespace EngineerTracker.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly INavigationService _navigationService;
        public MainViewModel(INavigationService navigationService) 
        {
            _navigationService = navigationService;
        }

        [RelayCommand]
        async Task EngineerAppointments() 
        {
            await _navigationService.NavigateTo(nameof(EngineerAppointments));
        }
    }
}
