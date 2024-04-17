using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static PawMates.Infrastructure.Data.DataConstants.IdentityConstants;

namespace PawMates.Infrastructure.Data.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        [StringLength(UserNamesMaxLenght)]
        public string? FirstName { get; set; }

        [StringLength(UserNamesMaxLenght)]
        public string? LastName { get; set; }
    }
}
