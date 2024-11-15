using EngineerTracker.Models.Appointments;

namespace EngineerTracker.Services.Interfaces
{
    public interface INavigationService
    {
        Task NavigateTo(string endpoint);
        Task NavigateTo(string endpoint, object parameter);
        Task GoBack();
    }
}