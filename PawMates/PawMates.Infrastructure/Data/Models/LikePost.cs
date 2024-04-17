using PawMates.Infrastructure.Data.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PawMates.Infrastructure.Data.Models
{
    public class LikePost
    {
        [Required]
        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }

        [Required]
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }
    }
}
