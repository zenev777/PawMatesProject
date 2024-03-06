using PawMates.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static PawMates.Data.DataConstants;

namespace PawMates.Data.Models
{
    public class ApplicationUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(UserNamesMaxLenght)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserNamesMaxLenght)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserCountryNameMaxLenght)]
        public string Country { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserAdressNameMaxLenght)]
        public string Adress { get; set; } = string.Empty;

        [Required]
        [MaxLength(UserPhoneMaxLenght)]
        public string MobileNumber { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        public int PetId { get; set; }
        [ForeignKey(nameof(PetId))]
        public Pet Pet { get; set; } = null!;

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}
