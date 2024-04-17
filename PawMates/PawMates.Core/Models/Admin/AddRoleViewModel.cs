using System.ComponentModel.DataAnnotations;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;
using static PawMates.Infrastructure.Data.DataConstants.IdentityConstants;

namespace PawMates.Core.Models.Admin
{
    public class AddRoleViewModel
    {
        [Required]
        [StringLength(UserRoleNameMaxLenght, MinimumLength = UserRoleNameMinLenght, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "Role Name")]
        public string RoleName { get; set; } = string.Empty;
    }
}
