using PawMates.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PawMates.Data.DataConstants;

namespace PawMates.Data.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(PetNameMaxLenght)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [MaxLength(PetAgeMaxLenght)]
        public int Age { get; set; }

        [Required]
        public DateTime MyProperty { get; set; }

        [Required]
        [MaxLength(PetBreedMaxLenght)]
        public string Breed { get; set; } = string.Empty;

        [Required]
        [MaxLength(PetColorMaxLenght)]
        public string MainColor { get; set; } = string.Empty;

        [MaxLength(PetColorMaxLenght)]
        public string SecondaryColor { get; set; } = string.Empty;

        [Required]
        public Gender Gender { get; set; }

        [Required]
        [MaxLength(PetWeightMaxLenght)]
        public double Weight { get; set; }

        [Required]
        public int PetTypeId { get; set; }
        [ForeignKey(nameof(PetTypeId))]
        public PetType PetType { get; set; } = null!;
    }
}
