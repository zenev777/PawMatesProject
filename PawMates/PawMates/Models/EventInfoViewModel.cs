using System.ComponentModel.DataAnnotations;

namespace PawMates.Models
{
    public class EventInfoViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;

        public string StartsOn { get; set; } = string.Empty;

        public string OrganiserId { get; set; } = string.Empty;
    }
}
