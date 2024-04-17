using PawMates.Infrastructure.Data.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawMates.Infrastructure.Data.Models
{
    public class EventParticipant
    {
        [Required]
        public string HelperId { get; set; } = string.Empty;

        [ForeignKey(nameof(HelperId))]

        public ApplicationUser Helper { get; set; } = null!;

        [Required]
        public int EventId { get; set; }

        [ForeignKey(nameof(EventId))]

        public Event Event { get; set; } = null!;
    }
}