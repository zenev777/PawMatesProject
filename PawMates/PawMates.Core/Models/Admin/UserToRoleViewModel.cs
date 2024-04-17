using System.ComponentModel.DataAnnotations;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;
using static PawMates.Infrastructure.Data.DataConstants.IdentityConstants;

namespace PawMates.Core.Models.Admin
{
    public class UserToRoleViewModel : AddRoleViewModel
    {
        public int RoleId { get; set; }
        
        [Required]
        [StringLength(UserUserameMaxLenght, MinimumLength = UserUserameMinLenght, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = string.Empty;


        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
