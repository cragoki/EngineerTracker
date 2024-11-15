namespace EngineerTracker.Models.Appointments
{
    public class AppointmentModel
    {
        public int Id { get; set; }
        public int EngineerId { get; set; }
        public string? EngineerName { get; set; }
        public int LocationId { get; set; }
        public string? LocationName { get; set; }
        public DateTime Date { get; set; }
        public string? Status { get; set; }
    }
}
