using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EngineerAdmin.Constants;
using EngineerTracker.Clients;
using EngineerTracker.Models.Appointments;
using EngineerTracker.Models.Shared;
using EngineerTracker.Services.Interfaces;

namespace EngineerTracker.ViewModels
{
    public partial class ViewAppointmentViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private AppointmentModel appointment;

        private readonly ApiClient _apiClient;
        private readonly INavigationService _navigationService;
        private List<KeyValueModel> keyValueModels = new List<KeyValueModel>();

        public ViewAppointmentViewModel(ApiClient apiClient, INavigationService navigationService)
        {
            _apiClient = apiClient;
            _navigationService = navigationService;

            LoadStatuses();
        }

        private async void LoadStatuses()
        {
            try
            {
                keyValueModels = await _apiClient.GetAsync<List<KeyValueModel>>(ApiEndpoints.GetAppointmentStatusAndIds);
                OnAppointmentChanged(Appointment);
            }
            catch (Exception ex) 
            {
                Console.WriteLine($" {ex.Message}");
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Appointment = (AppointmentModel)query["data"];
        }

        [RelayCommand]
        async Task GoBack() 
        {
            await _navigationService.GoBack();
        }

        [RelayCommand]
        async Task StepBack()
        {
            var currentModel = keyValueModels.FirstOrDefault(x => x.Value == Appointment.Status);

            if (currentModel != null)
            {
                // Find the index of the current status
                var currentIndex = keyValueModels.IndexOf(currentModel);

                // Check if there's a next model in the list
                if (currentIndex < keyValueModels.Count + 1)
                {
                    // Get the previous status
                    var nextStatus = keyValueModels[currentIndex - 1];

                    // Step the status back
                    Appointment.Status = nextStatus.Value;

                    // Notify UI of the change (if necessary)
                    OnPropertyChanged(nameof(Appointment));

                    // Put Data
                    await _apiClient.PutAsync(ApiEndpoints.UpdateAppointment, Appointment);
                    OnAppointmentChanged(Appointment);
                }
            }
        }

        [RelayCommand]
        async Task Progress()
        {
            var currentModel = keyValueModels.FirstOrDefault(x => x.Value == Appointment.Status);

            if (currentModel != null)
            {
                // Find the index of the current status
                var currentIndex = keyValueModels.IndexOf(currentModel);

                // Check if there's a next sttus in the list
                if (currentIndex < keyValueModels.Count - 1)
                {
                    // Get the next status
                    var nextModel = keyValueModels[currentIndex + 1];

                    // Update Appointment.Status to the Value of the next KeyValueModel
                    Appointment.Status = nextModel.Value;
                    OnPropertyChanged(nameof(Appointment));

                    // Put Data
                    await _apiClient.PutAsync(ApiEndpoints.UpdateAppointment, Appointment);
                    OnAppointmentChanged(Appointment);
                }
            }
        }

        // Used to update the enabled/disabled buttons each time a change is made to appointment
        partial void OnAppointmentChanged(AppointmentModel value)
        {
            OnPropertyChanged(nameof(IsBackButtonEnabled));
            OnPropertyChanged(nameof(IsForwardButtonEnabled));
        }

        public bool IsBackButtonEnabled => Appointment != null && keyValueModels.Count() > 0 && Appointment.Status != keyValueModels.OrderBy(x => x.Id).First().Value;
        public bool IsForwardButtonEnabled => Appointment != null && keyValueModels.Count() > 0 && Appointment.Status != keyValueModels.OrderBy(x => x.Id).Last().Value;
    }
}
