using PawMates.Infrastructure.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PawMates.Core.Models.PetStatusViewModels
{
    public class PetStatusViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public string Breed { get; set; } = string.Empty;

        public Status Status { get; set; }

        public string OwnerName { get; set; } = string.Empty;
    }
}
