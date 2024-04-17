using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PawMates.Infrastructure.Data.DataConstants.IdentityConstants;
using static PawMates.Infrastructure.Data.DataConstants.DataConstants;

namespace PawMates.Core.Models.IdentityViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [StringLength(UserUserameMaxLenght, MinimumLength = UserUserameMinLenght, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "Username")]
        public string UserName { get; set; } = null!;

        [StringLength(UserNamesMaxLenght, MinimumLength = UserNamesMinLenght,ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "First Name")]
        public string? FirstName { get; set; } = null!;

        [StringLength(UserNamesMaxLenght, MinimumLength = UserNamesMinLenght, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; } = null!;

        [StringLength(UserPhoneNumMaxLenght, MinimumLength = UserPhoneNumMinLenght, ErrorMessage = StringLengthErrorMessage)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; } = null!;

        [Required]
        [EmailAddress]
        [StringLength(UserEmailMaxLenght, MinimumLength = UserEmailMinLenght, ErrorMessage = StringLengthErrorMessage)]
        public string Email { get; set; } = null!;

        [Required]
        [StringLength(UserPasswordMaxLenght, MinimumLength = UserPasswordMinLenght, ErrorMessage = StringLengthErrorMessage)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = null!;

        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; } = null!;
    }
}
