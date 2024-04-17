using PawMates.Infrastructure.Data.Enums;
using System.ComponentModel.DataAnnotations;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.Core.Models.PetViewModels
{
    public class PetFormViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(PetNameMaxLenght, MinimumLength = PetNameMinLenght, ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Image URL")]
        public string? ImageUrl { get; set; }

        [Required(ErrorMessage = RequireErrorMessage)]
        [Display(Name = "Date of birth")]
        public string DateOfBirth { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(PetBreedMaxLenght, MinimumLength = PetBreedMinLenght, ErrorMessage = StringLengthErrorMessage)]
        public string Breed { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [StringLength(PetColorMaxLenght, MinimumLength = PetColorMinLenght, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "Main Color")]
        public string MainColor { get; set; } = string.Empty;

        [StringLength(PetColorMaxLenght, MinimumLength = PetColorMinLenght, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "Second Color")]
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
