using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static PawMates.Infrastructure.Data.DataConstants;
namespace PawMates.Infrastructure.Data.Models
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

        [Comment("List of pets")]
        public List<Pet> Pets { get; set; }
    }
}
