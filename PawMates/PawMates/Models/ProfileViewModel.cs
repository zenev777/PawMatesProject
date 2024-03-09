using PawMates.Data.Enums;
using PawMates.Data.Models;

namespace PawMates.Models
{
    public class ProfileViewModel
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Country { get; set; } = string.Empty;

        public string Adress { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        public ICollection<Pet> Pets { get; set; } = new List<Pet>();
    }
}
