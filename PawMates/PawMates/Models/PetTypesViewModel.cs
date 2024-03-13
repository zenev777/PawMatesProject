using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PawMates.Models
{
    public class PetTypesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

    }
}