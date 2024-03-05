using System.ComponentModel.DataAnnotations;
using static PawMates.Data.DataConstants;
namespace PawMates.Data.Models
{
    public class PetType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(TypeNameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(TypeDescriptionMaxLenght)]
        public string Description { get; set; } = string.Empty;
    }
}
