namespace EngineerTracker
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(EngineerAppointments), typeof(EngineerAppointments));
            Routing.RegisterRoute(nameof(ViewAppointment), typeof(ViewAppointment));

        }
    }
}
