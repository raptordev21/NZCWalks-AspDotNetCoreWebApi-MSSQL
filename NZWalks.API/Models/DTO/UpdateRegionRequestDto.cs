using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class UpdateRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Minimum 3 chars required")]
        [MaxLength(3, ErrorMessage = "Maximum length 3 chars exceeded")]
        public string Code { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "Maximum length 100 characters exceeded")]
        public string Name { get; set; }

        public string? RegionImageUrl { get; set; }
    }
}
