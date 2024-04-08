using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Models.PetViewModels
{
    public class AllPetsQueryModel
    {
        public int PetsPerPage { get; } = 3;

        public string PetType { get; init; } = null!;

        [Display(Name = "Search by text")]
        public string SearchTerm { get; init; } = null!;

        //public HouseSorting Sorting { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalPetsCount { get; set; }

        public IEnumerable<string> PetTypes { get; set; } = null!;

        public List<PetInfoViewModel> Pets { get; set; } = new List<PetInfoViewModel>();
    }
}
