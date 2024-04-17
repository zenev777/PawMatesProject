using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawMates.Infrastructure.Data.DataConstants.IdentityConstants;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

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
