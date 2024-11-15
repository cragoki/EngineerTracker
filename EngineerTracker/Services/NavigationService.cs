using EngineerTracker.Models.Appointments;
using EngineerTracker.Services.Interfaces;

namespace EngineerTracker.Services
{
    public class NavigationService : INavigationService
    {

        public NavigationService() 
        {
        }

        public async Task NavigateTo(string endpoint)
        {
            await Shell.Current.GoToAsync(endpoint);
        }

        public async Task NavigateTo(string endpoint, object parameter)
        {
            var query = new Dictionary<string, object>
            {
                { "data", parameter }
            };

            await Shell.Current.GoToAsync(endpoint, query);
        }
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
