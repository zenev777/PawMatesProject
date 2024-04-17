using System.ComponentModel.DataAnnotations;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.Core.Models.PostViewModels
{
    public class PostFormViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequireErrorMessage)]
        [Display(Name = "Image URL")]
        public string ImageUrl { get; set; } = string.Empty;

        public string Creator { get; set; } = string.Empty;
    }
}
