using PawMates.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace PawMates.Models
{
	public class PetInfoViewModel
	{
		public int Id { get; set; }

		public string Name { get; set; } = string.Empty;

		public string ImageUrl { get; set; } = string.Empty;

		public string DateOfBirth { get; set; } = string.Empty;

		public string Breed { get; set; } = string.Empty;

		public string MainColor { get; set; } = string.Empty;

		public string? SecondaryColor { get; set; }

		public Gender Gender { get; set; }

		public double Weight { get; set; }

		public string PetType { get; set; } = string.Empty;

		public string OwnerId { get; set; } = null!;

	}
}
