namespace PawMates.Core.Models.PetViewModels
{
    public class PetDeleteViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
    }
}
