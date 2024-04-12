using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawMates.Infrastructure.Data.DataConstants.IdentityConstants;

namespace PawMates.Core.Models.IdentityViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserUserameMaxLenght, MinimumLength = UserUserameMinLenght)]
        public string UserName { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(UserEmailMaxLenght, MinimumLength = UserEmailMinLenght)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(UserPasswordMaxLenght, MinimumLength = UserPasswordMinLenght)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;
    }
}
