using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PawMates.Data.DataConstants;

namespace PawMates.Data.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(EventDescriptionMaxLenght)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [MaxLength(EventLocationMaxLenght)]
        public string Location { get; set; } = string.Empty;

        [Required]
        public DateTime StartsOn{ get; set; }

        [Required]
        public string OrganiserId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OrganiserId))]
        public IdentityUser Organiser { get; set; } = null!;

        public ICollection<EventParticipant> EventParticipants { get; set; }
            = new List<EventParticipant>();
    }
}
