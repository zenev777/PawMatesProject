namespace PawMates.Core.Models.EventViewModels
{
    public class EventDeleteViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartsOn { get; set; }
    }
}
