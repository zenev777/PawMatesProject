using Microsoft.EntityFrameworkCore;
using PawMates.Infrastructure.Data.Enums;
using PawMates.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;
using static PawMates.Infrastructure.Data.DataConstants;

namespace PawMates.Core.Models.PetViewModels
{
    public class PetFormViewModel
    {

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(PetNameMaxLenght, MinimumLength = PetNameMinLenght, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        public string? ImageUrl { get; set; }

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

        [Required(ErrorMessage = RequireErrorMessage)]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [Range(PetWeightMinLenght, PetWeightMaxLenght, ErrorMessage = RangeIntErrorMessage)]
        public double Weight { get; set; }

        public int PetTypeId { get; set; }

        public IEnumerable<PetTypesViewModel> PetTypes { get; set; } = new List<PetTypesViewModel>();
    }
}
