using EngineerTracker.ViewModels;

namespace EngineerTracker;

public partial class ViewAppointment : ContentPage
{

    public ViewAppointment(ViewAppointmentViewModel viewAppointmentViewModel)
    {
        InitializeComponent();
        BindingContext = viewAppointmentViewModel;
    }
}