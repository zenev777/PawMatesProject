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
    public class UserToRoleViewModel : AddRoleViewModel
    {
        public int RoleId { get; set; }
        
        [Required]
        [StringLength(UserUserameMaxLenght, MinimumLength = UserUserameMinLenght, ErrorMessage = StringLengthErrorMessage)]
        public string UserName { get; set; } = string.Empty;


        public IEnumerable<string> Roles { get; set; } = new List<string>();
    }
}
