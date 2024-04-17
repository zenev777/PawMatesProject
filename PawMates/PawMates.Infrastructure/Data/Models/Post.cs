using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawMates.Infrastructure.Data.Enums;
using PawMates.Infrastructure.Data.IdentityModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.Infrastructure.Data.Models
{
    public class Post 
    {
        [Key]
        [Comment("Post identifier")]
        public int Id { get; set; }

        [Comment("Post Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Post ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;

        public string CreatorId { get; set; } =string.Empty;
        [ForeignKey(nameof(CreatorId))]
        public ApplicationUser Creator { get; set; } = null!;

        public List<LikePost> LikePosts { get; set; } = new List<LikePost>();
    }
}
