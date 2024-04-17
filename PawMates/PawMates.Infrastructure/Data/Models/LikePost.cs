using PawMates.Infrastructure.Data.IdentityModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
