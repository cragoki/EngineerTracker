using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EngineerAdmin.Constants;
using EngineerTracker.Clients;
using EngineerTracker.Models.Appointments;
using EngineerTracker.Services.Interfaces;
using System.Collections.ObjectModel;

namespace EngineerTracker.ViewModels
{
    public partial class EngineerAppointmentsViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<AppointmentModel> appointments;

        private readonly ApiClient _apiClient;
        private readonly INavigationService _navigationService;

        public EngineerAppointmentsViewModel(ApiClient apiClient, INavigationService navigationService)
        {
            _navigationService = navigationService;
            _apiClient = apiClient;
            Appointments = new ObservableCollection<AppointmentModel>();

            LoadAppointments();
        }

        private async void LoadAppointments()
        {
            try
            {
                var appointmentList = await _apiClient.GetAsync<List<AppointmentModel>>(ApiEndpoints.GetEngineerAppointments + "?engineerId=1"); // Hardcoding the Engineer Id of 1, until I get around to a Login page
                var todaysAppointments = appointmentList.Where(x => x.Date == DateTime.Now.Date).ToList();


                foreach (var appointment in todaysAppointments)
                {
                    //Interestingly, this needs to be referenced as upper case, opposed to the original appointments
                    //As appointments contains the data, but not the framework for  INotifyPropertyChanged
                    Appointments.Add(appointment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($" {ex.Message}");
            }
        }

        [RelayCommand]
        async Task ViewAppointment(AppointmentModel appointment)
        {
            await _navigationService.NavigateTo(nameof(ViewAppointment), appointment);
        }


    }
}
