using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static PawMates.Data.DataConstants;
namespace PawMates.Data.Models
{
    public class PetType
    {
        [Key]
        [Comment("Pet type identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(TypeNameMaxLenght)]
        [Comment("Type name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(TypeDescriptionMaxLenght)]
        [Comment("Type description")]
        public string Description { get; set; } = string.Empty;
    }
}
