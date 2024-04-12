using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawMates.Infrastructure.Data.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.Infrastructure.Data.Models
{
    public class Event
    {
        [Key]
        [Comment("Event identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLenght)]
        [Comment("Event name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(EventDescriptionMaxLenght)]
        [Comment("Event description")]
        public string? Description { get; set; }

        [Required]
        [MaxLength(EventLocationMaxLenght)]
        [Comment("Event location")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Comment("Event date and hour")]
        public DateTime StartsOn{ get; set; }

        [Required]
        [Comment("Organiser identifier")]
        public string OrganiserId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OrganiserId))]
        public ApplicationUser Organiser { get; set; } = null!;

        public ICollection<EventParticipant> EventParticipants { get; set; }
            = new List<EventParticipant>();
    }
}
