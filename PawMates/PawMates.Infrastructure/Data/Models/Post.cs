﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawMates.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PawMates.Infrastructure.Data.DataConstants;

namespace PawMates.Infrastructure.Data.Models
{
    public class Post 
    {
        [Key]
        [Comment("Post identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(PostNameMaxLenght)]
        [Comment("Post Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(PostDescriptionMaxLenght)]
        [Comment("Post Description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Comment("Post ImageUrl")]
        public string ImageUrl { get; set; } = string.Empty;


        public string CreatorId { get; set; } =string.Empty;
        [ForeignKey(nameof(CreatorId))]
        public IdentityUser Creator { get; set; } = null!;
    }
}
