using Microsoft.EntityFrameworkCore;
using PawMates.Data.Enums;
using PawMates.Data.Models;
using System.ComponentModel.DataAnnotations;
using static PawMates.Data.DataConstants;

namespace PawMates.Models
{
    public class PetFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(PetNameMaxLenght, MinimumLength = PetNameMinLenght, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [Range(PetAgeMinLenght, PetAgeMaxLenght, ErrorMessage = RangeIntErrorMessage)]
        public int Age { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        public string DateOfBirth { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(PetBreedMaxLenght, MinimumLength = PetBreedMinLenght, ErrorMessage = StringLengthErrorMessage)]
        public string Breed { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(PetColorMaxLenght, MinimumLength = PetColorMinLenght, ErrorMessage = StringLengthErrorMessage)]
        public string MainColor { get; set; } = string.Empty;

        [StringLength(PetColorMaxLenght, MinimumLength = PetColorMinLenght, ErrorMessage = StringLengthErrorMessage)]
        public string? SecondaryColor { get; set; }

        [Required]
        public Gender Gender { get; set; } 

        [Required]
        [Range(PetWeightMinLenght, PetWeightMaxLenght, ErrorMessage = RangeIntErrorMessage)]
        public double Weight { get; set; }

        public int PetTypeId { get; set; }

        public IEnumerable<PetTypesViewModel> PetTypes { get; set;} = new List<PetTypesViewModel>();
    }
}
