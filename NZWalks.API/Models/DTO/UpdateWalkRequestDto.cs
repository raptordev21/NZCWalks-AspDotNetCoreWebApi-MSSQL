using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length 100 characters exceeded")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Maximum length 1000 characters exceeded")]
        public string Description { get; set; }

        [Required]
        [Range(0, 50)]
        public double LengthInKms { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

        [Required]
        public Guid RegionId { get; set; }
    }
}
