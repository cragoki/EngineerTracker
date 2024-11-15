using EngineerTracker.ViewModels;

namespace EngineerTracker;

public partial class EngineerAppointments : ContentPage
{

    public EngineerAppointments(EngineerAppointmentsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}