using PawMates.Infrastructure.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace PawMates.Core.Models.PetViewModels
{
    public class AllPetsQueryModel
    {
        public int PetsPerPage { get; } = 4;

        public string PetType { get; init; } = null!;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; } = null!;

        //public HouseSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPetsCount { get; set; }

        public IEnumerable<string> PetTypes { get; set; } = null!;

        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}
