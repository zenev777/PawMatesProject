using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PawMates.Data.Enums;
using System.ComponentModel.DataAnnotations;
using static PawMates.Data.DataConstants;

namespace PawMates.Data.Models
{
    public class Owner 
    {
        [Key]
        [Comment("Owner identifier")]
        public int Id { get; set; }

        [Required]
        [MaxLength(UserNamesMaxLenght)]
        [Comment("User's first name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserNamesMaxLenght)]
        [Comment("User's last name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserCountryNameMaxLenght)]
        [Comment("User's countrty name")]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserAdressNameMaxLenght)]
        [Comment("User's adress name")]
        public string Adress { get; set; } = string.Empty;

        [Comment("User's gender")]
        public Gender Gender { get; set; }


        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}
